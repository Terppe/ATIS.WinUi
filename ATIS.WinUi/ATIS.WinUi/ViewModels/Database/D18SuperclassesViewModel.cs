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
using Microsoft.UI.Xaml.Data;

namespace ATIS.WinUi.ViewModels.Database
{
    public class D18SuperclassesViewModel : ViewModelBase
    {
        #region [Constructor]
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();


        public D18SuperclassesViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>();
            }
        }
        public bool IsInDesignMode { get; set; }

        #endregion [Constructor]     

        #region [Commands Regnum]




        private RelayCommand _getRegnumsByNameOrIdCommand;
        public ICommand GetRegnumsByNameOrIdCommand => _getRegnumsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetRegnumsByNameOrId(SearchRegnumName); });
        private RelayCommand _addRegnumCommand;
        public ICommand AddRegnumCommand => _addRegnumCommand ??= new RelayCommand(delegate { ExecuteAddRegnum(null); });


        #endregion [Commands Regnum]       

        #region [Methods Regnum]

        private void ExecuteGetRegnumsByNameOrId(string searchName)
        {
            if (Tbl03RegnumsList == null)
                Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsList.Clear();

            //Tbl03RegnumsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl03Regnum>(searchName, "Regnum");

            Tbl03RegnumsList = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_uow.Tbl03Regnums
                    .Find(e => e.RegnumId == id))
                : new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_uow.Tbl03Regnums
                    .Find(e => e.RegnumName.StartsWith(searchName))
                    .OrderBy(a => a.RegnumName)
                    .ThenBy(a => a.Subregnum)
                );
            //   Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
            //    RegnumsList     = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());

            //         if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl03RegnumsList.Count)) return;

            //SelectedMainTabIndex = 0;
            //SelectedDetailTabIndex = 0;

            //   RegnumsView.CollectionViewSource(Tbl03RegnumsList);
            //   RegnumsView.Refresh();
            // RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //RegnumsView.Refresh();
        }

        private void ExecuteAddRegnum(object o)
        {
            if (Tbl03RegnumsList == null)
                Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();

            //SelectedMainTabIndex = 0;
            //SelectedDetailTabIndex = 0;

            Tbl03RegnumsList ??= new ObservableCollection<Tbl03Regnum>();
            Tbl03RegnumsList.Insert(0, new Tbl03Regnum { RegnumName = "neu" });

            //RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //RegnumsView = (ICollectionView)Tbl03RegnumsList;
            //RegnumsView.MoveCurrentToFirst();
        }

        #endregion [Methods Regnum]



        #region "Public Properties Tbl03Regnum"

        private string _searchRegnumName = "";
        public string SearchRegnumName
        {
            get => _searchRegnumName;
            set { _searchRegnumName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RegnumsView;
        private Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsList
        {
            get => _tbl03RegnumsList;
            set { _tbl03RegnumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl03Regnum> _tbl03RegnumsAllList;
        public ObservableCollection<Tbl03Regnum> Tbl03RegnumsAllList
        {
            get => _tbl03RegnumsAllList;
            set { _tbl03RegnumsAllList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }


        #endregion "Public Properties"

        #region other Props
        public int RegnumId { get; set; }
        public string RegnumName { get; set; }
        public string Subregnum { get; set; }
        public int CountId { get; set; }
        public bool? Valid { get; set; }
        public string ValidYear { get; set; }
        public string Synonym { get; set; }
        public string Author { get; set; }
        public string AuthorYear { get; set; }
        public string Info { get; set; }
        public string EngName { get; set; }
        public string GerName { get; set; }
        public string FraName { get; set; }
        public string PorName { get; set; }
        public string Writer { get; set; }
        public System.DateTime WriterDate { get; set; }
        public string Updater { get; set; }
        public System.DateTime UpdaterDate { get; set; }
        public string Memo { get; set; }


        #endregion
    }
}
