﻿using System.Windows.Controls;
using ProductManagmentAdminPanel.ViewModels;
namespace ProductManagmentAdminPanel.Services;
public interface INavigationService
 {
	void Navigate<TView, TViewModel>() where TView : Page where TViewModel : BaseViewModel;

}

