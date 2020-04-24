using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects.DataClasses;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using System.Data.Objects;
using System.Linq;

namespace WindowsFormsApplication2
{
    public class GetBicDictionaryPresenter
    {
        readonly IGetBicDataView _view;
        public GetBicDictionaryPresenter(IGetBicDataView view)
        {
            _view = view;
            _view.GetDataEvent += ViewOnGetDataEvent;
        }

        private void ViewOnGetDataEvent(object sender, BicEventArgs eventArgs)
        {
            try
            {
                _view.AddLog("Выбранная дата " + eventArgs.SelectedDateTime.ToShortDateString());
                //Скачивание данных на выбранную дату
                string s = eventArgs.SelectedDateTime.ToString("yyyyMMdd");
                FileInfo fileInfo = new FileInfo(Path.GetTempFileName());
                //http://www.cbr.ru/VFS/mcirabis/BIKNew/20200409ED01OSBR.zip
                using (WebClient client = new WebClient())
                {
                    _view.AddLog("Скачивание");
                    client.DownloadFile("http://www.cbr.ru/VFS/mcirabis/BIKNew/" + s + "ED01OSBR.zip", fileInfo.FullName);
                }

                // распаковка
                _view.AddLog("Распаковка");
                string path = fileInfo.Directory.FullName + "\\" + s + "_ED807_full.xml";
                if (File.Exists(path))
                    File.Delete(path);
                ZipFile.ExtractToDirectory(fileInfo.FullName, fileInfo.Directory.FullName);

                {
                    // чтение xml
                    FileStream fs = new FileStream(path, FileMode.Open);
                    XmlReader xmlReader = XmlReader.Create(fs);
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof (ED807));
                    _view.AddLog("Чтение");
                    object deserialize = xmlSerializer.Deserialize(xmlReader);
                    xmlReader.Close();
                    fs.Close();

                    if (deserialize == null)
                        return;

                    _view.AddLog("Сопоставление");
                    Model1Container contex = new Model1Container();

                    ObjectSet<BICDirectoryEntry> bicDirectoryEntries = contex.CreateObjectSet<BICDirectoryEntry>();
                    ObjectSet<ParticipantInfo> participantInfos = contex.CreateObjectSet<ParticipantInfo>();
                    ObjectSet<Accounts> accountses = contex.CreateObjectSet<Accounts>();

                    // перекладывание данных в сущности EntityFramework
                    foreach (XmlBicDirectoryEntry xmlBicDirectoryEntry in (deserialize as ED807).BICDirectoryEntry)
                    //(deserialize as ED807).BICDirectoryEntry.AsParallel()
                    {
                        // ищем существующий или создаем новый объект
                        BICDirectoryEntry bicDirectoryEntry =
                            bicDirectoryEntries.SingleOrDefault(x => x.BIC == xmlBicDirectoryEntry.BIC) ??
                            new BICDirectoryEntry() {Accounts = new EntityCollection<Accounts>()};

                        if (string.IsNullOrWhiteSpace(bicDirectoryEntry.BIC))
                        {
                            bicDirectoryEntry = CopyObjectProperties(xmlBicDirectoryEntry, bicDirectoryEntry);
                            contex.AddToBICDirectoryEntrySet(bicDirectoryEntry);
                        }

                        // ищем существующий или создаем новый объект
                        ParticipantInfo participantInfo;
                        if (bicDirectoryEntry.ParticipantInfo == null)
                        {
                            participantInfo = new ParticipantInfo();
                            bicDirectoryEntry.ParticipantInfo =
                                CopyObjectProperties(xmlBicDirectoryEntry.ParticipantInfo,
                                    participantInfo);
                            contex.AddToParticipantInfoSet(participantInfo);
                        }
                        else
                        {
                            participantInfo =
                                participantInfos.SingleOrDefault(x => x.Id == bicDirectoryEntry.ParticipantInfo.Id);
                            participantInfo = CopyObjectProperties(xmlBicDirectoryEntry.ParticipantInfo, participantInfo);
                        }

                        if (bicDirectoryEntry.Accounts == null)
                            bicDirectoryEntry.Accounts = new EntityCollection<Accounts>();

                        List<Accounts> newAccs = new List<Accounts>();
                        // ищем существующий или создаем новый объект
                        foreach (XmlAccounts xmlAccounts in xmlBicDirectoryEntry.Accounts.AsParallel())
                        {
                            if (bicDirectoryEntry.EntityState == EntityState.Added)
                            {
                                // если бик новый, то все счета создаем новые
                                Accounts accounts = CopyObjectProperties(xmlAccounts, new Accounts());
                                newAccs.Add(accounts);
                            }
                            else
                            {
                                Accounts accounts =
                                    bicDirectoryEntry.Accounts.AsParallel().SingleOrDefault(x => x.Account.Equals(xmlAccounts.Account)) ??
                                    new Accounts();
                                accounts = CopyObjectProperties(xmlAccounts, accounts);
                                if (accounts.EntityState == EntityState.Detached)
                                {
                                    newAccs.Add(accounts);
                                }
                            }
                        }

                        // удаляем счета, которых больше нет в источнике данных
                        if (bicDirectoryEntry.EntityState != EntityState.Added)
                        {
                            IEnumerable<Accounts> deletedAccounts =
                                from c in bicDirectoryEntry.Accounts.AsParallel()
                                where !(from o in xmlBicDirectoryEntry.Accounts.AsParallel()
                                    select o.Account)
                                    .Contains(c.Account)
                                select c;
                            foreach (Accounts acc in deletedAccounts)
                            {
                                contex.DeleteObject(acc);
                            }
                        }

                        // добавляем новые счета
                        foreach (Accounts acc in newAccs)
                        {
                            bicDirectoryEntry.Accounts.Add(acc);
                            contex.AddToAccountsSet(acc);
                        }
                    }

                    //сохранение в бд
                    //
                    _view.AddLog("Сохранение");
                    contex.SaveChanges(SaveOptions.None);
                    _view.AddLog("Готово");
                }
            }
            catch (Exception ex)
            {
                _view.AddExceptionLog(ex.ToString());
            }
        }

        private T2 CopyObjectProperties<T1, T2>(T1 from, T2 to)
        {
            // копирование одноименных свойств с помощью Reflection
            var sourceProps = typeof(T1).GetProperties().Where(x => x.CanRead && x.GetCustomAttributes(true).Any(a => a is XmlAttributeAttribute)).ToList();
            var destProps = typeof(T2).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { 
                        p.SetValue(to, sourceProp.GetValue(from, null), null);
                    }
                }

            }
            return to;
        }

        public void ShowView()
        {
            _view.ShowView();
        }
    }
    [Serializable]
    [XmlRoot(ElementName = "ED807", Namespace = "urn:cbr-ru:ed:v2.0", IsNullable = false)]
    public class ED807
    {
        [XmlAttribute]
        public string EDNo { get; set; }
        [XmlAttribute]
        public string EDDate { get; set; }
        [XmlAttribute]
        public string EDAuthor { get; set; }
        [XmlAttribute]
        public string CreationReason { get; set; }
        [XmlAttribute]
        public string CreationDateTime { get; set; }
        [XmlAttribute]
        public string InfoTypeCode { get; set; }
        [XmlAttribute]
        public string BusinessDay { get; set; }
        [XmlAttribute]
        public string DirectoryVersion { get; set; }

        [XmlElement("BICDirectoryEntry")]
        public List<XmlBicDirectoryEntry> BICDirectoryEntry { get; set; }
    }
    [Serializable]
    public class XmlBicDirectoryEntry
    {
        [XmlAttribute]
        public string BIC { get; set; }
        [XmlElement("ParticipantInfo")]
        public XmlParticipantInfo ParticipantInfo { get; set; }
        [XmlElement("Accounts")]
        public List<XmlAccounts> Accounts { get; set; }
    }
    [Serializable]
    public class XmlParticipantInfo
    {
        [XmlAttribute]
        public string NameP { get; set; }
        [XmlAttribute]
        public string CntrCd { get; set; }
        [XmlAttribute]
        public string Rgn { get; set; }
        [XmlAttribute]
        public string Ind { get; set; }
        [XmlAttribute]
        public string Tnp { get; set; }
        [XmlAttribute]
        public string Nnp { get; set; }
        [XmlAttribute]
        public string Adr { get; set; }
        [XmlAttribute]
        public string DateIn { get; set; }
        [XmlAttribute]
        public string PtType { get; set; }
        [XmlAttribute]
        public string Srvcs { get; set; }
        [XmlAttribute]
        public string XchType { get; set; }
        [XmlAttribute]
        public string UID { get; set; }
        [XmlAttribute]
        public string ParticipantStatus { get; set; }
    }
    [Serializable]
    public class XmlAccounts 
    {
        [XmlAttribute]
        public string Account { get; set; }
        [XmlAttribute]
        public string RegulationAccountType { get; set; }
        [XmlAttribute]
        public string CK { get; set; }
        [XmlAttribute]
        public string AccountCBRBIC { get; set; }
        [XmlAttribute]
        public string DateIn { get; set; }
        [XmlAttribute]
        public string AccountStatus { get; set; }
    }
}
