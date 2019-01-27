using DarkRift;
using DarkRift.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerDarkRiftPlugin
{

    static class RandomLetter
    {
        static System.Random _random = new System.Random();
        public static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }
    }


    /// <summary>
    ///     Manages control flow.
    /// </summary>
    public class ControllerManager : Plugin
    {
        /// <summary>
        ///     The version number of the plugin in SemVer form.
        /// </summary>
        public override Version Version => new Version(1, 0, 0);

        /// <summary>
        ///     Indicates that this plugin is thread safe and DarkRift can invoke events from 
        ///     multiple threads simultaneously.
        /// </summary>
        public override bool ThreadSafe => true;

        Dictionary<string, ushort> idDict = new Dictionary<string, ushort>();

        public ControllerManager(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {

            //Subscribe for notification when a new client connects
            ClientManager.ClientConnected += ClientManager_ClientConnected;
        }

        /// <summary>
        ///     Invoked when a new client connects.
        /// </summary>
        /// <param name="sender">The client manager that initiated the event.</param>
        /// <param name="e">The event arguments.</param>
        void ClientManager_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            //When new clients connect we subscribe to when they send data on the WORLD tag.
            e.Client.MessageReceived += Client_WorldEvent;
        }

        /// <summary>
        ///     Invoked whenever data is received from a client.
        /// </summary>
        /// <param name="sender">The client that sent the data.</param>
        /// <param name="e">The event arguments.</param>
        void Client_WorldEvent(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage() as Message)
            {
                // filter messages
                if (message.Tag == MessageTags.Input)
                {
                    // find paired game
                }
                else if (message.Tag == MessageTags.Initialize)
                {
                    using (DarkRiftWriter writer = DarkRiftWriter.Create())
                    {
                        string ID = "";
                        for (int i = 0; i < 4; i++)
                        {
                            ID += RandomLetter.GetLetter();
                        }
                        writer.Write(ID);

                        // store in dictionary
                        idDict.Add(ID, e.Client.ID);

                        // generate ID and send it back
                        using (Message newMsg = Message.Create(MessageTags.Initialize, writer))
                            e.Client.SendMessage(newMsg, SendMode.Reliable);
                    }
                }
                else if (message.Tag == MessageTags.Pair)
                {
                    // try to validate

                    // store connection

                    // send pair status to both controller and game
                }
            }
        }

    }
}
