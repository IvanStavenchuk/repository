using System;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IGetBicDataView view;
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
                // оконный режим для пользователя
                view = new GetBicDictionaryView();
            else if (args.Length == 2 && (args[1] == "\\s" || args[1] == "\\S"))
                // тихий режим для сервиса
                view = new GetBicDictionarySilent();
            else
                return;

            GetBicDictionaryPresenter getBicDictionaryPresenter = new GetBicDictionaryPresenter(view);
            getBicDictionaryPresenter.ShowView();

            Application.EnableVisualStyles();
            Application.Run(view as Form);
        }
    }
}
