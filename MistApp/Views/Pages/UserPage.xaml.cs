using Microsoft.EntityFrameworkCore;
using MistApp.Models;
using MistApp.Services;
using MistApp.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MistApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {

        public UserViewModel ViewModel { get; set; }
        

        public UserPage()
        {
            InitializeComponent();
            User user = UserService.Instance.CurrentUser;

            ViewModel = new UserViewModel(user);
            DataContext = ViewModel;

            FriendList.ItemsSource = ViewModel.Friends;
            GameList.ItemsSource = ViewModel.FriendGames;

            
        }

        public UserPage(User user)
        {
            InitializeComponent();

            ViewModel = new UserViewModel(user);
            DataContext = ViewModel;

            FriendList.ItemsSource = ViewModel.Friends;
            GameList.ItemsSource = ViewModel.FriendGames;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            User user = UserService.Instance.CurrentUser;
            ViewModel = new UserViewModel(user);
            DataContext = ViewModel;

            FriendList.ItemsSource = ViewModel.Friends;
            GameList.ItemsSource = ViewModel.FriendGames;


        }

        private void OnLogoutClick(object sender, RoutedEventArgs e)
        {
            NavService.Instance.curMainWindow.NavigationView.HeaderVisibility = Visibility.Collapsed;
            NavService.Instance.curMainWindow.NavigationView.Navigate(typeof(LoginPage));
        }
    }
}
