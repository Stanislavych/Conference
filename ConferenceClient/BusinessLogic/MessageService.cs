namespace ConferenceClient.BusinessLogic
{
    public class MessageService
    {
        ApplicationContext applicationContext;
        public MessageService()
        {
            applicationContext = new ApplicationContext();
        }
        public void Add(Message message)
        {
            applicationContext.Messages.Add(message);
            applicationContext.SaveChanges();
        }
    }
}