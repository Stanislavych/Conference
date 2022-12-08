using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace WCFConference
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceConference : IServiceConference
    {
        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;

            SendMessage(": " + user.Name + " подключился к чату!", 0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user != null)
            {
                users.Remove(user);
                SendMessage(": " + user.Name + " покинул чат!", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();
                var user = users.FirstOrDefault(i => i.ID == id);
                if (user != null)
                {
                    answer += ": " + user.Name + " - ";
                }
                answer += message;
                item.operationContext.GetCallbackChannel<IServiceConferenceCallback>().MessageCallback(answer);
            }
        }
    }
}