using System.Windows.Controls;
using System.Windows;

namespace ProductManagmentUser.Views
{
	public static class PasswordHelper
	{
		public static readonly DependencyProperty BoundPassword =
			DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordHelper), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

		public static string GetBoundPassword(DependencyObject dp)
		{
			return (string)dp.GetValue(BoundPassword);
		}

		public static void SetBoundPassword(DependencyObject dp, string value)
		{
			dp.SetValue(BoundPassword, value);
		}

		private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var passwordBox = d as PasswordBox;

			if (passwordBox != null)
			{
				passwordBox.PasswordChanged -= PasswordChanged;

				if (!string.Equals(passwordBox.Password, e.NewValue as string))
				{
					passwordBox.Password = e.NewValue as string;
				}

				passwordBox.PasswordChanged += PasswordChanged;
			}
		}

		private static void PasswordChanged(object sender, RoutedEventArgs e)
		{
			var passwordBox = sender as PasswordBox;
			SetBoundPassword(passwordBox, passwordBox.Password);
		}
	}
}