using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ATIS.WinUi.Core;
using ATIS.WinUi.DataLayer.Models;
using ATIS.WinUi.Helper;
using Microsoft.UI.Xaml.Data;

namespace ATIS.WinUi.ViewModels.Database
{
    class D12SubphylumsViewModel : ViewModelBase
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        public D12SubphylumsViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

                // Code runs "for real" 
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>();
            }
        }

        public bool IsInDesignMode { get; set; }

        #region [Commands Phylum]

        private RelayCommand _getPhylumsByNameOrIdCommand;
        public ICommand GetPhylumsByNameOrIdCommand => _getPhylumsByNameOrIdCommand ??= new RelayCommand(delegate { ExecuteGetPhylumsByNameOrId(SearchPhylumName); });

        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { ExecuteAddPhylum(null); });


        #endregion [Commands Phylum]       


        #region [Methods Phylum]

        private void ExecuteGetPhylumsByNameOrId(string searchName)
        {
            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();

            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());

            //    Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");
            //       Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            if (Tbl06PhylumsList == null)
                Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();
            else
                Tbl06PhylumsList.Clear();

            Tbl06PhylumsList = int.TryParse(searchName, out var id)
                ? new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_uow.Tbl06Phylums
                    .Find(e => e.PhylumId == id))
                : new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_uow.Tbl06Phylums
                    .Find(e => e.PhylumName.StartsWith(searchName))
                    .OrderBy(a => a.PhylumName)
                );

            //  Tbl06PhylumsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl06Phylum>(searchName, "Phylum");

            //if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl06PhylumsList.Count)) return;

            //SelectedMainTabIndex = 0;
            //SelectedDetailTabIndex = 1;

            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.Refresh();
        }

        private void ExecuteAddPhylum(object o)
        {
            if (Tbl06PhylumsList == null)
                Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();

            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();

            //    Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");
            Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());

            Tbl06PhylumsList.Insert(0, new Tbl06Phylum { PhylumName = "New" });

            //SelectedMainTabIndex = 0;
            //SelectedDetailTabIndex = 1;

            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.MoveCurrentToFirst();
        }

        #endregion [Methods Phylum]

        //private Tbl06Phylum _selected;
        //public Tbl06Phylum Selected
        //{
        //    get => _selected;
        //    set { _selected = value; RaisePropertyChanged(""); }
        //}

        //public Tbl06Phylum Selected
        //{
        //    get => _selected;
        //    set => SetProperty(ref _selected, value);
        //}

        public ObservableCollection<Tbl06Phylum> SampleItems { get; private set; } = new ObservableCollection<Tbl06Phylum>();

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



        #endregion "Public Properties"

        #region "Public Properties Tbl06Phylum"

        private string _searchPhylumName = "";
        public string SearchPhylumName
        {
            get => _searchPhylumName;
            set { _searchPhylumName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView PhylumsView;
        private Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList;
            set { _tbl06PhylumsList = value; RaisePropertyChanged(""); }
        }

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsAllList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsAllList
        {
            get => _tbl06PhylumsAllList;
            set { _tbl06PhylumsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"   

        #region other Props
        public int PhylumId { get; set; }
        public int RegnumId { get; set; }
        public string PhylumName { get; set; }
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
