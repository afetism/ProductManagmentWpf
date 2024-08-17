

using Microsoft.EntityFrameworkCore;
using MyApp.Data.Models.EntityModel;
using ProductManagmentAdminPanel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProductManagmentAdminPanel.ViewModels;

class ProductViewModel : BaseViewModel
{


	public override ICommand OpenPopupCommand { get; set; }
	public override RelayCommand SaveChangesCommand { get; set; }
	public override ICommand ClosePopupCommand { get; set ; }
	public override ICommand UpdateEntityCommand { get; set; }
	public override ICommand DeleteEntityCommand { get; set; }

	private ObservableCollection<Category> categories=[];
	public ObservableCollection<Category> Categories { 
		get => categories;
		set
		{
			categories=value;
			OnPropertyChanged(nameof(Categories));
		}
    }

	private ObservableCollection<Product> products=[];
	public ObservableCollection<Product> Products {
		get => products;
		set
		{
			products=value;
			OnPropertyChanged(nameof(Products));	
		}
    }
	private int quantity;
	public int Quantity
	{
		get => quantity;
		set
		{
			quantity=value;
			OnPropertyChanged(nameof(Quantity));
		}
	}
	private decimal price;
	public decimal Price { 
		get => price;
		set
		{
			price=value;
			OnPropertyChanged(nameof(Price));	
		}
	}
	private string name;
	
	public string Name {
		get => name;
		set {
			name=value;
			OnPropertyChanged(nameof(Name));	
		}
	}

	private Product productData;
	

	public Product ProductData
	{
		get => productData;
		set
		{
			productData=value;
			OnPropertyChanged(nameof(ProductData));
		}
	}
	private Category category;
	public Category Category
	{
		get => category;
		set
		{
			category=value;
			OnPropertyChanged(nameof(Category));
		}
	}
	private PhotoProduct _photo;
	
	public PhotoProduct Photo
	{
		get => _photo;
		set
		{
			_photo = value;
			OnPropertyChanged(nameof(Photo));
		}
	}

	private ICommand _uploadFileCommand;
	public ICommand UploadFileCommand { get; set; }
	private void UploadFile(object ibj)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";

		if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
		{
			var fileBytes = File.ReadAllBytes(openFileDialog.FileName);

			// File size check: ensure file is less than 2 MB
			if (fileBytes.Length < 2097152)
			{
				Photo.Bytes = fileBytes;
				Photo.Description = Path.GetFileName(openFileDialog.FileName);
				Photo.FileExtension = Path.GetExtension(openFileDialog.FileName);
				Photo.Size = fileBytes.Length;
			}
			else
			{
                System.Windows.MessageBox.Show("The file is too large.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}

	public ProductViewModel()
	{
		OpenPopupCommand = new RelayCommand(OpenPopup);
		SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
		ClosePopupCommand = new RelayCommand(ClosePopup);
		UpdateEntityCommand = new RelayCommand(UpdateEntity);
		DeleteEntityCommand=new RelayCommand(DeleteEntity);
		Categories=new ObservableCollection<Category>(CategoryDb.GetAll());
		Products=new ObservableCollection<Product>(ProductDb.GetAll());
		Photo = new();
		UploadFileCommand=new RelayCommand(UploadFile);
	}

	private void DeleteEntity(object obj)
	{
		try
		{
			var product = obj as Product;
			if (product is not null)
			{
				var result = System.Windows.MessageBox.Show($"Do you want to delete {product.Name}", "Delete Category", MessageBoxButton.YesNo);
				if (result==MessageBoxResult.Yes)
				{
					ProductDb.Delete(product);
					ProductDb.SaveChanges();
					Products=new(ProductDb.GetAll());
					var productId = product.Id;
					var photoList =PhotoProductDb.GetAll(p=>p.ProductId == productId);
					foreach(var i in photoList)
					{

						PhotoProductDb.Delete(i);
					}
					PhotoProductDb.SaveChanges();

					
				}
			}
		}
		catch (Exception ex)
		{


            System.Windows.MessageBox.Show("Error:"+ex.Message);
		}
	}

	private void UpdateEntity()
	{
		
	}

	private void ClosePopup(object obj)
	{
		IsPopupOpen = false;
	}

	private bool CanSaveChanges(object arg)
	{
		return Validator.TryValidateObject(this, new ValidationContext(this), null);
	}

	private void SaveChanges(object obj)
	{
		bool isSaved = false;
		try
		{


			var product = obj as Product;
			if (product != null)
			{
				if (product.Id == 0)
				{
					product = new Product() { Name=Name, Quantity=Quantity, Price=Price,CategoryId=Category.Id,Photo=Photo };
					Photo.ProductId = product.Id;
					ProductDb.Add(product);
					PhotoProductDb.Add(Photo);

				}
				else
				{
					
				}
				PhotoProductDb.SaveChanges();
				ProductDb.SaveChanges();
				
				isSaved = true;
				Products=new ObservableCollection<Product>(ProductDb.GetAll());


			}

		}
		catch (Exception)
		{


		}
		finally
		{
			if (isSaved)
				ClosePopup(obj);

		}
	}

	private void OpenPopup(object obj)
	{
		try
		{

			if (obj is null || obj is not Product objAsProduct)
				ProductData = new();
			else
			{
				ProductData =objAsProduct;
				Name = ProductData.Name;
				Quantity = ProductData.Quantity;
				Price=ProductData.Price;
				Category=ProductData.Category;
				


			}

			IsPopupOpen = true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
