using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ATIS.WinUi.Core;
using ATIS.WinUi.DataLayer.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Pages.Database
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class D03RegnumsPage : Page
    {
        private readonly UnitOfWork _uow = new UnitOfWork(new AtisDbContext());
        private readonly AtisDbContext _context = new AtisDbContext();

        public D03RegnumsPage()
        {

            InitializeComponent();
            // DataContext = new D03RegnumsViewModel();
        }



        // private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        // {
        ////     RegnumsList.ItemsSource = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());

        //     var list = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());
        // }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           // RegnumsList.ItemsSource = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());

        }

        private void AutoSuggestBox_OnTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason != AutoSuggestionBoxTextChangeReason.UserInput)
                return;
            var test = sender.Text;

            //RegnumsList3.ItemsSource = new ObservableCollection<Tbl03Regnum>(_context.Tbl03Regnums.ToList());
            //RegnumsList3.ItemsSource = int.TryParse(sender.Text, out var id)
            //    ? new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_uow.Tbl03Regnums
            //        .Find(e => e.RegnumId == id))
            //    : new ObservableCollection<Tbl03Regnum>((IEnumerable<Tbl03Regnum>)_uow.Tbl03Regnums
            //        .Find(e => e.RegnumName.StartsWith(sender.Text))
            //        .OrderBy(a => a.RegnumName)
            //        .ThenBy(a => a.Subregnum)
            //    );

        }

    }
}
