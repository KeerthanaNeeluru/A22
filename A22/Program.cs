using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A22
{
    public class Program
    {
        public static void MapProperties<T, U>(T Source, U destination)
        {
            Type sourceType = typeof(T);
            Type destinationType = typeof(U);

            PropertyInfo[] s_prop=sourceType.GetProperties();
            PropertyInfo[] d_prop= destinationType.GetProperties();

            foreach(var s_proper in s_prop)
            {
                PropertyInfo Dproperty=Array.Find(d_prop,p=>p.Name==s_proper.Name&&p.PropertyType==s_proper.PropertyType);
                if (Dproperty!=null && Dproperty.CanWrite)
                {
                    Dproperty.SetValue(destination, s_proper.GetValue(Source));
                }
                else
                {
                    Console.WriteLine($"Ignore property : {s_proper.Name}");
                }
            }
            foreach(var d_proper in d_prop)
            {
                PropertyInfo Sprop = Array.Find(s_prop, p => p.Name == d_proper.Name && p.PropertyType == d_proper.PropertyType);
                if (Sprop == null)
                {
                    Console.WriteLine($"Ignoring Property: {d_proper.Name}");
                }
            }
        }

        static void Main(string[] args)
        {
            Destination des = new Destination();
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the values for the source class");
                    Console.Write("ID : ");
                    int id = int.Parse(Console.ReadLine());
                    Console.WriteLine("Name : ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Designation : ");
                    string designation = Console.ReadLine();

                    Source src = new Source()
                    {
                        Id = id,
                        Name = name,
                        Designation = designation
                    };
                    MapProperties(src, des);

                    Console.WriteLine("Destination values");
                    Console.WriteLine($"ID : {des.Id}");
                    Console.WriteLine($"Name : {des.Name}");
                    Console.WriteLine($"Designation : {des.Designation}");
                    Console.WriteLine($"Age : {des.Age}");
                }
                catch (Exception e) { Console.WriteLine("ERRO !!!" + e.Message); }
                Console.WriteLine("If you want to continue y/n");
                string ip = Console.ReadLine();
                if (ip.ToLower() != "y")
                {
                    break;
                }
            }
        }
    }
}
