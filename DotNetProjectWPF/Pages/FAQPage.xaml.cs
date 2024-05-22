using System.Windows.Controls;

namespace DotNetProjectWPF.Pages
{
    public partial class FAQPage : Page
    {
        Frame MainFrame;
        public FAQPage(Frame mainFrame)
        {
            MainFrame = mainFrame;

            InitializeComponent();
        }
    }
}
