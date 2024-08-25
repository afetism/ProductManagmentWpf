using Microsoft.IdentityModel.Tokens;
using MyApp.Data.Models.EntityModel;
using ProductManagmentAdminPanel.Helpers;
using ProductManagmentUser.Services;
using ProductManagmentUser.Views;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProductManagmentUser.ViewModels;

public class MainUserPanelViewModel:BaseViewModel
{
	private Page currentPage;
	public Page CurrentPage { get => currentPage; set { currentPage=value; OnPropertyChanged(nameof(CurrentPage)); } }

	private User currentUser = App.Container.GetInstance<RegisterViewModel>().UserData;
	private string photoPath;
	private ICommand showCart;
	private readonly INavigationService navigationService;

	public User CurrentUser
		{ 
		get => currentUser; 
		set 
		{
			
				currentUser=value;
				OnPropertyChanged(nameof(CurrentUser));
			
		
		} 
	}
	public ICommand ShowCart { get => showCart;
		set { showCart=value; OnPropertyChanged(nameof(showCart)); }   }
	public string PhotoUs { get; set; }

	public string PhotoPath { get => photoPath; set { photoPath=value; OnPropertyChanged(nameof(photoPath)); } }
	public MainUserPanelViewModel(INavigationService navigationService)
    {

		this.navigationService = navigationService;
        LoadProducts();
		ShowCart=new RelayCommand(executeShowCart);

	}

	private void executeShowCart(object obj)
	{

		CurrentPage = (App.Container.GetInstance<CartView>())!;
		CurrentPage.DataContext = App.Container.GetInstance<CartViewModel>();
		App.Container.GetInstance<CartViewModel>().LoadCart();
	}

	public void LoadProducts()
	{
		   
		
		    CurrentUser=App.Container.GetInstance<RegisterViewModel>().UserData;
		if (CurrentUser is not null && CurrentUser.PhotoUs is null)
		{
			PhotoPath="C:\\Users\\user\\source\\repos\\ProductManagmentWpf\\ProductManagmentUser\\Resources\\no-image-icon-23479.png";

		}
		CurrentPage = (App.Container.GetInstance<AllProductsView>())!;
		CurrentPage.DataContext = App.Container.GetInstance<AllProductsViewModel>();
	}


}
