using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tutorials.CSharp8.App
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var car in Cars)
            {
                Console.WriteLine($"{car.GetType().Name}: {CalculateFee(car).ToString("C")}");
            }

            var someArray = new string[] { "hello", "world", "!" };

            Console.WriteLine($"{someArray.Select(x => x?.ToUpper()).First()}");

            Console.WriteLine($"{someArray.SelectToUpper().First()}");

            Console.ReadKey();
        }

        private static decimal CalculateFee(object car) => car switch
        {
            Car { Passengers: 1 } => 2.00m + 1.25m,
            Car { Passengers: 2 } => 2.00m + 0.50m,
            Car _ => 2.00m,

            Taxi t when t.LicensePlate.StartsWith("WA") => 1.50m,
            Taxi _ => 3.50m,

            SemiTruck s when s.Axles >= 8 => 6.00m,
            SemiTruck _ => 5.00m,

            _ => 4.00m
        };

        public static IEnumerable<IVehicle> Cars => new IVehicle[]
        {
            new Car { ExteriorColor = Color.Blue, LicensePlate = "VA 123-7890" },
            new Taxi { ExteriorColor = Color.Yellow, LicensePlate = "WA YWC-1564" },
            new Car { ExteriorColor = Color.Blue, LicensePlate = "WA KJH-6782" },
            new SemiTruck { ExteriorColor = Color.Green, Axles = 9, LicensePlate = "WA JHU-8769" },
            new SemiTruck { ExteriorColor = Color.Green, Axles = 3, LicensePlate = "NC JIU-8798" }
        };

        public interface IVehicle
        {
            Color ExteriorColor { get; set; }
            string LicensePlate { get; set; }
            int Passengers { get; set; }
        }

        public enum Color
        {
            Blue,
            Red,
            Green,
            Gold,
            Yellow
        }

        public abstract class VehicleBase : IVehicle
        {
            public Color ExteriorColor { get; set; }
            public string LicensePlate { get; set; }
            public int Passengers { get; set; }
        }

        public class SemiTruck : VehicleBase, IVehicle
        {
            public int Axles { get; set; }
        }

        public class Taxi : VehicleBase, IVehicle
        { }


        public class Car : VehicleBase, IVehicle
        { }
    }

    public static class StringExtensions
    {
        public static IEnumerable<string> SelectToUpper(this string[] someArray)
        {
            foreach (var x in someArray)
            {
                yield return x?.ToUpper();
            }
        }
    }
}
