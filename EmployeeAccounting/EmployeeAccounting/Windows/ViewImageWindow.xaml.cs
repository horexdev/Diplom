using System.Windows;
using System.Windows.Media;

namespace EmployeeAccounting.Windows
{
    /// <summary>
    /// Interaction logic for ViewImageWindow.xaml
    /// </summary>
    public partial class ViewImageWindow : Window
    {
        public ViewImageWindow(ImageSource image)
        {
            InitializeComponent();

            Image.Source = image;
        }
    }
}
