using ProductManagmentAdminPanel.Helpers;
using ProductManagmentAdminPanel.Services;
using ProductManagmentAdminPanel.Views;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductManagmentAdminPanel.ViewModels;

class MainViewModel : BaseViewModel
    {
	private Page currentPage;
	public Page CurrentPage { get => currentPage; set { currentPage=value; OnPropertyChanged(nameof(CurrentPage)); } }

	private readonly INavigationService navigationService;
	public MainViewModel(INavigationService navigationService)
	{
		HomeCommand=new(home);
		ProductCommand=new(product);
		CategoryCommand=new(category);
		AllUserCommand=new(alluser);
		this.navigationService=navigationService;

	}
	void home(object obj)
	{
		navigationService.Navigate<HomeView, HomeViewModel>();

	}
	void product(object obj)
	{
		navigationService.Navigate<ProductView, ProductViewModel>();
	}

	void category(object obj)
	{
		navigationService.Navigate<CategoryView, CategoryViewModel>();
	}
	void alluser(object obj)
	{
		
	}
	

        public RelayCommand HomeCommand { get; set; }
        public RelayCommand ProductCommand { get; set; }
        public RelayCommand CategoryCommand { get; set; }
        public RelayCommand AllUserCommand { get; set; }
	public override ICommand OpenPopupCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public override RelayCommand SaveChangesCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public override ICommand ClosePopupCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public override ICommand UpdateEntityCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
	public override ICommand DeleteEntityCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
}
