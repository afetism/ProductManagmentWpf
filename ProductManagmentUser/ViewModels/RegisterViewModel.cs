namespace ProductManagmentUser.ViewModels;

    class RegisterViewModel:BaseViewModel
{


	private DateTime? selectedDate;
	public DateTime? SelectedDate
	{
		get => selectedDate;
		set
		{
			selectedDate = value;
			OnPropertyChanged(nameof(SelectedDate));
		}
	}
	private string _password;
	public string Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged(nameof(Password));
		}
	}


}
