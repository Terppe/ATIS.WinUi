using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Input;
using ATIS.WinUi.Core;
using ATIS.WinUi.DataLayer.Models;
using ATIS.WinUi.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml.Data;

namespace ATIS.WinUi.ViewModels.Database
{
    public class D03RegnumsViewModel : ViewModelBase
    {
        #region [Private Data Members]
        //private readonly CrudFunctions _extCrud = new CrudFunctions();
        //private readonly DeleteFunctions _extDelete = new DeleteFunctions();
        //private readonly SaveFunctions _extSave = new SaveFunctions();
        //private readonly AllMessageBoxes _allMessageBoxes = new AllMessageBoxes();
        private int _position;

        #endregion [Private Data Members]               

        #region [Constructor]
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();


        public D03RegnumsViewModel()
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
        private RelayCommand _copyRegnumCommand;
        public ICommand CopyRegnumCommand => _copyRegnumCommand ??= new RelayCommand(delegate { ExecuteCopyRegnum(null); });
        private RelayCommand _deleteRegnumCommand;
        public ICommand DeleteRegnumCommand => _deleteRegnumCommand ??= new RelayCommand(delegate { ExecuteDeleteRegnum(SearchRegnumName); });
        private RelayCommand _saveRegnumCommand;
        public ICommand SaveRegnumCommand => _saveRegnumCommand ??= new RelayCommand(delegate { ExecuteSaveRegnum(SearchRegnumName); });

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
                ? new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                    .Find(e => e.RegnumId == id))
                : new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums
                    .Find(e => e.RegnumName.StartsWith(searchName))
                    .OrderBy(a => a.RegnumName)
                    .ThenBy(a => a.Subregnum)
                );

            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_context.Tbl06Phylums
                .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                .OrderBy(k => k.PhylumName));

            Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_context.Tbl09Divisions
                .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                .OrderBy(k => k.DivisionName));

            //           Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(SelectedRegnum.RegnumId);

            Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefExperts)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefSourceId == null)
                .OrderBy(k => k.Info));

            Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefSources)
                .Where(e => e.RegnumId == id && e.RefAuthorId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));

            Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                .Include(a => a.Tbl90RefAuthors)
                .Where(e => e.RegnumId == id && e.RefSourceId == null && e.RefExpertId == null)
                .OrderBy(k => k.Info));

            Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_context.Tbl93Comments
                .Where(e => e.RegnumId == id)
                .OrderBy(e => e.Info));

            //   Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_uow.Tbl03Regnums.GetAll());
            //    RegnumsList     = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());

            //         if (_allMessageBoxes.NoDatasetFoundInfoMessageBox(Tbl03RegnumsList.Count)) return;

            //SelectedMainTabIndex = 0;
            //SelectedDetailTabIndex = 0;

            //  RegnumsView.CollectionViewSource(Tbl03RegnumsList);
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

        private void ExecuteCopyRegnum(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;

            //Tbl03RegnumsList = _extCrud.CopyRegnum(CurrentTbl03Regnum);

            //// evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            //RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //RegnumsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteRegnum(string searchName)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;

            //_extDelete.DeleteRegnum(CurrentTbl03Regnum);

            //Tbl03RegnumsList = _extCrud.GetCollectionFromSearchNameOrIdOrderBy<Tbl03Regnum>(searchName, "Regnum");
            //RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //RegnumsView.MoveCurrentToLast();
        }

        private void ExecuteSaveRegnum(string searchName)
        {
      //      if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl03Regnum)) return;


      //      _position = RegnumsView.CurrentPosition;

            //var ret = _extSave.SaveRegnum(CurrentTbl03Regnum);

            //if (ret != true)
            //{
            //    RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //    RegnumsView.Refresh();
            //    return;
            //}

            //if (CurrentTbl03Regnum.RegnumId == 0) //new
            //{
            //    Tbl03RegnumsList = _extCrud.GetLastRegnumsDatasetOrderById();
            //    RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //    RegnumsView.MoveCurrentToFirst();
            //}
            //else
            //{
            //    Tbl03RegnumsList = _extCrud.GetRegnumsCollectionFromSearchNameOrIdOrderBy<Tbl03Regnum>(searchName);
            //    RegnumsView = CollectionViewSource.GetDefaultView(Tbl03RegnumsList);
            //    RegnumsView.MoveCurrentToPosition(_position);
            //}
        }

        #endregion [Methods Regnum]

        //    Part 2    

        //    Part 3    

        //    Part 4    

        #region [Public Commands Connect ==> Tbl06Phylum]                 

        private RelayCommand _addPhylumCommand;
        public ICommand AddPhylumCommand => _addPhylumCommand ??= new RelayCommand(delegate { ExecuteAddPhylum(null); });

        private RelayCommand _copyPhylumCommand;
        public ICommand CopyPhylumCommand => _copyPhylumCommand ??= new RelayCommand(delegate { ExecuteCopyPhylum(null); });

        private RelayCommand _deletePhylumCommand;
        public ICommand DeletePhylumCommand => _deletePhylumCommand ??= new RelayCommand(delegate { ExecuteDeletePhylum(null); });

        private RelayCommand _savePhylumCommand;
        public ICommand SavePhylumCommand => _savePhylumCommand ??= new RelayCommand(delegate { ExecuteSavePhylum(null); });

        #endregion [Public Commands Connect ==> Tbl06Phylum]    

        #region [Public Methods Connect ==> Tbl06Phylum]                   

        private void ExecuteAddPhylum(object o)
        {
            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();

     //       Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 0;

            Tbl06PhylumsList ??= new ObservableCollection<Tbl06Phylum>();

            Tbl06PhylumsList.Insert(0, new Tbl06Phylum { PhylumName = "New" });

            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyPhylum(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            //Tbl06PhylumsList = _extCrud.CopyPhylum(CurrentTbl06Phylum);

            //// evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.MoveCurrentToFirst();
        }

        private void ExecuteDeletePhylum(string searchName)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            //_extDelete.DeletePhylum(CurrentTbl06Phylum);

            //Tbl06PhylumsList = _extCrud.GetPhylumsCollectionFromRegnumIdOrderBy<Tbl06Phylum>(CurrentTbl06Phylum.RegnumId);
            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.MoveCurrentToFirst();
        }

        private void ExecuteSavePhylum(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl06Phylum)) return;

            //CurrentTbl06Phylum.RegnumId = CurrentTbl03Regnum.RegnumId;

            //_extSave.SavePhylum(CurrentTbl06Phylum);
            //Tbl06PhylumsList = _extCrud.GetPhylumsCollectionFromRegnumIdOrderBy<Tbl06Phylum>(CurrentTbl06Phylum.RegnumId);

            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl06Phylum]                                                                                                                                            

        //    Part 5    

        #region [Public Commands Connect ==> Tbl09Division]                

        private RelayCommand _addDivisionCommand;

        public ICommand AddDivisionCommand => _addDivisionCommand ??= new RelayCommand(delegate { ExecuteAddDivision(null); });

        private RelayCommand _copyDivisionCommand;

        public ICommand CopyDivisionCommand => _copyDivisionCommand ??= new RelayCommand(delegate { ExecuteCopyDivision(null); });

        private RelayCommand _deleteDivisionCommand;

        public ICommand DeleteDivisionCommand => _deleteDivisionCommand ??= new RelayCommand(delegate { ExecuteDeleteDivision(null); });

        private RelayCommand _saveDivisionCommand;

        public ICommand SaveDivisionCommand => _saveDivisionCommand ??= new RelayCommand(delegate { ExecuteSaveDivision(null); });

        #endregion [Public Commands Connect ==> Tbl09Division]                

        #region [Public Methods Connect ==> Tbl09Division]                        

        private void ExecuteAddDivision(object o)
        {
            //Tbl09DivisionsList ??= new ObservableCollection<Tbl09Division>();
            //Tbl09DivisionsList.Insert(0, new Tbl09Division { DivisionName = CultRes.StringsRes.DatasetNew });

            //Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            //DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            //DivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyDivision(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            //Tbl09DivisionsList = _extCrud.CopyDivision(CurrentTbl09Division);

            //// evtl verbundene tabellen-Datensätze auch kopieren Expert, Source, Author und Comment

            //DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            //DivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteDivision(string searchName)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            //_extDelete.DeleteDivision(CurrentTbl09Division);

            //Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromRegnumIdOrderBy<Tbl09Division>(CurrentTbl09Division.RegnumId);

            //DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            //DivisionsView.MoveCurrentToFirst();
        }

        private void ExecuteSaveDivision(string searchName)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl09Division)) return;

            //CurrentTbl09Division.RegnumId = CurrentTbl03Regnum.RegnumId;

            //_extSave.SaveDivision(CurrentTbl09Division);


            //Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromRegnumIdOrderBy<Tbl09Division>(CurrentTbl09Division.RegnumId);

            //SelectedMainTabIndex = 2;
            //DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
            //DivisionsView.MoveCurrentToFirst();
        }

        #endregion [Public Methods  Connect ==> Tbl09Division]                                                                                                                                                  

        //    Part 6    

        //    Part 7    

        //    Part 8    

        #region [Commands Regnum ==> Tbl90Reference Author]

        private RelayCommand _addReferenceAuthorCommand;

        public ICommand AddReferenceAuthorCommand => _addReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteAddReferenceAuthor(null); });

        private RelayCommand _copyReferenceAuthorCommand;

        public ICommand CopyReferenceAuthorCommand => _copyReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceAuthor(null); });

        private RelayCommand _deleteReferenceAuthorCommand;

        public ICommand DeleteReferenceAuthorCommand => _deleteReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceAuthor(null); });

        private RelayCommand _saveReferenceAuthorCommand;

        public ICommand SaveReferenceAuthorCommand => _saveReferenceAuthorCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceAuthor(null); });

        #endregion [Commands Regnum ==> Tbl90Reference Author]                

        #region [Methods Regnum ==> Tbl90Reference Author]

        public void ExecuteAddReferenceAuthor(object o)
        {
            //Tbl90ReferenceAuthorsList ??= new ObservableCollection<Tbl90Reference>();

            //Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");
            //Tbl90ReferenceAuthorsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            //ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            //ReferenceAuthorsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceAuthor(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            //Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceRegnum(CurrentTbl90ReferenceAuthor, "Author");

            //ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            //ReferenceAuthorsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceAuthor(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            //_extDelete.DeleteReferenceAuthor(CurrentTbl90ReferenceAuthor);

            //Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl90ReferenceAuthor.RegnumId);
            //ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            //ReferenceAuthorsView.Refresh();
        }

        public void ExecuteSaveReferenceAuthor(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceAuthor)) return;

            //CurrentTbl90ReferenceAuthor.RegnumId = CurrentTbl03Regnum.RegnumId;

            //_extSave.SaveReferenceAuthor(CurrentTbl90ReferenceAuthor, "Regnum");

            //Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

            //ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
            //ReferenceAuthorsView.Refresh();
        }

        #endregion [Methods Regnum ==> Tbl90Reference Author]              

        #region [Commands Regnum ==> Tbl90Reference Source]      

        private RelayCommand _addReferenceSourceCommand;

        public ICommand AddReferenceSourceCommand => _addReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteAddReferenceSource(null); });

        private RelayCommand _copyReferenceSourceCommand;

        public ICommand CopyReferenceSourceCommand => _copyReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceSource(null); });

        private RelayCommand _deleteReferenceSourceCommand;

        public ICommand DeleteReferenceSourceCommand => _deleteReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceSource(null); });

        private RelayCommand _saveReferenceSourceCommand;

        public ICommand SaveReferenceSourceCommand => _saveReferenceSourceCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceSource(null); });


        #endregion [Commands Regnum ==> Tbl90Reference Source]         

        #region [Methods Regnum ==> Tbl90Reference Source]      

        public void ExecuteAddReferenceSource(object o)
        {
            //Tbl90ReferenceSourcesList ??= new ObservableCollection<Tbl90Reference>();

            //Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

            //Tbl90ReferenceSourcesList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            //ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceSource(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //Tbl90ReferenceAuthorsList = _extCrud.CopyReferenceRegnum(CurrentTbl90ReferenceSource, "Source");

            //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            //ReferenceSourcesView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceSource(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //_extDelete.DeleteReferenceSource(CurrentTbl90ReferenceSource);

            //Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);
            //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            //ReferenceSourcesView.MoveCurrentToFirst();
        }

        public void ExecuteSaveReferenceSource(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceSource)) return;

            //CurrentTbl90ReferenceSource.RegnumId = CurrentTbl03Regnum.RegnumId;

            //_extSave.SaveReferenceSource(CurrentTbl90ReferenceSource, "Regnum");

            //Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);


            //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
            //ReferenceSourcesView.MoveCurrentToFirst();
        }

        #endregion [Methods Regnum ==> Tbl90Reference Source]                    

        #region [Commands Regnum ==> Tbl90Reference Expert]                 

        private RelayCommand _addReferenceExpertCommand;

        public ICommand AddReferenceExpertCommand => _addReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteAddReferenceExpert(null); });

        private RelayCommand _copyReferenceExpertCommand;

        public ICommand CopyReferenceExpertCommand => _copyReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteCopyReferenceExpert(null); });

        private RelayCommand _deleteReferenceExpertCommand;

        public ICommand DeleteReferenceExpertCommand => _deleteReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteDeleteReferenceExpert(null); });
        private RelayCommand _saveReferenceExpertCommand;

        public ICommand SaveReferenceExpertCommand => _saveReferenceExpertCommand ??= new RelayCommand(delegate { ExecuteSaveReferenceExpert(null); });

        #endregion [Commands Regnum ==> Tbl90Reference Expert]                    

        #region [Methods Regnum ==> Tbl90Reference Expert]                 

        public void ExecuteAddReferenceExpert(object o)
        {
            //Tbl90ReferenceExpertsList ??= new ObservableCollection<Tbl90Reference>();

            //Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");
            //Tbl90ReferenceExpertsList.Insert(0, new Tbl90Reference { Info = CultRes.StringsRes.DatasetNew });

            //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            //ReferenceExpertsView.MoveCurrentToFirst();
        }

        public void ExecuteCopyReferenceExpert(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //Tbl90ReferenceExpertsList = _extCrud.CopyReferenceRegnum(CurrentTbl90ReferenceExpert, "Expert");

            //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            //ReferenceExpertsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteReferenceExpert(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //_extDelete.DeleteReferenceExpert(CurrentTbl90ReferenceExpert);

            //Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);
            //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            //ReferenceExpertsView.Refresh();
        }

        public void ExecuteSaveReferenceExpert(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl90ReferenceExpert)) return;

            //CurrentTbl90ReferenceExpert.RegnumId = CurrentTbl03Regnum.RegnumId;

            //_extSave.SaveReferenceExpert(CurrentTbl90ReferenceExpert, "Regnum");

            //Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);


            //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
            //ReferenceExpertsView.MoveCurrentToFirst();
        }

        #endregion [Methods Regnum ==> Tbl90Reference Expert]                               

        #region [Commands Regnum ==> Tbl93Comments]        

        private RelayCommand _addCommentCommand;

        public ICommand AddCommentCommand => _addCommentCommand ??= new RelayCommand(delegate { ExecuteAddComment(null); });

        private RelayCommand _copyCommentCommand;

        public ICommand CopyCommentCommand => _copyCommentCommand ??= new RelayCommand(delegate { ExecuteCopyComment(null); });

        private RelayCommand _deleteCommentCommand;

        public ICommand DeleteCommentCommand => _deleteCommentCommand ??= new RelayCommand(delegate { ExecuteDeleteComment(null); });

        private RelayCommand _saveCommentCommand;

        public ICommand SaveCommentCommand => _saveCommentCommand ??= new RelayCommand(delegate { ExecuteSaveComment(null); });

        #endregion [Commands Regnum ==> Tbl93Comments]

        #region [Methods Regnum ==> Tbl93Comments]        

        private void ExecuteAddComment(object o)
        {
            //Tbl93CommentsList ??= new ObservableCollection<Tbl93Comment>();

            //Tbl93CommentsList.Insert(0, new Tbl93Comment { Info = CultRes.StringsRes.DatasetNew });

            //CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            //CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteCopyComment(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            //Tbl93CommentsList = _extCrud.CopyComment(CurrentTbl93Comment, "Comment");

            //CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            //CommentsView.MoveCurrentToFirst();
        }

        private void ExecuteDeleteComment(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            //_extDelete.DeleteComment(CurrentTbl93Comment);

            //Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRegnumIdOrderBy<Tbl93Comment>(CurrentTbl03Regnum.RegnumId);

            //CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            //CommentsView.Refresh();
        }

        private void ExecuteSaveComment(object o)
        {
            //if (_allMessageBoxes.NoDatasetSelectedInfoMessageBox(CurrentTbl93Comment)) return;

            //CurrentTbl93Comment.RegnumId = CurrentTbl03Regnum.RegnumId;

            //_extSave.SaveComment(CurrentTbl93Comment, "Regnum");

            //Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRegnumIdOrderBy<Tbl93Comment>(CurrentTbl03Regnum.RegnumId);


            //CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
            //CommentsView.MoveCurrentToFirst();
        }

        #endregion [Methods Regnum ==> Tbl93Comments]                 


        //    Part 9    



        #region "Public Commands Connected Tables by DoubleClick"

        private RelayCommand _getConnectedTablesCommand;
        public ICommand GetConnectedTablesCommand => _getConnectedTablesCommand ??= new RelayCommand(delegate { GetConnectedTablesById(null); });

        #endregion "Public Commands Connected Tables by DoubleClick"

        #region "Public Method Connected Tables by DoubleClick"

        private void GetConnectedTablesById(object o)
        {


            if (Tbl03RegnumsAllList == null)
                Tbl03RegnumsAllList ??= new ObservableCollection<Tbl03Regnum>();
            else
                Tbl03RegnumsAllList.Clear();

            //Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

            //Tbl06PhylumsList = _extCrud.GetPhylumsCollectionFromRegnumIdOrderBy<Tbl06Phylum>(CurrentTbl03Regnum.RegnumId);


            //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
            //PhylumsView.Refresh();

            SelectedMainTabIndex = 0;
            SelectedDetailTabIndex = 1;

        }

        #endregion "Public Method Connected Tables by DoubleClick"     



        //    Part 10    


        #region "Public Commands to open Detail TabItems"

        private int _selectedMainTabIndex;
        private int _selectedMainSubRefTabIndex;
        private int _selectedDetailTabIndex;
        private int _selectedDetailSubRefTabIndex;

        public int SelectedMainTabIndex
        {
            get => _selectedMainTabIndex;
            set
            {
                if (value == _selectedMainTabIndex) return;
                _selectedMainTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainTabIndex == 0)   //Phylums
                {
                    //if (SelectedRegnum != null)
                    //{
                    // //   Tbl06PhylumsList = _extCrud.GetPhylumsCollectionFromRegnumIdOrderBy<Tbl06Phylum>(CurrentTbl03Regnum.RegnumId);

                    //Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_context.Tbl06Phylums
                    //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                    //    .OrderBy(k => k.PhylumName));

                    ////    Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

                    //    Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_context.Tbl03Regnums
                    //        .OrderBy(a => a.RegnumName)
                    //        .ThenBy(a => a.Subregnum));

                    //    //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                    //    //PhylumsView.Refresh();
                    //}
                         //    SelectedDetailTabIndex = 1;
                }

                if (_selectedMainTabIndex == 1)   //Divisions
                {
                    if (SelectedRegnum != null)
                    {
                        //Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromRegnumIdOrderBy<Tbl09Division>(CurrentTbl03Regnum.RegnumId);
                        //Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_context.Tbl09Divisions
                        //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                        //    .OrderBy(k => k.DivisionName));

                        //Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

                        //DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        //DivisionsView.Refresh();
                    }
                    SelectedDetailTabIndex = 2;
                }

                if (_selectedMainTabIndex == 2)  //References
                {
                    SelectedDetailTabIndex = 3;  //Reference
                    SelectedDetailSubRefTabIndex = 0;  //RefExpert
                }

                if (_selectedMainTabIndex == 3)   //Comments
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl93CommentsList = _extCrud.GetCommentsCollectionFromRegnumIdOrderBy<Tbl93Comment>(CurrentTbl03Regnum.RegnumId);

                        //CommentsView = CollectionViewSource.GetDefaultView(Tbl93CommentsList);
                        //CommentsView.Refresh();
                    }
                    SelectedDetailTabIndex = 4;
                }

            }
        }

        public int SelectedDetailTabIndex
        {
            get => _selectedDetailTabIndex;
            set
            {
                if (value == _selectedDetailTabIndex) return;
                _selectedDetailTabIndex = value; RaisePropertyChanged("");

                if (_selectedDetailTabIndex == 0)
                {
                   // SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 1)  //Phylum
                {
                     //if (SelectedRegnum != null)
                    //{

                    //    //   Tbl06PhylumsList = _extCrud.GetPhylumsCollectionFromRegnumIdOrderBy<Tbl06Phylum>(CurrentTbl03Regnum.RegnumId);

                    //Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_context
                    //    .Tbl06Phylums
                    //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                    //    .OrderBy(k => k.PhylumName));

                    //    //    Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

                    //    Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>) _context
                    //        .Tbl03Regnums
                    //        .OrderBy(a => a.RegnumName)
                    //        .ThenBy(a => a.Subregnum));
                    //}
                         //      SelectedMainTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 2)  //Division
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl09DivisionsList = _extCrud.GetDivisionsCollectionFromRegnumIdOrderBy<Tbl09Division>(CurrentTbl03Regnum.RegnumId);

                        //Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_context.Tbl09Divisions
                        //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                        //    .OrderBy(k => k.DivisionName));

                        //Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

                        //DivisionsView = CollectionViewSource.GetDefaultView(Tbl09DivisionsList);
                        //DivisionsView.Refresh();
                    }
                    SelectedMainTabIndex = 1;
                }

                if (_selectedDetailTabIndex == 3)  //Reference
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        //Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        //ReferenceExpertsView.Refresh();
                    }
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                    SelectedDetailSubRefTabIndex = 0;
                }

                if (_selectedDetailTabIndex == 4) //Comment
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        //Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        //ReferenceSourcesView.Refresh();
                    }
                    SelectedMainTabIndex = 3;
                }
            }
        }

        public int SelectedMainSubRefTabIndex
        {
            get => _selectedMainSubRefTabIndex;
            set
            {
                if (value == _selectedMainSubRefTabIndex) return;
                _selectedMainSubRefTabIndex = value; RaisePropertyChanged("");

                if (_selectedMainSubRefTabIndex == 0)  //Experts
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        //Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        //ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedDetailSubRefTabIndex = 0;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 1) //RefSources
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        //Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        //ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedDetailSubRefTabIndex = 1;
                    SelectedMainTabIndex = 2;
                }

                if (_selectedMainSubRefTabIndex == 2)  //RefAuthors
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        //Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        //ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedDetailSubRefTabIndex = 2;
                    SelectedMainTabIndex = 2;
                }

            }
        }

        public int SelectedDetailSubRefTabIndex
        {
            get => _selectedDetailSubRefTabIndex;
            set
            {
                if (value == _selectedDetailSubRefTabIndex) return;
                _selectedDetailSubRefTabIndex = value; RaisePropertyChanged("");

                if (_selectedDetailSubRefTabIndex == 0)   //RefExpert
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90ExpertsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefExpert>("Expert");

                        //Tbl90ReferenceExpertsList = _extCrud.GetReferenceExpertsCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefSourceIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceExpertsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceExpertsList);
                        //ReferenceExpertsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 0;
                }

                if (_selectedDetailSubRefTabIndex == 1)  //RefSource
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90SourcesAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefSource>("Source");

                        //Tbl90ReferenceSourcesList = _extCrud.GetReferenceSourcesCollectionFromRegnumIdAndRefAuthorIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceSourcesView = CollectionViewSource.GetDefaultView(Tbl90ReferenceSourcesList);
                        //ReferenceSourcesView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 1;
                }

                if (_selectedDetailSubRefTabIndex == 2)  //RefAuthor
                {
                    if (CurrentTbl03Regnum != null)
                    {
                        //Tbl90AuthorsAllList = _extCrud.GetCollectionAllOrderBy<Tbl90RefAuthor>("Author");

                        //Tbl90ReferenceAuthorsList = _extCrud.GetReferenceAuthorsCollectionFromRegnumIdAndRefSourceIdIsNullAndRefExpertIdIsNullOrderBy<Tbl90Reference>(CurrentTbl03Regnum.RegnumId);

                        //ReferenceAuthorsView = CollectionViewSource.GetDefaultView(Tbl90ReferenceAuthorsList);
                        //ReferenceAuthorsView.Refresh();
                    }
                    SelectedDetailTabIndex = 3;
                    SelectedMainTabIndex = 2;
                    SelectedMainSubRefTabIndex = 2;
                }

            }
        }

        #endregion "Public Commands to open Detail TabItems"          


        //    Part 11    


        #region "Public Properties Tbl03Regnum"

        //private Tbl03Regnum _selectedRegnum;
        //public Tbl03Regnum SelectedRegnum
        //{
        //    get => _selectedRegnum;
        //    set { _selectedRegnum = value; RaisePropertyChanged(nameof(SelectedRegnum)); }
        //}

        private Tbl03Regnum _selectedRegnum;
        public Tbl03Regnum SelectedRegnum
        {
            get => _selectedRegnum;
            //    set { _selectedRegnum = value; RaisePropertyChanged(nameof(SelectedRegnum)); }
            set
            {
                if (value == _selectedRegnum) return;
                _selectedRegnum = value; RaisePropertyChanged(nameof(SelectedRegnum));

                //    //if (Tbl06PhylumsList != null)
                //    //{
                //    //    Tbl06PhylumsList.Clear();
                //    //}
                Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_context
                    .Tbl06Phylums
                    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                    .OrderBy(k => k.PhylumName));
                //    //if (Tbl09DivisionsList != null)
                //    //{
                //    //    Tbl09DivisionsList.Clear();
                //    //}
                Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_context
                    .Tbl09Divisions
                    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                    .OrderBy(k => k.DivisionName));

                Tbl90ReferenceExpertsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                    .Include(a => a.Tbl90RefExperts)
                    .Where(e => e.RegnumId == SelectedRegnum.RegnumId && e.RefAuthorId == null && e.RefSourceId == null)
                    .OrderBy(k => k.Info));

                Tbl90ReferenceSourcesList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                    .Include(a => a.Tbl90RefSources)
                    .Where(e => e.RegnumId == SelectedRegnum.RegnumId && e.RefAuthorId == null && e.RefExpertId == null)
                    .OrderBy(k => k.Info));

                Tbl90ReferenceAuthorsList = new ObservableCollection<Tbl90Reference>(_context.Tbl90References
                    .Include(a => a.Tbl90RefAuthors)
                    .Where(e => e.RegnumId == SelectedRegnum.RegnumId && e.RefSourceId == null && e.RefExpertId == null)
                    .OrderBy(k => k.Info));

                Tbl93CommentsList = new ObservableCollection<Tbl93Comment>(_context.Tbl93Comments
                    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                    .OrderBy(e => e.Info));

                //    SelectedMainTabIndex = 0; //Phylums
                //     SelectedDetailTabIndex = 0;  //Regnum

                //}


                //set
                //{
                //    if (value == _selectedSelectedRegnum) return;
                //    _selectedSelectedRegnum = value; RaisePropertyChanged(nameof(SelectedRegnum));

                //    if (SelectedRegnum != null)
                //    {
                //        //Tbl03RegnumsList = int.TryParse(searchName, out var id)
                //        //    ? new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_uow.Tbl03Regnums
                //        //        .Find(e => e.RegnumId == id))
                //        //    : new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_uow.Tbl03Regnums
                //        //        .Find(e => e.RegnumName.StartsWith(searchName))
                //        //        .OrderBy(a => a.RegnumName)
                //        //        .ThenBy(a => a.Subregnum)
                //        //    );


                //        //Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_context
                //        //    .Tbl03Regnums
                //        //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //        //    .OrderBy(k => k.RegnumName));

                //        if (SelectedPhylum == null)
                //        {
                //            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_context
                //                .Tbl06Phylums
                //                .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //                .OrderBy(k => k.PhylumName));
                //        }
                //        if (SelectedPhylum != null && SelectedRegnum.RegnumId != SelectedPhylum.RegnumId)
                //        {
                //            Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_context
                //                .Tbl06Phylums
                //                .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //                .OrderBy(k => k.PhylumName));
                //        }

                //        //if (SelectedPhylum == null  )
                //        //{
                //        //    Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_context
                //        //        .Tbl06Phylums
                //        //        .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //        //        .OrderBy(k => k.PhylumName));
                //        //}
                //        //else
                //        //{
                //        //    Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_context
                //        //        .Tbl06Phylums
                //        //        .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //        //        .OrderBy(k => k.PhylumName));

                //        //}
                //        //if (  SelectedRegnum.RegnumId == SelectedPhylum.RegnumId)
                //        //{
                //        //    Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_context
                //        //        .Tbl06Phylums
                //        //        .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //        //        .OrderBy(k => k.PhylumName));
                //        //}
                //        ////   Tbl06PhylumsList = _extCrud.GetPhylumsCollectionFromRegnumIdOrderBy<Tbl06Phylum>(CurrentTbl03Regnum.RegnumId);
                //        //else
                //        //{
                //        //    Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>((IEnumerable<Tbl06Phylum>)_context
                //        //        .Tbl06Phylums
                //        //        .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //        //        .OrderBy(k => k.PhylumName));

                //        ////    SelectedRegnum.RegnumId = SelectedPhylum.RegnumId;

                //        //}

                //        Tbl09DivisionsList = new ObservableCollection<Tbl09Division>((IEnumerable<Tbl09Division>)_context
                //            .Tbl09Divisions
                //            .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //            .OrderBy(k => k.DivisionName));

                //        //    Tbl03RegnumsAllList = _extCrud.GetCollectionAllOrderBy<Tbl03Regnum>("Regnum");

                //        Tbl03RegnumsAllList = new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_context
                //            .Tbl03Regnums
                //            .OrderBy(a => a.RegnumName)
                //            .ThenBy(a => a.Subregnum));

                //        //PhylumsView = CollectionViewSource.GetDefaultView(Tbl06PhylumsList);
                //        //PhylumsView.Refresh();
                //        SelectedMainTabIndex = 0; //Phylums
                //        SelectedDetailTabIndex = 0; //Regnum
                //    }

                //    //if (SelectedMainTabIndex == 0) //Phylums
                //    //{
                //    //    SelectedDetailTabIndex = 0;  //Regnum
                //    //}
                //    //if (SelectedMainTabIndex == 1) //Divisions
                //    //{
                //    //    SelectedDetailTabIndex = 2;  //Regnum
                //    //}
                //    //if (SelectedMainTabIndex == 2) //References
                //    //{
                //    //    SelectedDetailTabIndex = 3;  //Reference
                //    //    SelectedDetailSubRefTabIndex = 0;  //RefExpert
                //    //}

                //    //if (SelectedDetailTabIndex == 0) //Regnum
                //    //{
                //    //    SelectedMainTabIndex = 0;  //Phylums
                //    //}
                //    //if (SelectedDetailTabIndex == 1) //Phylum
                //    //{
                //    //    SelectedMainTabIndex = 0;  //Phylums
                //    //}
                //        //if (SelectedDetailTabIndex == 2) //Division
                //    //{
                //    //    SelectedMainTabIndex = 1;  //Divisions
                //    //}
                //    //if (SelectedDetailTabIndex == 3) //Reference
                //    //{
                //    //    SelectedMainTabIndex = 2;  //Reference
                //    //    SelectedMainSubRefTabIndex = 0; //RefExpert
                //    //}


                //}
            }
        }


        private string _searchRegnumName = "";
        public string SearchRegnumName
        {
            get => _searchRegnumName;
            set { _searchRegnumName = value; RaisePropertyChanged(""); }
        }

        public ICollectionView RegnumsView;
        public Tbl03Regnum CurrentTbl03Regnum => RegnumsView?.CurrentItem as Tbl03Regnum;

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

        #endregion "Public Properties Tbl03Regnum"

        #region "Public Properties Tbl06Phylum"

        private Tbl06Phylum _selectedPhylum;

        public Tbl06Phylum SelectedPhylum
        {
            get => _selectedPhylum;
            //   set { _selectedSelectedPhylum = value; RaisePropertyChanged(nameof(SelectedPhylum)); }
            set
            {
                if (value == _selectedPhylum) return;
                _selectedPhylum = value; RaisePropertyChanged(nameof(SelectedPhylum));

                //    //if (Tbl06PhylumsList != null)
                //    //{
                //    //    Tbl06PhylumsList.Clear();
                //    //}
                //    if (SelectedPhylum != null)
                //    {
                //Tbl06PhylumsList = new ObservableCollection<Tbl06Phylum>(_context
                //.Tbl06Phylums
                //.Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //.OrderBy(k => k.PhylumName));
                //    }
                //    //Tbl03RegnumsList = new ObservableCollection<Tbl03Regnum>(_context
                //    //    .Tbl03Regnums
                //    //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //    //    .OrderBy(k => k.RegnumName));

                //    SelectedMainTabIndex = 0; //Phylums
                //    SelectedDetailTabIndex = 1;  //Phylum


                }

        }

        public ICollectionView PhylumsView;
        private Tbl06Phylum CurrentTbl06Phylum => PhylumsView?.CurrentItem as Tbl06Phylum;

        private ObservableCollection<Tbl06Phylum> _tbl06PhylumsList;
        public ObservableCollection<Tbl06Phylum> Tbl06PhylumsList
        {
            get => _tbl06PhylumsList;
            set { _tbl06PhylumsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl09Division"

        private Tbl09Division _selectedSelectedDivision;
        public Tbl09Division SelectedDivision
        {
            get => _selectedSelectedDivision;
          //  set { _selectedSelectedDivision = value; RaisePropertyChanged(nameof(SelectedDivision)); }

            set
            {
                if (value == _selectedSelectedDivision) return;
                _selectedSelectedDivision = value; RaisePropertyChanged(nameof(SelectedDivision));


                //Tbl09DivisionsList = new ObservableCollection<Tbl09Division>(_context
                //    .Tbl09Divisions
                //    .Where(e => e.RegnumId == SelectedRegnum.RegnumId)
                //    .OrderBy(k => k.DivisionName));

          //      SelectedMainTabIndex = 1; //Divisions
          //      SelectedDetailTabIndex = 2;  //Division

            }



        }

    public ICollectionView DivisionsView;
        private Tbl09Division CurrentTbl09Division => DivisionsView?.CurrentItem as Tbl09Division;

        private ObservableCollection<Tbl09Division> _tbl09DivisionsList;
        public ObservableCollection<Tbl09Division> Tbl09DivisionsList
        {
            get => _tbl09DivisionsList;
            set { _tbl09DivisionsList = value; RaisePropertyChanged(""); }
        }
        #endregion "Public Properties"     

        #region "Public Properties Tbl12Subphylum"

        public ICollectionView SubphylumsView;
        private Tbl12Subphylum CurrentTbl12Subphylum => SubphylumsView?.CurrentItem as Tbl12Subphylum;

        private ObservableCollection<Tbl12Subphylum> _tbl12SubphylumsList;
        public ObservableCollection<Tbl12Subphylum> Tbl12SubphylumsList
        {
            get => _tbl12SubphylumsList;
            set { _tbl12SubphylumsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     

        #region "Public Properties Tbl15Subdivision"

        private ObservableCollection<Tbl15Subdivision> _tbl15SubdivisionsList;
        public ObservableCollection<Tbl15Subdivision> Tbl15SubdivisionsList
        {
            get => _tbl15SubdivisionsList;
            set { _tbl15SubdivisionsList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties"     

        #region Public Properties Tbl90References

        private ObservableCollection<Tbl90Reference> _tbl90ReferencesList;

        public ObservableCollection<Tbl90Reference> Tbl90ReferencesList
        {
            get => _tbl90ReferencesList;
            set { _tbl90ReferencesList = value; RaisePropertyChanged(""); }
        }

        #endregion

        #region "Public Properties Tbl90Author"

        private ObservableCollection<Tbl90RefAuthor> _tbl90AuthorsAllList;
        public ObservableCollection<Tbl90RefAuthor> Tbl90AuthorsAllList
        {
            get => _tbl90AuthorsAllList;
            set { _tbl90AuthorsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Source"

        private ObservableCollection<Tbl90RefSource> _tbl90SourcesAllList;
        public ObservableCollection<Tbl90RefSource> Tbl90SourcesAllList
        {
            get => _tbl90SourcesAllList;
            set { _tbl90SourcesAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90Expert"

        private ObservableCollection<Tbl90RefExpert> _tbl90ExpertsAllList;
        public ObservableCollection<Tbl90RefExpert> Tbl90ExpertsAllList
        {
            get => _tbl90ExpertsAllList;
            set { _tbl90ExpertsAllList = value; RaisePropertyChanged(""); }
        }

        #endregion "Public Properties "

        #region "Public Properties Tbl90ReferenceAuthor"

        public ICollectionView ReferenceAuthorsView;
        private Tbl90Reference CurrentTbl90ReferenceAuthor => ReferenceAuthorsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceAuthorsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceAuthorsList
        {
            get => _tbl90ReferenceAuthorsList;
            set { _tbl90ReferenceAuthorsList = value; RaisePropertyChanged(""); }
        }

        private Tbl90Reference _selectedRefAuthor;
        public Tbl90Reference SelectedRefAuthor
        {
            get => _selectedRefAuthor;
            set { _selectedRefAuthor = value; RaisePropertyChanged(nameof(SelectedRefAuthor)); }
        }

        
        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceSource"

        public ICollectionView ReferenceSourcesView;
        private Tbl90Reference CurrentTbl90ReferenceSource => ReferenceSourcesView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceSourcesList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceSourcesList
        {
            get => _tbl90ReferenceSourcesList;
            set { _tbl90ReferenceSourcesList = value; RaisePropertyChanged(""); }
        }
        private Tbl90Reference _selectedRefSource;
        public Tbl90Reference SelectedRefSource
        {
            get => _selectedRefSource;
            set { _selectedRefSource = value; RaisePropertyChanged(nameof(SelectedRefSource)); }
        }

        #endregion "Public Properties"

        #region "Public Properties Tbl90ReferenceExpert"

        public ICollectionView ReferenceExpertsView;
        private Tbl90Reference CurrentTbl90ReferenceExpert => ReferenceExpertsView?.CurrentItem as Tbl90Reference;

        private ObservableCollection<Tbl90Reference> _tbl90ReferenceExpertsList;
        public ObservableCollection<Tbl90Reference> Tbl90ReferenceExpertsList
        {
            get => _tbl90ReferenceExpertsList;
            set { _tbl90ReferenceExpertsList = value; RaisePropertyChanged(""); }
        }

        private Tbl90Reference _selectedRefExpert;
        public Tbl90Reference SelectedRefExpert
        {
            get => _selectedRefExpert;
            set { _selectedRefExpert = value; RaisePropertyChanged(nameof(SelectedRefExpert)); }
        }

        #endregion "Public Properties"   

        #region "Public Properties Tbl93Comment"

        public ICollectionView CommentsView;
        private Tbl93Comment CurrentTbl93Comment => CommentsView?.CurrentItem as Tbl93Comment;

        private ObservableCollection<Tbl93Comment> _tbl93CommentsList;
        public ObservableCollection<Tbl93Comment> Tbl93CommentsList
        {
            get => _tbl93CommentsList;
            set { _tbl93CommentsList = value; RaisePropertyChanged(""); }
        }

        private Tbl93Comment _selectedComment;
        public Tbl93Comment SelectedComment
        {
            get => _selectedComment;
            set { _selectedComment = value; RaisePropertyChanged(nameof(SelectedComment)); }
        }

        #endregion "Public Properties"     


        #region other Props
        public int RegnumId { get; set; }
        private string _regnumName = string.Empty;

        //public string RegnumName { get; set; }
        [Required]
        public string RegnumName
        {
            get => _regnumName;
            set
            {
                _regnumName = value;
                RaisePropertyChanged("RegnumName");
            }
        }

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
