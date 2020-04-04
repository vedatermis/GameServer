using System;
using System.Net.Sockets;

namespace GameServer
{
    public class Client
    {
        public TcpClient socket;
        public NetworkStream stream;
        private byte[] recBuffer;
        public Kaan_ByteBuffer buffer;

        public int connectionId;

        public void Start()
        {
            socket.SendBufferSize = 4096;
            socket.ReceiveBufferSize = 4096;
            stream = socket.GetStream();
            recBuffer = new byte[4096];

            stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);
        }

        private void OnReceiveData(IAsyncResult result)
        {
            try
            {
                int lenght = stream.EndRead(result);

                if (lenght <= 0)
                {
                    CloseConnection();
                    return;
                }

                byte[] newBytes = new byte[lenght];
                Array.Copy(recBuffer, newBytes, lenght);
                ServerHandleData.HandleData(connectionId, newBytes);
                stream.BeginRead(recBuffer, 0, socket.ReceiveBufferSize, OnReceiveData, null);
            }
            catch (Exception e)
            {
                CloseConnection();
            }
        }

        private void CloseConnection()
        {
            Sabitler.bagliClient.Remove(connectionId);
            Sabitler.Oyuncu_Cikti(connectionId.ToString());
            socket.Close();
        }
    }
}