using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    public class ClientManager
    {
        public static void CreateNewConnection(TcpClient tempClient)
        {
            Client newClient = new Client();
            newClient.socket = tempClient;
            newClient.connectionId = ((IPEndPoint)tempClient.Client.RemoteEndPoint).Port;
            newClient.Start();
            Sabitler.bagliClient.Add(newClient.connectionId, newClient);

            //if(Sabitler.Odalar)

            //Sabitler.bagliClient[newClient.connectionId].baglanti = Sabitler.Mysql_Data.MySqlBaslat();




        }
    }
}