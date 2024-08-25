using MyApp.Data.Models.EntityModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace ProductManagmentUser.ViewModels;

public class CartViewModel:BaseViewModel
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
	
	
	private Cart cart;
	public Cart Cart { 
		get => cart; 
		set {
			cart=value;
			OnPropertyChanged(nameof(Cart)); 
		} 
	}

	private int currentQuantity;
	public int CurrentQuantity
	{
		get => currentQuantity;
		set
		{

			currentQuantity=value;
			OnPropertyChanged(nameof(CurrentQuantity));
		}
	}

	public CartViewModel()
    {

		LoadCart();




	}
	public void LoadCart()
	{
		Products=new ObservableCollection<Product>();
		var user = App.Container.GetInstance<MainUserPanelViewModel>();


		if (user.CurrentUser is not null)
		{
			var curruntUser = CartDb.Get(e => e.UserId==user.CurrentUser.Id);
			if (curruntUser is null)
			{
				var newCart = new Cart() { UserId=user.CurrentUser.Id, Items = JsonConvert.SerializeObject(Products) };
				CartDb.Add(newCart);
				Cart = newCart;
				CartDb.SaveChanges();
			}
			else
			{
				Products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(curruntUser.Items) ?? new ObservableCollection<Product>();
				Cart = curruntUser;

			}
		}
		// Subscribe to Products CollectionChanged event to update Cart.Items when Products changes
		//Products.CollectionChanged += (s, e) =>
		//{
		//	Cart.Items = JsonConvert.SerializeObject(Products);
		//	CartDb.Update(Cart); // Update the cart in the database whenever Products changes
		//};
	}
	public void AddProduct(Product product)
	{
		Products.Add(product);
		var jsonSettings = new JsonSerializerSettings
		{
			ReferenceLoopHandling = ReferenceLoopHandling.Ignore
		};

		Cart.Items = JsonConvert.SerializeObject(Products, jsonSettings);

		CartDb.Update(Cart);
		CartDb.SaveChanges();
	}

	
	public void RemoveProduct(Product product)
	{
		
		Products.Remove(product);

		
		Cart.Items = JsonConvert.SerializeObject(Products);

		
		CartDb.Update(Cart);
	}




}
