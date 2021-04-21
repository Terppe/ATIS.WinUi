using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using ATIS.WinUi.UserControls;
// Add "using" for WinUI controls.
using muxc = Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi
{
    public enum NaviIcon
    {
        Home,
        Account,
        Document,
        Game,
        Music,
        Page,
        Mail,

        None,
    }

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static IReadOnlyDictionary<NaviIcon, Type> _pages = new Dictionary<NaviIcon, Type>()
        {
            {NaviIcon.Home, typeof(Pages.HomePage)},
            {NaviIcon.Account, typeof(Pages.AccountPage)},
            {NaviIcon.Document, typeof(Pages.DocumentPage)},
            {NaviIcon.Game, typeof(Pages.GamePage)},
            {NaviIcon.Music, typeof(Pages.MusicPage)},
            {NaviIcon.Page, typeof(Pages.NotesPage)},
            {NaviIcon.Mail, typeof(Pages.MailPage)},
            //None
            {NaviIcon.None, typeof(Pages.BlankPage)},
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NaviView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            try
            {
                var selectedItem = (NavigationViewItem)args.SelectedItem;

                var iconName = selectedItem.Tag?.ToString();

                sender.Header = iconName;

                if (Enum.TryParse(iconName, out NaviIcon icon))
                {
                    ContentFrame.Navigate(_pages[icon]);
                }
                else
                {
                    ContentFrame.Navigate(_pages[NaviIcon.None]);
                }
            }
            catch (Exception ex)
            {
                //      MessageBox.Show(ex.Message);
            }
        }


        public double NavViewCompactModeThresholdWidth => NaviView.CompactModeThresholdWidth;

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            LogInView log = new LogInView();

            log.InitializeComponent();
            await ContentDialog.ShowAsync();
        }
    }
}
