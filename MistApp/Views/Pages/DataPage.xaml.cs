// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using MistApp.ViewModels.Pages;
using System.Threading.Tasks;
using System;
using System.Windows.Data;
using Wpf.Ui.Controls;
using MistApp.Models;
using System.Collections.ObjectModel;
using MistApp.Services;
using MistApp.Views.Windows;

namespace MistApp.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        private CollectionViewSource gameViewSource;

        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            gameViewSource =
                (CollectionViewSource)FindResource(nameof(gameViewSource));
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            using (var _context = new GameContext())
            {

                // this is for demo purposes only, to make it easier
                // to get up and running
                _context.Database.EnsureCreated();

                // load the entities into EF Core
                //_context.Game.Load();

                // Filter the data to include only games with even IDs
                var gamesSnapshot = new ObservableCollection<Game>(_context.Game.AsNoTracking().ToList());
                var copiesSnapshot = new ObservableCollection<Copy>( _context.Copy.AsNoTracking().ToList());
                string uid = UserService.Instance.CurrentUser.Handle;
                var filteredGames = gamesSnapshot.Where(game => copiesSnapshot.Any(copy => copy.GameId == game.Id && copy.UserId == uid)).ToList();

                // Bind to the source
                gameViewSource.Source = new ObservableCollection<Game>(filteredGames);
            }

        }

        private void OnGameButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Game selectedGame = (Game)button.Tag;
            NavService.Instance.GameToNav = selectedGame; 

            NavService.Instance.curMainWindow.NavigationView.Navigate(typeof(GamePage));

            //NavigationService.Navigate(new GamePage(selectedGame));

            //NavigationService.Navigate(new Uri("GamePage.xaml"), selectedGame);
            //NavigationService.Navigate(new Uri($"GamePage.xaml?parameter={gpid}"), UriKind.Relative,);
            // Retrieve the Game object from the Tag property of the button
            //var selectedGame = (Game)button.Tag;
            //GamePage gp = new GamePage(selectedGame);
            //NavigationService.Navigate(gp);

            // Navigate to the details page and pass the selected game as a parameter
            //navigationService.Navigate(typeof(Views.Pages.GamePage),);
        }


        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
