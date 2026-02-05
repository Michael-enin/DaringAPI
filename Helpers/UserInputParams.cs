namespace DaringAPI.Helpers
{
    public class UserInputParams :PaginationParams
    {
       public string CurrentUserName { get; set; }
       public string Gender { get; set; }
       public int MinAge { get; set; }=18;
       public int MaxAge { get; set; }=130;
       public string OrderBy { get; set; }="lastActive";
    }
}      