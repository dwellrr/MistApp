using Microsoft.EntityFrameworkCore;
using MistApp.Models;
using MistApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MistApp.ViewModels.Pages
{
    public partial class UserViewModel : ObservableObject
    {
        public User curUser;
        public ICollection<User> userFriends;
        public ICollection<Game> userGames;
        //public ICollection<Copy> copies;
        public List<string> dateAddedList;
        private readonly GameContext _context = new GameContext();

        public UserViewModel(User selectedUser)
        {
            curUser = selectedUser;
            string curUserId = selectedUser.Handle;

            // Get friend ids of the current user
            var friendIds = _context.Friendship
                .Where(friendship => friendship.Friend1 == curUserId || friendship.Friend2 == curUserId)
                .Select(friendship => friendship.Friend1 == curUserId ? friendship.Friend2 : friendship.Friend1)
                .ToList();

            // Get friend entities
            userFriends = _context.User
                .Where(user => friendIds.Contains(user.Handle))
                .ToList();

            userGames = _context.Game.Where(Game => Game.Copies.Any(Copy => Copy.UserId == curUserId)).ToList();
            //copies = _context.Copy.Where(Copy => Copy.UserId == curUserId).ToList();

            ICollection<string> dateAddedList = _context.Copy
                .Where(copy => copy.UserId == curUserId)
                .Select(copy => copy.DateAdded)
                .ToList();

        }

        public string Name => curUser.Name;
        public string Handle => curUser.Handle;
        public ObservableCollection<User> Friends => new ObservableCollection<User>(userFriends);
        public ObservableCollection<Game> FriendGames => new ObservableCollection<Game>(userGames);
        public ObservableCollection<string> Dates => new ObservableCollection<string>(dateAddedList);
        //public ObservableCollection<Copy> Copies => new ObservableCollection<Copy>(copies);
    }
}
