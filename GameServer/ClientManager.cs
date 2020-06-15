using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

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

        public static void SendDataTo(int connectionId, byte[] data)
        {
            Kaan_ByteBuffer buffer = new Kaan_ByteBuffer();
            buffer.Int_Yaz(data.GetUpperBound(0) - data.GetLowerBound(0) + 1);
            buffer.Bytes_Yaz(data);

            Sabitler.bagliClient[connectionId].stream
                .BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);

            buffer.Dispose();
        }

        public static async void SendDataToInGameAll(int connectionId, byte[] data)
        {
            Kaan_ByteBuffer buffer = new Kaan_ByteBuffer();
            buffer.Int_Yaz(data.GetUpperBound(0) - data.GetLowerBound(0) + 1);
            buffer.Bytes_Yaz(data);

            foreach (var player in Sabitler.bagliClient.Values)
            {
                if (player != null && player.connectionId != connectionId && player.PlayerInGame)
                {
                    Sabitler.bagliClient[player.connectionId].stream
                        .BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);

                }
            }

            await Task.Delay(20);
            buffer.Dispose();
        }

        public static void SendDataToAll(int connectionId, byte[] data)
        {
            Kaan_ByteBuffer buffer = new Kaan_ByteBuffer();
            buffer.Int_Yaz(data.GetUpperBound(0) - data.GetLowerBound(0) + 1);
            buffer.Bytes_Yaz(data);

            foreach (var player in Sabitler.bagliClient.Values)
            {
                if (player != null && player.connectionId != connectionId)
                {
                    Sabitler.bagliClient[player.connectionId].stream
                        .BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);

                }
            }

            buffer.Dispose();
        }
    }
}