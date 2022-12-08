using System.Windows;
using System.Windows.Input;
using ConferenceClient.ServiceConference;
using ConferenceClient.BusinessLogic;
using System;

namespace ConferenceClient
{
    public partial class MainWindow : Window, IServiceConferenceCallback
    {
        UserService userService = new UserService();
        MessageService messageService = new MessageService();
        bool isConnected = false;
        ServiceConferenceClient client;
        int ID;
        User user = new User();
        public MainWindow()
        {
            InitializeComponent();
            TextBoxMessage.IsEnabled = false;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceConferenceClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(TextBoxName.Text);
                TextBoxMessage.IsEnabled = true;
                TextBoxName.IsEnabled = false;
                ButtonConnectDisconnect.Content = "Отключиться";
                isConnected = true;
                user.Name = TextBoxName.Text;
                user.Date = DateTime.Now;
                userService.Add(user);
            }
        }
        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                TextBoxMessage.IsEnabled = false;
                TextBoxName.IsEnabled = true;
                ButtonConnectDisconnect.Content = "Подключиться";
                isConnected = false;
            }
        }

        private void ButtonConnectDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }
        public void MessageCallback(string message)
        {
            ListBoxChat.Items.Add(message);
            ListBoxChat.ScrollIntoView(ListBoxChat.Items[ListBoxChat.Items.Count - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TextBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMessage(TextBoxMessage.Text, ID);
                    messageService.Add(new Message { Date = DateTime.Now, textMessage = TextBoxMessage.Text, UserId = user.UserId });
                    TextBoxMessage.Text = string.Empty;
                }
            }
        }
    }
}