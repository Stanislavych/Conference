using System.ServiceModel;

namespace WCFConference
{
    [ServiceContract(CallbackContract = typeof(IServiceConferenceCallback))]
    public interface IServiceConference
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int id);
    }

    public interface IServiceConferenceCallback
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallback(string message);
    }
}