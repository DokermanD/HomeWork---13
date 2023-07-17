using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Threading.Channels;

namespace HomeWork___13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch swSer = new Stopwatch();
            
            F f = new F();

            //Сереализация объекта
            Console.WriteLine("Сереализация объекта");
            swSer.Start();
            for (int i = 0; i < 1000; i++)
            {
                Castom.Ser(f.Get()); 
            }
            swSer.Stop();
            Console.WriteLine($"Затрачено времени на 1000 итераций - {swSer.ElapsedMilliseconds.ToString()}\n{Castom.Serial}\n");

            //Десереализация объекта
            Console.WriteLine("Десереализация объекта");
            swSer.Restart();
            for (int i = 0; i < 1000; i++)
            {
                Castom.DeSer(); 
            }
            swSer.Stop();
            Console.WriteLine($"Затрачено времени на 1000 итераций - {swSer.ElapsedMilliseconds.ToString()}\n{Castom.DeSerial}\n");


            //------------------------------------------------------------------------------------------------//

            string stringSerialize = string.Empty;
            var v = f.Get();

            //Сереализация объекта (Newtonsoft.Json)
            Console.WriteLine("Сереализация объекта (Newtonsoft.Json)");
            swSer.Restart();
            for (int i = 0; i < 1000; i++)
            {   
               stringSerialize = JsonConvert.SerializeObject(v);
            }
            swSer.Stop();
            Console.WriteLine($"Затрачено времени на 1000 итераций - {swSer.ElapsedMilliseconds.ToString()}\n{stringSerialize}\n");

            F? deser = new F();
            //Десереализация объекта  (Newtonsoft.Json)
            Console.WriteLine("Десереализация объекта (Newtonsoft.Json)");
            swSer.Restart();
            for (int i = 0; i < 1000; i++)
            {
                deser = JsonConvert.DeserializeObject<F>(stringSerialize);
            }
            swSer.Stop();
            Console.WriteLine($"Затрачено времени на 1000 итераций - {swSer.ElapsedMilliseconds.ToString()}\n");
            var deserType = deser?.GetType();
            var deserFilds = deserType?.GetFields();
            foreach (var item in deserFilds)
            {
                Console.WriteLine($"{item.Name}:{item.GetValue(deser)}");
            }

            Console.ReadKey();

        }
    }
}