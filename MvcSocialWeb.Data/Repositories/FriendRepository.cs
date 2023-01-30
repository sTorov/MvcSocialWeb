﻿using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.DBModel;
using MvcSocialWeb.Data.DBModel.Friend;

namespace MvcSocialWeb.Data.Repositories
{
    public class FriendRepository : Repository<Friend>
    {
        public FriendRepository(SocialWebContext db) : base(db){ }

        public void AddFriend(User target, User friend)
        {
            var friends = Set
                .AsEnumerable()
                .FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

            if(friends == null)
            {
                var item = new Friend()
                {
                    UserId = target.Id,
                    User = target,
                    CurrentFriendId = friend.Id,
                    CurrentFriend = friend
                };

                Create(item);
            }
        }

        public List<User> GetFriendsByUser(User target)
        {
            var friends = Set.Include(x => x.CurrentFriend)
                .AsEnumerable()
                .Where(x => x.User.Id == target.Id)
                .Select(x => x.CurrentFriend);

            return friends.ToList();
        }

        public void DeleteFriend(User target, User friend)
        {
            var friends = Set
                .AsEnumerable()
                .FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

            if(friends != null)
                Delete(friends);
        }
    }
}
