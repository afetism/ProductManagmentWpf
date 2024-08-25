﻿using ProductManagmentUser.Services;
using ProductManagmentUser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductManagmentUser.Views
{
	/// <summary>
	/// Interaction logic for MainUserPanel.xaml
	/// </summary>
	public partial class MainUserPanelView : Window
	{
		public MainUserPanelView()
		{
			InitializeComponent();
			DataContext=new MainUserPanelViewModel(App.Container.GetInstance<INavigationService>());
		

		}
	}
}
