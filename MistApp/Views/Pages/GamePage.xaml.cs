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

using Microsoft.EntityFrameworkCore;
using MistApp.ViewModels.Pages;
using Wpf.Ui.Controls;
using MistApp.Models;
using MistApp.Services;

namespace MistApp.Views.Pages
{
    public partial class GamePage : INavigableView<GameViewModel>
    {
        public GameViewModel ViewModel { get; set; }
        public string chosenRating;
        public bool editMode;



        public GamePage()
        {

            InitializeComponent();
            
            chosenRating = "0";

        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {

            ViewModel = new GameViewModel();
            DataContext = ViewModel;
            //ViewModel.RefreshGame();
            ReviewList.ItemsSource = ViewModel.curReviewsVM;
            if (ViewModel.foundReview != null)
            {
                editMode = false;
                txtReview.Text = ViewModel.foundReview.ReviewText;
                txtReview.IsEnabled = false;
                chosenRating = ViewModel.foundReview.Rating.ToString();
                LockStars();
                SubmitButton.IsEnabled = false;
                SubmitButton.Content = "Review already submitted";
                EditButton.Content = "Edit";
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                EditButton.Visibility = Visibility.Hidden;
                DeleteButton.Visibility = Visibility.Hidden;
                editMode = true;
                txtReview.Text = "";
                txtReview.IsEnabled = true;
                SubmitButton.IsEnabled = true;
                SubmitButton.Content = "Submit";
            }
        }

        private void OnAddButtonClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.IsGameInLibrary)
            {
                ViewModel.RemoveCopyFromUser();
                ViewModel.ButtonText = "Removed!";
                ViewModel.IsButtonEnabled = false;
            }
            else
            {
                ViewModel.AddNewCopyToUser();
                ViewModel.ButtonText = "Added!";
                ViewModel.IsButtonEnabled = false;
            }
        }

        private void PageUnloaded(object sender, RoutedEventArgs e)
        {
        }

        private void LockStars()
        {
            if (chosenRating == "0")
            {
                ViewModel.Filled1 = false;
                ViewModel.Filled2 = false;
                ViewModel.Filled3 = false;
                ViewModel.Filled4 = false;
                ViewModel.Filled5 = false;
            }
            else if (chosenRating == "1")
            {
                ViewModel.Filled1 = true;
                ViewModel.Filled2 = false;
                ViewModel.Filled3 = false;
                ViewModel.Filled4 = false;
                ViewModel.Filled5 = false;
            }
            else if (chosenRating == "2")
            {
                ViewModel.Filled1 = true;
                ViewModel.Filled2 = true;
                ViewModel.Filled3 = false;
                ViewModel.Filled4 = false;
                ViewModel.Filled5 = false;
            }
            else if (chosenRating == "3")
            {
                ViewModel.Filled1 = true;
                ViewModel.Filled2 = true;
                ViewModel.Filled3 = true;
                ViewModel.Filled4 = false;
                ViewModel.Filled5 = false;
            }
            else if (chosenRating == "4")
            {
                ViewModel.Filled1 = true;
                ViewModel.Filled2 = true;
                ViewModel.Filled3 = true;
                ViewModel.Filled4 = true;
                ViewModel.Filled5 = false;

            }
            else if (chosenRating == "5")
            {
                ViewModel.Filled1 = true;
                ViewModel.Filled2 = true;
                ViewModel.Filled3 = true;
                ViewModel.Filled4 = true;
                ViewModel.Filled5 = true;
            }
        }

        private void StarButton_Click(object sender, RoutedEventArgs e)
        {
            if (editMode == true)
            {
                var button = (System.Windows.Controls.Button)sender;
                chosenRating = (string)button.Tag;
                LockStars();

            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (System.Windows.Controls.Button)sender;
            int finalRating = Convert.ToInt32(chosenRating);
            string reviewText = txtReview.Text;
            if (finalRating == 0)
            {
                button.Content = "Cannot submit without a rating!";
            }
            else if (ViewModel.foundReview == null)
            {
                ViewModel.AddNewReview(finalRating, reviewText);
                button.IsEnabled = false;
                button.Content = "Review added";
                EditButton.Content = "Edit";
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                editMode = false;
                ReviewList.ItemsSource = ViewModel.curReviewsVM;

            }
            else if (ViewModel.foundReview != null)
            {
                ViewModel.EditReview(finalRating, reviewText);
                button.IsEnabled = false;
                button.Content = "Review changed!";
                EditButton.Content = "Edit";
                EditButton.Visibility = Visibility.Visible;
                DeleteButton.Visibility = Visibility.Visible;
                editMode = false;
                ReviewList.ItemsSource = ViewModel.curReviewsVM;
            }



        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            var button = (System.Windows.Controls.Button)sender;
            string hoveredRating = (string)button.Tag;
            if (chosenRating == "0" || editMode == true)
            {
                if (hoveredRating == "1")
                {
                    ViewModel.Filled1 = true;
                    ViewModel.Filled2 = false;
                    ViewModel.Filled3 = false;
                    ViewModel.Filled4 = false;
                    ViewModel.Filled5 = false;
                }
                else if (hoveredRating == "2")
                {
                    ViewModel.Filled1 = true;
                    ViewModel.Filled2 = true;
                    ViewModel.Filled3 = false;
                    ViewModel.Filled4 = false;
                    ViewModel.Filled5 = false;
                }
                else if (hoveredRating == "3")
                {
                    ViewModel.Filled1 = true;
                    ViewModel.Filled2 = true;
                    ViewModel.Filled3 = true;
                    ViewModel.Filled4 = false;
                    ViewModel.Filled5 = false;
                }
                else if (hoveredRating == "4")
                {
                    ViewModel.Filled1 = true;
                    ViewModel.Filled2 = true;
                    ViewModel.Filled3 = true;
                    ViewModel.Filled4 = true;
                    ViewModel.Filled5 = false;

                }
                else if (hoveredRating == "5")
                {
                    ViewModel.Filled1 = true;
                    ViewModel.Filled2 = true;
                    ViewModel.Filled3 = true;
                    ViewModel.Filled4 = true;
                    ViewModel.Filled5 = true;
                }
            }
            
            
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            if (chosenRating == "0")
            {
                ViewModel.Filled1 = false;
                ViewModel.Filled2 = false;
                ViewModel.Filled3 = false;
                ViewModel.Filled4 = false;
                ViewModel.Filled5 = false;
            }
            else if (editMode == true)
            {
                LockStars();
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteReview();
            ReviewList.ItemsSource = ViewModel.curReviewsVM;
            EditButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            editMode = true;
            txtReview.Text = "";
            txtReview.IsEnabled = true;
            chosenRating = "0";
            SubmitButton.IsEnabled = true;
            SubmitButton.Content = "Submit";
            LockStars();
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            if (editMode == true)
            {
                editMode = false;
                txtReview.Text = ViewModel.foundReview.ReviewText;
                txtReview.IsEnabled = false;
                chosenRating = ViewModel.foundReview.Rating.ToString();
                LockStars();
                SubmitButton.IsEnabled = false;
                SubmitButton.Content = "Review already submitted";
                EditButton.Content = "Edit";
            }
            else
            {
                editMode = true;
                EditButton.Content = "Cancel";
                txtReview.IsEnabled = true;
                SubmitButton.IsEnabled = true;
                SubmitButton.Content = "Submit";

            }
        }
    }
}
