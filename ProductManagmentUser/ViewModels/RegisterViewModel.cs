using ProductManagmentAdminPanel.Helpers;
using ProductManagmentAdminPanel.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ProductManagmentUser.Helpers;
using ProductManagmentUser.Services;
using MyApp.Data.Models.EntityModel;
namespace ProductManagmentUser.ViewModels;

public class RegisterViewModel :BaseViewModel
{

	public  ICommand OpenPopupCommand { get; set; }
	public  RelayCommand VerifyCodeCommand { get; set; }
	public  ICommand CloseVerificationPopupCommand { get; set; }

	private string _email;
	private string _firstName;
	private string _lastName;
	private string _password;
	private DateOnly _selectedDate;

	private bool _isVerificationPopupOpen;
	private string verificationCode;
	private int randomCode;

	public bool IsVerificationPopupOpen
	{
		get => _isVerificationPopupOpen;
		set
		{
			if (_isVerificationPopupOpen != value)
			{
				_isVerificationPopupOpen = value;
				OnPropertyChanged(nameof(IsVerificationPopupOpen));
			}
		}
	}
	public string VerificationCode {
		get => verificationCode;
		set
		{
			verificationCode=value;
			OnPropertyChanged(nameof(VerificationCode));
		}
		}
	private User _userData;
	private int yourRandomCode;

	public User UserData
	{
		get => _userData;
		set
		{
			_userData = value;
			OnPropertyChanged(nameof(UserData));
		}
	}

	public string Email
	{
		get => _email;
		set
		{
			_email = value;
			OnPropertyChanged(nameof(Email));
		}
	}

	public string FirstName
	{
		get => _firstName;
		set
		{
			_firstName = value;
			OnPropertyChanged(nameof(FirstName));
		}
	}

	public string LastName
	{
		get => _lastName;
		set
		{
			_lastName = value;
			OnPropertyChanged(nameof(LastName));
		}
	}

	public string Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged(nameof(Password));
			CommandManager.InvalidateRequerySuggested();
		}
	}

	public DateOnly SelectedDate
	{
		get => _selectedDate;
		set
		{
			_selectedDate = value;
			OnPropertyChanged(nameof(SelectedDate));
		}
	}
	public int RandomCode { 
		get => randomCode; 
		set { 
			randomCode=value;
			OnPropertyChanged(nameof(RandomCode)); 
		} 
	}
	

	public ICommand LoginCommand { get; }
	public ICommand SignUpCommand { get; }

	private readonly INavigationService navigationService;

	public RegisterViewModel(INavigationService navigationService)
	{
		LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
		SignUpCommand = new RelayCommand(ExecuteSignUp, CanExecuteSignUp);
		VerifyCodeCommand=new RelayCommand(ExecuteVerifyCode);
		CloseVerificationPopupCommand=new RelayCommand(ExecuteCloseVerificationPopup);
		this.navigationService = navigationService;
		
	}

	private void ExecuteCloseVerificationPopup(object parameter)
	{
		IsVerificationPopupOpen=false;
		VerificationCode=string.Empty;
	}

	private void ExecuteVerifyCode(object parameter)
	{

		var param = parameter as RegisterViewModel;

		
		if (param is not null)
		{
	
			var userData = param.UserData;
			var randomCode = param.VerificationCode;

			
			if (randomCode.ToString() != RandomCode.ToString())
			{
				
				MessageBox.Show("Girilen kod yanlış. Lütfen tekrar deneyin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				UserDb.Add(UserData);
				UserDb.SaveChanges();
				MessageBox.Show("Kod doğru!", "Başarı", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}
		else
		{
			
			MessageBox.Show("Parametreler geçersiz. Lütfen tekrar deneyin.", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}

	
	private bool CanExecuteLogin(object parameter)
	{
		
		return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) ;
	}

	private void ExecuteLogin(object parameter)
	{
		var passwordManager=new PasswordManager();
		var user = UserDb.Get(e => e.Email==Email);
		 if(user is not null && passwordManager.VerifyPassword(user.Password,Password))
		{
			MessageBox.Show("hELLOR");
		}
		else
		{

			MessageBox.Show("can not find user");
		}

	}

	private bool CanExecuteSignUp(object parameter)
	{

		return !string.IsNullOrEmpty(FirstName) &&
			   !string.IsNullOrEmpty(LastName) &&
			   !string.IsNullOrEmpty(Email) &&
			   !string.IsNullOrEmpty(Password);
	}

	private void ExecuteSignUp(object parameter)
	{
		UserData = UserDb.Get(e => e.Email==Email);
		if (UserData is  not null) {
			MessageBox.Show("This user is exist");
			UserData=null;
		}
		else
		{
			var emailService = new MailService();
			Random random = new Random();

			RandomCode = random.Next(100, 1000);
			PasswordManager password = new();
			emailService.SendMail(Email, "Welcome to Our Service", $"{RandomCode}");
			UserData=new() {Firstname=FirstName, LatsName=LastName, Email=Email, DateofBirth =SelectedDate,Password=password.HashPassword(Password)};
		    IsVerificationPopupOpen=true;
		    
			

		};
			
		
	}

	
}


