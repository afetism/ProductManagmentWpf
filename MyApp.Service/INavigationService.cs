namespace MyApp.Service;

public interface INavigationService
{
	void Navigate<TView, TViewModel>() where TView : Page where TViewModel : BaseViewModel;
}
