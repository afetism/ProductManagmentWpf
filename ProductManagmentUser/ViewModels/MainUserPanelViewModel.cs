using MyApp.Data.Models.EntityModel;
using System.Collections.ObjectModel;

namespace ProductManagmentUser.ViewModels;

public class MainUserPanelViewModel:BaseViewModel
{
	private ObservableCollection<Product> _products;

	public ObservableCollection<Product> Products
	{
		get { return _products; }
		set
		{
			_products = value;
			OnPropertyChanged(nameof(Products));
		}
	}

    public MainUserPanelViewModel()
    {
        LoadProducts();
    }
    private void LoadProducts()
	{
		
			Products = new ObservableCollection<Product>(ProductDb.GetAll());
		
	}


}
