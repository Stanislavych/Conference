namespace ConferenceClient.BusinessLogic
{
    public class UserService
    {
        ApplicationContext applicationContext;
        public UserService()
        {
            applicationContext = new ApplicationContext();
        }
        public void Add(User user)
        {
            applicationContext.Users.Add(user);
            applicationContext.SaveChanges();
        }
    }
}