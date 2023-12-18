// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;

namespace MistApp.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Copy>
            Copies
        { get; private set; } =
            new ObservableCollection<Copy>();

        public virtual ICollection<Review>
           Reviews
        { get; private set; } =
           new ObservableCollection<Review>();

    }

    public class User
    {
        [Key]
        public string Handle { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Copy>
            Copies
        { get; private set; } =
            new ObservableCollection<Copy>();

        public virtual ICollection<Friendship>
            Friends
        { get; private set; } =
            new ObservableCollection<Friendship>();

        public virtual ICollection<Review>
            Reviews
        { get; private set; } =
            new ObservableCollection<Review>();


    }

    public class Copy
    {
        public int CopyId { get; set; }
        public string DateAdded { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

    }

    public class Friendship
    {
        public int Id { get; set; }

        public string Friend1 { get; set; }
        public string Friend2 { get; set; }

    }

    public class Review
    {
        public int ReviewId { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public string UserHandle { get; set; }
        public virtual User User { get; set; }


    }

    public struct DataColor
    {
        public Brush Color { get; set; }
    }


}
