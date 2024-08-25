using MyApp.Data.Models.EntityModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		public AllProductsViewModel()
        {
			Products = new ObservableCollection<Product>(ProductDb.GetAll());
		}
    }
}
