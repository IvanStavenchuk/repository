using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    /// <summary>
    /// Оконная реализация для пользователя
    /// </summary>
    public partial class GetBicDictionaryView : Form, IGetBicDataView
    {

        public GetBicDictionaryView()
        {
            InitializeComponent();
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            lbLog.Items.Clear();
            Refresh();
            EventHandler<BicEventArgs> handler = GetDataEvent;
            if(handler != null)
                handler(null, new BicEventArgs(GetSelectedDateTime));
        }

        public void ShowView()
        {
            Show();
        }

        public event EventHandler<BicEventArgs> GetDataEvent;

        private DateTime GetSelectedDateTime
        {
            get { return dtpGetData.Value; }
        }

        public void AddLog(string mesage)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate { AddLog(mesage); });
                return;
            }

            lbLog.Items.Add(mesage);
            lbLog.Refresh();
        }

        public void AddExceptionLog(string mesage)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)delegate { AddExceptionLog(mesage); });
                return;
            }

            MessageBox.Show(mesage,"Ошибка", MessageBoxButtons.OK);
        }
    }
}
