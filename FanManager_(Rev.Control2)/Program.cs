using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace TrayIcon_Rev.Center2
{
    static class Program
    {
        private static Mutex _mutex = new Mutex(true, "AppMutex");
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!_mutex.WaitOne(0, true))
            {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TrayIcon_Rev());
        }
    }
}
