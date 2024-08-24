using ProductManagmentUser.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ProductManagmentUser.Views;


public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
		DataContext=App.Container.GetInstance<RegisterViewModel>();
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

	private void CloseApp_Click(object sender, RoutedEventArgs e)
	{
		Environment.Exit(0);
	}
}
