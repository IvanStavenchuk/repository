using System;

namespace WindowsFormsApplication2
{
    public class BicEventArgs : EventArgs
    {
        public BicEventArgs(DateTime arg)
        {
            SelectedDateTime = arg;
        }

        public DateTime SelectedDateTime { get; private set; }
    }

    public interface IGetBicDataView
    {
        void ShowView();
        event EventHandler<BicEventArgs> GetDataEvent;
        void AddLog(string mesage);
        void AddExceptionLog(string mesage);
    }
}