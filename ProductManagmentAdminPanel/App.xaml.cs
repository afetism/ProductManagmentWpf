using System.ComponentModel;
using System.Windows;
using ProductManagmentAdminPanel.Services;
using ProductManagmentAdminPanel.ViewModels;
using ProductManagmentAdminPanel.Views;
using SimpleInjector;
namespace ProductManagmentAdminPanel;


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
		Container.RegisterSingleton<ProductViewModel>();
		Container.RegisterSingleton<CategoryViewModel>();

	}

	private void View()
	{
		Container.RegisterSingleton<MainAdminView>();
		Container.RegisterSingleton<ProductView>();
		Container.RegisterSingleton<CategoryView>(); ;

	}
	protected override void OnStartup(StartupEventArgs e)
	{
		var mainView = Container.GetInstance<MainAdminView>();
		mainView.DataContext = Container.GetInstance<MainViewModel>();
		mainView.Show();
		base.OnStartup(e);
	}
}
