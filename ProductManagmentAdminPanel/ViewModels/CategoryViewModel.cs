using MyApp.Data.Models.EntityModel;
using ProductManagmentAdminPanel.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Input;

namespace ProductManagmentAdminPanel.ViewModels;

public class CategoryViewModel : BaseViewModel
{
	
	public override ICommand OpenPopupCommand { get ; set; }
	public override RelayCommand SaveChangesCommand { get ; set ; }
	public override ICommand ClosePopupCommand { get ; set ; }
	public override ICommand UpdateEntityCommand { get; set ; }
	public override ICommand DeleteEntityCommand { get ; set; }


	

	private ObservableCollection<Category> categories = [];
	
	

	public ObservableCollection<Category> Categories
	{
		get => categories;
		set
		{
			categories=value;
			OnPropertyChanged(nameof(Categories));
		}
	}
	
	private string name;
	[Required(ErrorMessage = "Name is Required")]
	public string Name
	{
		get => name;
		set
		{
			name=value;
			OnPropertyChanged(nameof(Name));
		}

	}

	private Category categoryData;
	public Category CategoryData
	{
		get => categoryData;
		set
		{
			categoryData=value;
			OnPropertyChanged(nameof(CategoryData));
		}
	}

    public CategoryViewModel()
    {
		OpenPopupCommand = new RelayCommand(OpenPopup);
		SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
		ClosePopupCommand = new RelayCommand(ClosePopup);
		UpdateEntityCommand = new RelayCommand(UpdateEntity);
		DeleteEntityCommand=new RelayCommand(DeleteEntity);
		Categories=new ObservableCollection<Category>(CategoryDb.GetAll());
	}

	private void DeleteEntity(object obj)
	{
		try
		{
			var category = obj as Category;
			if (category is not null)
			{
				var result = MessageBox.Show($"Do you want to delete {category.Name}", "Delete Category", MessageBoxButton.YesNo);
				if (result==MessageBoxResult.Yes)
				{
					CategoryDb.Delete(category);
					CategoryDb.SaveChanges();
					Categories=new(CategoryDb.GetAll());

				}
			}
		}
		catch (Exception ex)
		{


			MessageBox.Show("Error:"+ex.Message);
		}
	}

	private void UpdateEntity(object obj)
	{
		var category = obj as Category;
		if (category != null)
		{
			OpenPopup(category);
		}
	}

	private void ClosePopup(object obj)
	{
		Name = string.Empty;
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


			var category = obj as Category;
			if (category != null)
			{
				if (category.Id == 0)
				{
					category = new Category() {Name = Name };
					CategoryDb.Add(category);

				}
				else
				{
					category.Name = Name;
					CategoryDb.Update(category);
				}
				
				CategoryDb.SaveChanges();
				isSaved = true;
				Categories=new ObservableCollection<Category>(CategoryDb.GetAll());
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

			if (obj is null || obj is not Category objAsCategory)
				CategoryData = new();
			else
			{
				CategoryData = objAsCategory;
				Name = CategoryData.Name;
			}

			IsPopupOpen = true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}
}
