using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ATIS.WinUi.Views
{
    public sealed partial class LogInView : UserControl
    {
        public LogInView()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //StringBuilder displayText = new StringBuilder("Hello, ");
            //displayText.AppendFormat("{0} {1}.", firstName.Text, lastName.Text);
            //result.Text = displayText.ToString();
        }

    }
}
