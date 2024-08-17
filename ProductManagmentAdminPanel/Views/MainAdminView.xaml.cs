using System;
using System.Windows;

namespace ProductManagmentAdminPanel.Views
{
	public partial class MainAdminView : Window
	{
		public MainAdminView()
		{
			InitializeComponent();
		
		}

		private void CloseApp_Click(object sender, RoutedEventArgs e)
		{
			Close();
        }
    }
}
