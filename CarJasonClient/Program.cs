using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ModelLib;
using Newtonsoft.Json;

namespace CarJasonClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello car jason exercise!");

            // Plain text communication

            // jason serialization and deserialization - newtonsoft.json
            //Console.WriteLine("Model?");
            //string model = Console.ReadLine();
            //Console.WriteLine("Color?");
            //string color = Console.ReadLine();
            //Console.WriteLine("registration number? ");
            //string regNr = Console.ReadLine();

            //Car car = new Car { Color = color, Model = model, RegNr = regNr };
            //Car car = new Car { Color = "red", Model = "Tesla", RegNr = "EL23400" };
            List<Car> cars = new List<Car>();
            cars.Add(new Car { Color = "red", Model = "Tesla", RegNr = "EL23400" });
            cars.Add(new Car { Color = "black", Model = "Tesla", RegNr = "EL32400" });
            AutoSale sale = new AutoSale {name = "Magnum", address = "Nørretorv 53, 2, 1, 4100 Ringsted", cars = cars};

            string carJasonString = JsonConvert.SerializeObject(sale);
            Console.WriteLine("Json format: " + carJasonString);
            //car = JsonConvert.DeserializeObject<Car>(carJasonString);
            //Console.WriteLine(car.Model.ToUpper() + " " + car.Color.ToUpper() + " " + car.RegNr);

            Client.Start("localhost", carJasonString);
            // Jason communication 2 classes

            //XML communication (optional)
        }
    }

    class Client
    {
        public static void Start(string server, string userData)
        {
            try
            {
                Int32 port = 10001;
                TcpClient socket = new TcpClient(server, port);
                NetworkStream ns = null;

                Byte[] data = Encoding.ASCII.GetBytes(userData);

                ns = socket.GetStream();

                ns.Write(data, 0, data.Length);

                Console.WriteLine("Sent: {0}", userData);

                data = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = ns.Read(data, 0, data.Length);
                responseData = Encoding.ASCII.GetString(data, 0, bytes);
                Console.WriteLine("Received: {0}", responseData);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
