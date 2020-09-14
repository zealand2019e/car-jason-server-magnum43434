using System;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace CarJsonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello car jason exercise!");

            // get IP address

            IPAddress ip = GetIpFromHost.GetIp("google.com");
            IPAddress ip1 = GetIpFromHost.GetIp("cnn.com");
            IPAddress ip2 = GetIpFromHost.GetIp("dr.dk");
            IPAddress ip3 = GetIpFromHost.GetIp("localhost");

            // Plain text communication

            // jason serialization and deserialization - newtonsoft.json
            Console.WriteLine("Model?");
            string model = Console.ReadLine();
            Console.WriteLine("Color?");
            string color = Console.ReadLine();
            Console.WriteLine("registration number? ");
            string regNr = Console.ReadLine();

            Car car = new Car {Color = color, Model = model, RegNr = regNr};
            string carJasonString = JsonConvert.SerializeObject(car);
            Console.WriteLine("Json format: " + carJasonString);
            car = JsonConvert.DeserializeObject<Car>(carJasonString);
            Console.WriteLine(car.Model.ToUpper() + " " + car.Color.ToUpper() + " "+car.RegNr);


            // Jason communication 2 classes

            //XML communication (optional)
        }
    }
}
