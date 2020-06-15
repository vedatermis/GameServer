using System.Diagnostics;

namespace GameServer
{
    public class General
    {
        public static void StartGameServer()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            ServerTCP.InitializeNetwork();
            Yazi.Log_yaz("Sunucu Başlatıldı...");
            sw.Stop();

            Yazi.Log_yaz(text: $"Sunucu Başlama Süresi {sw.ElapsedMilliseconds} ms");
        }
    }
}