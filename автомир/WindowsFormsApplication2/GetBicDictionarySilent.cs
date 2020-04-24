using System;

namespace WindowsFormsApplication2
{
    /// <summary>
    /// реализация для запуска по расписанию
    /// </summary>
    public class GetBicDictionarySilent : IGetBicDataView
    {
        public void ShowView()
        {
            EventHandler<BicEventArgs> handler = GetDataEvent;
            if(handler != null)
                handler(null, new BicEventArgs(DateTime.Now));
        }

        public event EventHandler<BicEventArgs> GetDataEvent;
        public void AddLog(string mesage)
        {
            // запись в лог файл
        }

        public void AddExceptionLog(string mesage)
        {
            // запись в лог файл
        }
    }
}