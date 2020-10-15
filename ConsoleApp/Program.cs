using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using ClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverWelcomingSocket = new TcpListener(ip, 4646);

            serverWelcomingSocket.Start();

            while (true)
            {
                TcpClient serverConnectionSocket = serverWelcomingSocket.AcceptTcpClient();
                Console.WriteLine("Starting a new connection!");
                TCPService tcpService = new TCPService(serverConnectionSocket);

                Task.Factory.StartNew(() => tcpService.SendReceiveData());
            }
        }
    }
}
