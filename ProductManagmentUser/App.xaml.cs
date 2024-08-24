using ProductManagmentAdminPanel.Services;
using ProductManagmentUser.ViewModels;
using ProductManagmentUser.Views;
using System.Windows;

namespace ProductManagmentUser;


public partial class App : Application
{
	public static SimpleInjector.Container Container { get; set; } = new();
	public App()
	{
		View();
		ViewModel();
		AddOtherServices();
	}


	private void AddOtherServices()
	{
		Container.RegisterSingleton<INavigationService, NavigationService>();
	}

	private void ViewModel()
	{
		Container.RegisterSingleton<MainViewModel>();
		Container.RegisterSingleton<RegisterViewModel>();
		Container.RegisterSingleton<MainUserPanelViewModel>();

	}

	private void View()
	{
		
         Container.RegisterSingleton<RegisterWindow>();
		 Container.RegisterSingleton<MainUserPanelView>();
	}
	protected override void OnStartup(StartupEventArgs e)
	{
		var mainView = Container.GetInstance<RegisterWindow>();
		mainView.DataContext = Container.GetInstance<RegisterViewModel>();
		mainView.Show();
		base.OnStartup(e);
	}
}
