using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductManagmentUser.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

		private void SwitchToSignUp(object sender, MouseButtonEventArgs e)
		{
			LoginPanel.Visibility = Visibility.Collapsed;
			SignUpPanel.Visibility = Visibility.Visible;
		}

		private void SwitchToLogin(object sender, MouseButtonEventArgs e)
		{
			SignUpPanel.Visibility = Visibility.Collapsed;
			LoginPanel.Visibility = Visibility.Visible;
		}
	}
}
