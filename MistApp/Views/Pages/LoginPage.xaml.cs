using MistApp.Models;
using MistApp.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MistApp.Views.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private GameContext context;
        public LoginPage()
        {
            InitializeComponent();
            context = new GameContext();
        }

        private void onButtonClick(object sender, RoutedEventArgs e)
        {
            User newUser;
            User curUser = UserService.Instance.CurrentUser;
            string newHandle = UsernameTxt.Text;
            if (newHandle != null)
            {
                newUser = context.User.Where(User => User.Handle == newHandle).FirstOrDefault();
                if (newUser == null)
                {
                    SubmitButton.Content = "That user doesn't exist!";
                }
                else
                {
                    UserService.Instance.CurrentUser = newUser;
                    NavService.Instance.curMainWindow.NavigationView.HeaderVisibility = Visibility.Visible;
                    NavService.Instance.curMainWindow.NavigationView.Navigate(typeof(UserPage));
                }
            }
        }
    }
}
