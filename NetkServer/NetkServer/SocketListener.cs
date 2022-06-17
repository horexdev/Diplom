using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Script;

namespace NetkServer;

public static class SocketListener
{
    public static void StartListening(string ip, int port)
    {
        // получаем адреса для запуска сокета
        var ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);

        // создаем сокет
        var listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            // связываем сокет с локальной точкой, по которой будем принимать данные
            listenSocket.Bind(ipPoint);

            // начинаем прослушивание
            listenSocket.Listen(10);

            Console.WriteLine($"[{DateTime.Now.ToLocalTime()}] Server started on {ip}:{port}");

            while (true)
            {
                var handler = listenSocket.Accept();
                if (handler.Connected == false)
                    return;

                //104857600
                var data = new byte[104857600];

                using var networkStream = new NetworkStream(handler);
                using var memoryStream = new MemoryStream();

                if (networkStream.CanRead)
                {
                    do
                    {
                        var bytesRead = networkStream.Read(data, 0, data.Length);
                        memoryStream.Write(data, 0, bytesRead);
                    } while (networkStream.DataAvailable);
                }

                memoryStream.Position = 0;

                var message = Encoding.Unicode.GetString(memoryStream.ToArray());
                var uploadData = JsonConvert.DeserializeObject<UploadData>(message);

                var args = new List<object> { handler };
                args.AddRange(uploadData.Args);
                

                var localEndPoint = handler.RemoteEndPoint?.ToString()?.Split(':');
                var clientIp = string.IsNullOrEmpty(localEndPoint?[0]) ? "undefined" : localEndPoint[0];

                Console.WriteLine($"[{DateTime.Now.ToLocalTime()}] {uploadData.Name} triggered by {clientIp}");

                ServerScripts.Invoke(uploadData.Name, args.ToArray());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public static void Send(Socket handler, string eventName, params object[] args)
    {
        var uploadData = new UploadData(eventName, args);
        var json = JsonConvert.SerializeObject(uploadData);

        var buffer = Encoding.Unicode.GetBytes(json);

        var localEndPoint = handler.RemoteEndPoint?.ToString()?.Split(':');
        var clientIp = string.IsNullOrEmpty(localEndPoint?[0]) ? "undefined" : localEndPoint[0];

        Console.WriteLine($"[{DateTime.Now.ToLocalTime()}] Send trigger {eventName} to {clientIp} (bufferSize: {buffer.Length})");

        handler.Send(buffer);
        // закрываем сокет
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
    }
}