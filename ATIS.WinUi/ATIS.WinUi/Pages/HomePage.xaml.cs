using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Foundation.Metadata;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();

            LbVersion.Text = ".NET Core Version: " + Environment.Version + " + OS-Version " + Environment.OSVersion;

        }

        private async void MyButton_Click(object sender, RoutedEventArgs e)
        {
            MyButton.Content = "Clicked";

            var description = new System.Text.StringBuilder();
            var process = System.Diagnostics.Process.GetCurrentProcess();
            foreach (System.Diagnostics.ProcessModule module in process.Modules)
            {
                description.AppendLine(module.FileName);
            }

            CdTextBlock.Text = description.ToString();
            await ContentDialog.ShowAsync();
        }

        public void PrepareConnectedAnimation(ConnectedAnimationConfiguration config)
        {
            var anim = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("ForwardConnectedAnimation", SourceElement);

            if (config != null && ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7))
            {
                anim.Configuration = config;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("BackwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(SourceElement);
            }
        }


    }
}
