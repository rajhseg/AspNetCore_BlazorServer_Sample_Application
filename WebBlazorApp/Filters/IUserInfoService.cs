namespace WebBlazorApp.Filters
{
    public interface IUserInfoService
    {
        UserInfo? GetUserInfo(string username);
    }
}
