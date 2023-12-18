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
using Wpf.Ui.Controls;
using Microsoft.EntityFrameworkCore;


namespace MistApp.Views.Pages
{
    public partial class FriendsPage : INavigableView<FriendsViewModel>
    {
        public FriendsViewModel ViewModel { get; }
        private readonly GameContext _context =
            new GameContext();

        private CollectionViewSource gameViewSource;

        public FriendsPage(FriendsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            gameViewSource =
                (CollectionViewSource)FindResource(nameof(gameViewSource));
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {

            // this is for demo purposes only, to make it easier
            // to get up and running
            _context.Database.EnsureCreated();

            // load the entities into EF Core
            _context.Game.Load();

            // Filter the data to include only games with even IDs
            var gamesSnapshot = _context.Game.Local.ToList();
            string uid = UserService.Instance.CurrentUser.Handle;

            // Get friend ids of the current user
            var friendIds = _context.Friendship
                .Where(friendship => friendship.Friend1 == uid || friendship.Friend2 == uid)
                .Select(friendship => friendship.Friend1 == uid ? friendship.Friend2 : friendship.Friend1)
                .ToList();

            // Get friend entities
            var friends = _context.User
                .Where(user => friendIds.Contains(user.Handle))
                .ToList();

            // Bind to the source
            gameViewSource.Source = new ObservableCollection<User>(friends);


        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Wpf.Ui.Controls.Button)sender;

            // Retrieve the Game object from the Tag property of the button
            var selectedFriend = (User)button.Tag;
            NavigationService.Navigate(new FriendPage(selectedFriend));

            // Navigate to the details page and pass the selected game as a parameter
            //navigationService.Navigate(typeof(Views.Pages.GamePage),);
        }

        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
            //_context.Dispose();
        }
    }
}
