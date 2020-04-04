using System.Collections.Generic;

namespace GameServer
{
    public class ServerHandleData
    {
        public delegate void Packet(int connectionId, byte[] data);
        public static Dictionary<int, Packet> Packets = new Dictionary<int, Packet>();

        public static void InitializePackets()
        {

        }

        public static void HandleData(int connectionId, byte[] data)
        {
            byte[] buffer = (byte[]) data.Clone();
            int pLength = 0;

            if (Sabitler.bagliClient[connectionId].buffer == null)
                Sabitler.bagliClient[connectionId].buffer = new Kaan_ByteBuffer();

            Sabitler.bagliClient[connectionId].buffer.Bytes_Yaz(buffer);

            if (Sabitler.bagliClient[connectionId].buffer.Count() == 0)
            {
                Sabitler.bagliClient[connectionId].buffer.Clear();
                return;
            }

            if (Sabitler.bagliClient[connectionId].buffer.Length() >= 4)
            {

                pLength = Sabitler.bagliClient[connectionId].buffer.Int_Oku(false);
                if (pLength <= 0)
                {
                    Sabitler.bagliClient[connectionId].buffer.Clear();
                    return;
                }
            }

            while (pLength > 0 & pLength <= Sabitler.bagliClient[connectionId].buffer.Length() - 4)
            {
                if (pLength <= Sabitler.bagliClient[connectionId].buffer.Length() - 4)
                {
                    Sabitler.bagliClient[connectionId].buffer.Int_Oku();
                    data = Sabitler.bagliClient[connectionId].buffer.Bytes_Oku(pLength);
                    HandleDataPackets(connectionId, data);
                }
                pLength = 0;

                if (Sabitler.bagliClient[connectionId].buffer.Length() >= 4)
                {
                    pLength = Sabitler.bagliClient[connectionId].buffer.Int_Oku(false);
                    if (pLength <= 0)
                    {
                        Sabitler.bagliClient[connectionId].buffer.Clear();
                        return;
                    }
                }

                if (pLength <= 1)
                    Sabitler.bagliClient[connectionId].buffer.Clear();

            }
        }

        private static void HandleDataPackets(int connetiocID, byte[] data)
        {
            Kaan_ByteBuffer buffer = new Kaan_ByteBuffer();
            buffer.Bytes_Yaz(data);
            int packetID = buffer.Int_Oku();
            buffer.Dispose();
            if (Packets.TryGetValue(packetID, out Packet packet))
            {
                packet.Invoke(connetiocID, data);
            }
        }
    }
}