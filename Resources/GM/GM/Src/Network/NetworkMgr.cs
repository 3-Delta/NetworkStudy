using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace GM.Src.Network
{
    public class NetworkMgr : Singleton<NetworkMgr>
    {
        public NetworkClient client { get; private set; }
        public NetworkConnection connection { get; private set; }

        public void Init()
        {
            client = new NetworkClient();
            client.RegisterHandler(MsgType.Connect, OnConnected);
            client.RegisterHandler(MsgType.Disconnect, OnDisConnected);
            client.RegisterHandler(MsgType.Error, OnError);
        }
        public void Lunch()
        {
            if (connection == null || !connection.isConnected)
            {
                client.Connect("127.0.0.1", 20086);
            }
        }
        public void Exit()
        {
            connection?.Disconnect();
            connection?.Dispose();
        }

        private void OnConnected(NetworkMessage msg)
        {
            Console.WriteLine("OnConnected");
            connection = msg.conn;
        }
        private void OnDisConnected(NetworkMessage msg)
        {
            connection = null;
        }
        private void OnError(NetworkMessage msg)
        {

        }
    }
}
