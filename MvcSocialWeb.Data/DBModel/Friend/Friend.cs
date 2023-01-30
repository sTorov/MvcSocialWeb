namespace MvcSocialWeb.Data.DBModel.Friend
{
    public class Friend
    {
        public string Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string CurrentFriendId { get; set; }
        public User CurrentFriend { get; set; }

    }
}
