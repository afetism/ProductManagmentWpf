﻿using ProductManagmentUser;
using ProductManagmentUser.ViewModels;
using System.Windows.Controls;

namespace ProductManagmentUser.Services;
class NavigationService : INavigationService
{
	public void Navigate<TView, TViewModel>() where TView : Page where TViewModel : BaseViewModel
	{
		var mainVm = App.Current.MainWindow.DataContext as MainUserPanelViewModel;
		if(mainVm is not  null )
		{
			mainVm.CurrentPage = (App.Container.GetInstance<TView>())!;
			mainVm.CurrentPage.DataContext = App.Container.GetInstance<TViewModel>();
		}
	}
}

