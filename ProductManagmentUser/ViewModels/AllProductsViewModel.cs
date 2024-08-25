using MyApp.Data.Models.EntityModel;
using ProductManagmentAdminPanel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProductManagmentUser.ViewModels
{
	internal class AllProductsViewModel:BaseViewModel
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
       public ICommand AddCartList { get; set; }
        public AllProductsViewModel()
        {
			Products = new ObservableCollection<Product>(ProductDb.GetAll());
			AddCartList=new RelayCommand(executeAddCart);
		}

		private void executeAddCart(object obj)
		{

			var product =obj as Product;
			App.Container.GetInstance<CartViewModel>().LoadCart();
			App.Container.GetInstance<CartViewModel>().AddProduct(product);

			MessageBox.Show("Success!");

		}
	}
}
