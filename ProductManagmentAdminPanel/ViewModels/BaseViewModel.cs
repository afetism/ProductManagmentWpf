using MyApp.Data.Data;
using MyApp.Data.Models.EntityModel;
using MyApp.Data.Repositories;
using ProductManagmentAdminPanel.Helpers;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace ProductManagmentAdminPanel.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
	public readonly IRepository<Category> CategoryDb=new Repository<Category>();
	public readonly IRepository<Product> ProductDb = new Repository<Product>();
	public readonly IRepository<PhotoProduct> PhotoProductDb = new Repository<PhotoProduct>();
	

	Dictionary<String, List<string>> Errors = new();

	private bool _isPopupOpen;
	public bool IsPopupOpen
	{
		get { return _isPopupOpen; }
		set
		{
			_isPopupOpen = value;
			OnPropertyChanged(nameof(IsPopupOpen));
		}
	}

	public abstract ICommand OpenPopupCommand { get; set; }
	public abstract RelayCommand SaveChangesCommand { get; set; }
	public abstract ICommand ClosePopupCommand { get; set; }
	public abstract ICommand UpdateEntityCommand { get; set; }
	public abstract ICommand DeleteEntityCommand { get; set; }



	public event PropertyChangedEventHandler? PropertyChanged;


	protected void OnPropertyChanged(string propertyName)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}



	public bool HasErrors => Errors.Count > 0;

	public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

	public IEnumerable GetErrors(string? propertyName)
	{
		if (Errors.ContainsKey(propertyName))
		{
			return Errors[propertyName];

		}
		else
		{
			return Enumerable.Empty<string>();
		}

	}




	public void Validate(string propertyName, object propertyValue)
	{
		var results = new List<ValidationResult>();

		Validator.TryValidateProperty(propertyValue, new ValidationContext(this) { MemberName = propertyName }, results);

		if (results.Any())
		{
			// If the property name already exists, update the list of errors
			if (Errors.ContainsKey(propertyName))
			{
				Errors[propertyName] = results.Select(r => r.ErrorMessage).ToList();
			}
			else
			{
				Errors.Add(propertyName, results.Select(r => r.ErrorMessage).ToList());
			}
			ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
		}
		else
		{
			// If there are no errors, remove the property name from the dictionary
			if (Errors.ContainsKey(propertyName))
			{
				Errors.Remove(propertyName);
				ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
			}
		}

		SaveChangesCommand.RaiseCanExecuteChanged();
	}


}
