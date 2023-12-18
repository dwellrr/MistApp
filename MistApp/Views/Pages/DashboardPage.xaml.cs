// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using MistApp.Models;
using MistApp.Services;
using MistApp.ViewModels.Pages;
using MistApp.Views.Windows;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Navigation;
using Wpf.Ui.Controls;

namespace MistApp.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }
        public ObservableCollection<Game> GamesToDisplay { get; set; }

        //public INavigationService navigationService;
        private CollectionViewSource gameViewSource;

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            gameViewSource =
                (CollectionViewSource)FindResource(nameof(gameViewSource));
        }

        private void OnGameButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            Game selectedGame = (Game)button.Tag;
            NavService.Instance.GameToNav = selectedGame;

            NavService.Instance.curMainWindow.NavigationView.Navigate(typeof(GamePage));

            //NavigationService.Navigate(new Uri("GamePage.xaml"), selectedGame);

            // Navigate to the details page and pass the selected game as a parameter
            //navigationService.Navigate(typeof(Views.Pages.GamePage),);

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            using (var _context = new GameContext())
            {
                // this is for demo purposes only, to make it easier
                // to get up and running
                _context.Database.EnsureCreated();

                // load the entities into EF Core
                //_context.Game.AsNoTracking().Load();
                GamesToDisplay = new ObservableCollection<Game>(_context.Game.AsNoTracking().ToList());

                // bind to the source
                gameViewSource.Source =
                    GamesToDisplay;
            }
        }

        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
        }

        public event EventHandler GameButtonPressed;
    }
}
