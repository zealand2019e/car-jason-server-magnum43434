using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ModelLib;
using Newtonsoft.Json;

namespace CarJsonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // get IP address

            //IPAddress ip = GetIpFromHost.GetIp("google.com");
            //IPAddress ip1 = GetIpFromHost.GetIp("cnn.com");
            //IPAddress ip2 = GetIpFromHost.GetIp("dr.dk");
            //IPAddress ip3 = GetIpFromHost.GetIp("localhost");

            Server.Start();
        }
    }

    class Server
    {
        public static void Start()
        {
            TcpListener server = null;
            int clientsConnected = 0;
            try
            {
                Int32 port = 10001;
                IPAddress localAddress = IPAddress.Loopback;

                server = new TcpListener(localAddress, port);

                server.Start();

                while (true)
                {
                    Console.WriteLine("Waiting for a connection");

                    TcpClient socket = server.AcceptTcpClient();
                    clientsConnected++;
                    Console.WriteLine($"Client {clientsConnected} has connected to the server");
                    Task.Run(() => { DoClient(socket, $"Client {clientsConnected}"); });
                    //socket.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        private static void DoClient(TcpClient socket, string client)
        {
            Byte[] bytes = new byte[256];
            string data = null;

            NetworkStream ns = socket.GetStream();

            int i;
            i = ns.Read(bytes, 0, bytes.Length);
            data = Encoding.ASCII.GetString(bytes, 0, i);
            Console.WriteLine($"Received from {client}: {data}");

            AutoSale sale = JsonConvert.DeserializeObject<AutoSale>(data);
            data = "Name: " + sale.name + "| Address: " + sale.address + "| Cars: " + sale.cars.Count;

            Byte[] msg = Encoding.ASCII.GetBytes(data);

            ns.Write(msg, 0, msg.Length);
            Console.WriteLine($"Sent to {client}: {data}");
        }
    }
}
