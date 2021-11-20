using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using KemberFrontend.View;

namespace KemberFrontend.ViewModel
{
    internal class AutorisationViewModel : ViewModelBase
    {
        private UserControl Main = new MainPage();
        //private UserControl Autorisation = new AutorisationPage();
        private UserControl _CurPage = new MainPage();

        public UserControl CurWinPage
        {
            get => _CurPage;
            set => Set(ref _CurPage, value);
        }

        public ICommand OpenMainPage
        {
            get
            {
                return new RelayCommand(() => CurWinPage = Main);
            }
        }
    }
}
