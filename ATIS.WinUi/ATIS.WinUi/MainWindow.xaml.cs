using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using ATIS.WinUi.Views;
using ATIS.WinUi.Views.Pages;
// Add "using" for WinUI controls.
using muxc = Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi
{
    public enum NaviIcon
    {
        Home,
        Fish,
        Plant,
        Account,
        Document,
        Game,
        Music,
        Page,
        Mail,
        Diseases,
        Food,
        Div,
        Search,
        About,
        User,
        Admin,
        Setting,

        None,
    }

    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static readonly IReadOnlyDictionary<NaviIcon, Type> Pages = new Dictionary<NaviIcon, Type>()
        {
            {NaviIcon.Home, typeof(HomePage)},
            {NaviIcon.Fish, typeof(FishPage)},
            {NaviIcon.Plant, typeof(PlantPage)},
            {NaviIcon.Account, typeof(DiseasePage)},
            {NaviIcon.Document, typeof(FoodPage)},
            {NaviIcon.Document, typeof(DivPage)},
            {NaviIcon.Search, typeof(SearchPage)},
            {NaviIcon.Music, typeof(AboutPage)},
            {NaviIcon.Page, typeof(UserPage)},
            {NaviIcon.Mail, typeof(AdminPage)},
            //None
            {NaviIcon.None, typeof(SettingPage)},
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
                    ContentFrame.Navigate(Pages[icon]);
                }
                else
                {
                    ContentFrame.Navigate(Pages[NaviIcon.None]);
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
