using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Command;

namespace KeepAwakeTray
{
    public class AppManager
    {
        public RelayCommand ExitCommand { get; }

        public AppManager()
        {
            ExitCommand = new RelayCommand(() => Application.Current.Shutdown());
        }

        public void Start()
        {
            
        }
    }
}
