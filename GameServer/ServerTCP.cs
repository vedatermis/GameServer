using System;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    public class ServerTCP
    {
        static TcpListener serverSocket = new TcpListener(IPAddress.Any, 6060);

        public static void InitializeNetwork()
        {
            Yazi.Log_yaz("Paketleriniz Başlatılıyor");
            ServerHandleData.InitializePackets();
            serverSocket.Start();
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnet), null);
        }

        private static void OnClientConnet(IAsyncResult result)
        {
            TcpClient client = serverSocket.EndAcceptTcpClient(result);
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnet), null);


            ClientManager.CreateNewConnection(client);
        }
    }
}