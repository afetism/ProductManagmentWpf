using System.Windows.Controls;

namespace ProductManagmentUser.ViewModels;

class MainViewModel:BaseViewModel
    {
	private Page currentPage;

	public Page CurrentPage {
		get => currentPage;
		set
		{
			currentPage=value;
			OnPropertyChanged(nameof(CurrentPage));
		}
    }
}
