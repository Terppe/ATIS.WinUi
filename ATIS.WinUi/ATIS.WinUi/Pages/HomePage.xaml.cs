using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

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

    }
}
