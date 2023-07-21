namespace WebBlazorApp.Filters
{
    public class UserInfoService : IUserInfoService
    {
        private List<UserInfo> _userInfo;

        public UserInfoService()
        {
            _userInfo = new List<UserInfo> {
                    new UserInfo { UserName="username1", Password="password1", Role="User"},
                    new UserInfo{ UserName="admin1", Password="password2", Role="Admin"}
            };
        }
        public UserInfo? GetUserInfo(string username)
        {
            var result = _userInfo.FirstOrDefault(x => x.UserName == username);
            return result;
        }
    }
}
