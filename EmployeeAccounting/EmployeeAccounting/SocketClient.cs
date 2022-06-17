using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Script;
using Timer = System.Timers.Timer;

namespace EmployeeAccounting;

public static class SocketClient
{
    private static Grid _grid;
    private static Label _label;
    private static Timer _attemptTimer;
    private static bool _agentAnswer = true;
    private static int _agentAttempts;

    private static void ShowLoadingScreen(bool enable)
    {
        _grid.Dispatcher.Invoke(() =>
        {
            _grid.Visibility = enable ? Visibility.Visible : Visibility.Hidden;
        });
    }

    private static void SetLabelText(string text)
    {
        _label.Dispatcher.Invoke(() =>
        {
            _label.Content = text;
        });
    }

    public static void InitiateLoadingScreen(Grid grid, Label label)
    {
        _grid = grid;
        _label = label;
    }

    public static void RefreshAgent()
    {
        RemoteEvent("Agent:Refresh");
    }

    public static void StartAgent()
    {
        RefreshAgent();

        _attemptTimer = new Timer(10000);

        _attemptTimer.Elapsed += (sender, args) =>
        {
            if (_agentAttempts >= 5)
            {
                _attemptTimer.Stop();
                _attemptTimer.Dispose();

                var answer = MessageBox.Show("Превышено время ожидания сервера. \rПовторите попытку или обратитесь к администратору сервера.", 
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                if (answer == MessageBoxResult.OK)
                {
                    Utils.MainWindow.Dispatcher.Invoke(() =>
                    {
                        Utils.MainWindow.Close();
                    });
                }
            }

            if (_agentAnswer == false)
            {
                RefreshAgent();

                _agentAttempts++;

                SetLabelText($"Попытка подключиться к серверу {_agentAttempts}..");

                return;
            }

            _agentAttempts = 0;
            _agentAnswer = false;

            SetLabelText($"Ожидание сервера..");
        };

        _attemptTimer.Start();
    }

    public static void NotifyAgent()
    {
        _agentAnswer = true;
    }

    public static async void RemoteEvent(string eventName, params object[] args)
    {
        try
        {
            var ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var uploadData = new UploadData(eventName, args);
            var json = JsonConvert.SerializeObject(uploadData);

            ShowLoadingScreen(true);

            await socket.ConnectAsync(ipPoint);

            var data = Encoding.Unicode.GetBytes(json);

            socket.Send(data);

            data = new byte[104857600];

            await using var networkStream = new NetworkStream(socket);

            var bytes = networkStream.Read(data);
            var message = Encoding.Unicode.GetString(data, 0, bytes);

            var responseData = JsonConvert.DeserializeObject<UploadData>(message);

            ClientScripts.Invoke(responseData.Name, responseData.Args);

            socket.Shutdown(SocketShutdown.Both);
            socket.Close();

            ShowLoadingScreen(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.Read();
    }
}