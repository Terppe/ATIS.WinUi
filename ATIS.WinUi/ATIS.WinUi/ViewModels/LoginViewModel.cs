using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ATIS.WinUi.Core;
using ATIS.WinUi.DataLayer.Models;
using ATIS.WinUi.Helper;
using Microsoft.EntityFrameworkCore;

namespace ATIS.WinUi.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly AtisDbContext _context = new AtisDbContext();

        public LoginViewModel()
        {
             TblUserProfileList = new ObservableCollection<TblUserProfile> {_context.TblUserProfiles.SingleOrDefault(i => i.Email == Username)};
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(nameof(Username)); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(nameof(Password)); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; RaisePropertyChanged(nameof(Message)); }
        }

        private ObservableCollection<TblUserProfile> _tblUserProfileList;
        public ObservableCollection<TblUserProfile> TblUserProfileList
        {
            get => _tblUserProfileList;
            set { _tblUserProfileList = value; RaisePropertyChanged(""); }
        }

    }
}
