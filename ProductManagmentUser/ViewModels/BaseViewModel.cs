
using MyApp.Data.Models.EntityModel;
using MyApp.Data.Repositories;
using System.ComponentModel;

namespace ProductManagmentUser.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{

      public readonly Repository<User> UserDb =new Repository<User>() { };

	  protected void OnPropertyChanged(string propertyName)
	  {
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	  }


	  public event PropertyChangedEventHandler? PropertyChanged;
     



}
