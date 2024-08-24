
using MyApp.Data.Models.EntityModel;
using MyApp.Data.Repositories;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductManagmentUser.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
	Dictionary<String, List<string>> Errors = new();
	public readonly Repository<User> UserDb =new Repository<User>() { };
	public readonly Repository<Product> ProductDb = new Repository<Product>() { };

	protected void OnPropertyChanged(string propertyName)
	  {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	  }


	  public event PropertyChangedEventHandler? PropertyChanged;

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


}
