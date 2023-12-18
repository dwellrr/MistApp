using MistApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MistApp.ViewModels
{
    public partial class RatingViewModel : ObservableObject
    {
        private Review _review;

        public RatingViewModel(Review review)
        {
            _review = review;
        }

        public string ReviewText
        {
            get { return _review.ReviewText; }
            set
            {
                _review.ReviewText = value;
                OnPropertyChanged(nameof(ReviewText));
            }
        }

        public int Rating
        {
            get { return _review.Rating; }
            set
            {
                _review.Rating = value;
                OnPropertyChanged(nameof(Rating));
                OnPropertyChanged(nameof(RatingIcons));
            }
        }

        public string UserHandle
        {
            get { return _review.UserHandle; }
            set
            {
                _review.UserHandle = value;
                OnPropertyChanged(nameof(UserHandle));
            }
        }

        public int GameId
        {
            get { return _review.GameId; }
            set
            {
                _review.GameId = value;
                OnPropertyChanged(nameof(GameId));
            }
        }

        public IEnumerable<object> RatingIcons
        {
            get { return Enumerable.Repeat(new object(), Rating); }
        }
    }
}
