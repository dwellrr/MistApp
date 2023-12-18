using Microsoft.EntityFrameworkCore;
using MistApp.Models;
using MistApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MistApp.ViewModels;

namespace MistApp.ViewModels.Pages
{
    public partial class GameViewModel : ObservableObject
    {
        private GameContext context;
        public Game curGame;
        private User curUser;
        public ICollection<Review> gameReviews;
        public Copy foundCopy;
        public Review foundReview { get; set; }
        public string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                }
            }
        }
        public string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        public bool filled1;
        public bool Filled1
        {
            get { return filled1; }
            set
            {
                if (filled1 != value)
                {
                    filled1 = value;
                    OnPropertyChanged(nameof(Filled1));
                }
            }
        }

        public bool filled2;
        public bool Filled2
        {
            get { return filled2; }
            set
            {
                if (filled2 != value)
                {
                    filled2 = value;
                    OnPropertyChanged(nameof(Filled2));
                }
            }
        }

        public bool filled3;
        public bool Filled3
        {
            get { return filled3; }
            set
            {
                if (filled3 != value)
                {
                    filled3 = value;
                    OnPropertyChanged(nameof(Filled3));
                }
            }
        }

        public bool filled4;
        public bool Filled4
        {
            get { return filled4; }
            set
            {
                if (filled4 != value)
                {
                    filled4 = value;
                    OnPropertyChanged(nameof(Filled4));
                }
            }
        }

        public bool filled5;
        public bool Filled5
        {
            get { return filled5; }
            set
            {
                if (filled5 != value)
                {
                    filled5 = value;
                    OnPropertyChanged(nameof(Filled5));
                }
            }
        }

        public string _buttonText;
        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (_buttonText != value)
                {
                    _buttonText = value;
                    OnPropertyChanged(nameof(ButtonText));
                }
            }
        }

        private bool _isButtonEnabled;
        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set
            {
                if (_isButtonEnabled != value)
                {
                    _isButtonEnabled = value;
                    OnPropertyChanged(nameof(IsButtonEnabled));
                }
            }
        }

        public bool IsGameInLibrary;
        //private GameContext _context;
        public ObservableCollection<RatingViewModel> curReviewsVM { get; set; }

        public GameViewModel()
        {
            curGame = NavService.Instance.GameToNav;
            curUser = UserService.Instance.CurrentUser;
            context = new GameContext();


            curReviewsVM = new ObservableCollection<RatingViewModel>(
            context.Review.Where(Review => Review.GameId == curGame.Id).AsNoTracking().ToList().Select(review => new RatingViewModel(review))
            );
            foundReview = context.Review.Where(Review => Review.UserHandle == curUser.Handle && Review.GameId == curGame.Id).AsNoTracking().FirstOrDefault();

            Name = curGame.Name;
            Description = curGame.Description;
            ImagePath = curGame.ImagePath;

            if (context.Copy.Any(copy => copy.UserId == UserService.Instance.CurrentUser.Handle && copy.GameId == curGame.Id))
            {
                IsGameInLibrary = true;
                IsButtonEnabled = true;
                ButtonText = "Remove from library";
                foundCopy = context.Copy.Where(Copy => Copy.UserId == UserService.Instance.CurrentUser.Handle && Copy.GameId == curGame.Id).FirstOrDefault();
            }            
            else
            {
                IsGameInLibrary = false;
                IsButtonEnabled = true;
                ButtonText = $"Add to library for {curGame.Price} $";
            }

        }



        public void AddNewCopyToUser()
        {
                DateTime currentDateTime = DateTime.Now;
                string formattedDateTime = currentDateTime.ToString("dd.MM.yyyy");
                Copy newCopy = new Copy
                {
                    DateAdded = formattedDateTime,
                    GameId = curGame.Id,
                    UserId = curUser.Handle,
                };

                context.Copy.Add(newCopy);
                context.SaveChanges();
            foundCopy = newCopy;


        }

        public void RemoveCopyFromUser()
        {
            using (var _context = new GameContext())
            {
                if (foundCopy != null)
                {
                    _context.Copy.Remove(foundCopy);
                    _context.SaveChanges();
                }
            }
            
        }


        public void AddNewReview(int rating, string reviewText)
        {

                foundReview = new Review();

                foundReview.GameId = curGame.Id;
                foundReview.UserHandle = curUser.Handle;
                foundReview.ReviewText = reviewText;
                foundReview.Rating = rating;

                context.Review.Add(foundReview);
                context.SaveChanges();
            curReviewsVM = new ObservableCollection<RatingViewModel>(
            context.Review.Where(Review => Review.GameId == curGame.Id).AsNoTracking().ToList().Select(review => new RatingViewModel(review))
            );
        }

        public void EditReview(int rating, string reviewText)
        {
            context.Entry(foundReview).State = EntityState.Modified;
            //Review editedReview = foundReview;
            foundReview.ReviewText = reviewText;
            foundReview.Rating = rating;

            context.SaveChanges();
            curReviewsVM = new ObservableCollection<RatingViewModel>(
            context.Review.Where(Review => Review.GameId == curGame.Id).AsNoTracking().ToList().Select(review => new RatingViewModel(review))
            );
        }

        public void DeleteReview()
        {
            curReviewsVM.Clear();
                context.Review.Remove(foundReview);
                
                context.SaveChanges();
                curReviewsVM = new ObservableCollection<RatingViewModel>(
                context.Review.Where(Review => Review.GameId == curGame.Id).AsNoTracking().ToList().Select(review => new RatingViewModel(review))
                );
                foundReview = null;
        }




    }
}
