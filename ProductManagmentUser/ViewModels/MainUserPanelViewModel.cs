using Microsoft.IdentityModel.Tokens;
using MyApp.Data.Models.EntityModel;
using ProductManagmentUser.Services;
using ProductManagmentUser.Views;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Controls;

namespace ProductManagmentUser.ViewModels;

public class MainUserPanelViewModel:BaseViewModel
{
	private Page currentPage;
	public Page CurrentPage { get => currentPage; set { currentPage=value; OnPropertyChanged(nameof(CurrentPage)); } }
	private ObservableCollection<Product> _products;
	private User currentUser = App.Container.GetInstance<RegisterViewModel>().UserData;
	private string photoPath;
	private readonly INavigationService navigationService;
	public ObservableCollection<Product> Products
	{
		get { return _products; }
		set
		{
			_products = value;
			OnPropertyChanged(nameof(Products));
		}
	}
	public User CurrentUser
		{ 
		get => currentUser; 
		set 
		{
			
				currentUser=value;
				OnPropertyChanged(nameof(CurrentUser));
			
		
		} 
	}
	public string PhotoUs { get; set; }

	public string PhotoPath { get => photoPath; set { photoPath=value; OnPropertyChanged(nameof(photoPath)); } }
	public MainUserPanelViewModel(INavigationService navigationService)
    {

		this.navigationService = navigationService;
        LoadProducts();
    }
    public void LoadProducts()
	{
		   
			Products = new ObservableCollection<Product>(ProductDb.GetAll());
		    CurrentUser=App.Container.GetInstance<RegisterViewModel>().UserData;
		if (CurrentUser is not null && CurrentUser.PhotoUs is null)
		{
			PhotoPath="C:\\Users\\user\\source\\repos\\ProductManagmentWpf\\ProductManagmentUser\\Resources\\no-image-icon-23479.png";

		}
		CurrentPage = (App.Container.GetInstance<AllProductsView>())!;
		CurrentPage.DataContext = App.Container.GetInstance<AllProductsViewModel>();
	}


}
