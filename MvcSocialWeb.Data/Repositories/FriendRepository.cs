using Microsoft.EntityFrameworkCore;
using MvcSocialWeb.Data.DBModel.Friend;
using MvcSocialWeb.Data.DBModel.Users;

namespace MvcSocialWeb.Data.Repositories
{
    public class FriendRepository : Repository<Friend>
    {
        public FriendRepository(SocialWebContext db) : base(db){ }

        public async Task AddFriendAsync(User target, User friend)
        {
            var friends = await Set.FirstOrDefaultAsync(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

            if(friends == null)
            {
                var item = new Friend()
                {
                    UserId = target.Id,
                    User = target,
                    CurrentFriendId = friend.Id,
                    CurrentFriend = friend
                };

                await CreateAsync(item);
            }
        }

        public async Task<List<User>> GetFriendsByUserAsync(User target)
        {
            var friends = Set
            .Include(x => x.CurrentFriend)
            .Include(x => x.User)
            .AsEnumerable()
            .Where(x => x.User.Id == target.Id)
            .Select(x => x.CurrentFriend);

            return await Task.Run(() => friends.ToList());
        }

        public async Task DeleteFriendAsync(User target, User friend)
        {
            var friends = await Set.FirstOrDefaultAsync(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

            if (friends != null)
                await DeleteAsync(friends);
        }
    }
}
