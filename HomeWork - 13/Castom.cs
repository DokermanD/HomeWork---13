using System.Text;

namespace HomeWork___13
{
    public class Castom
    {

        public static string? Serial { get; set; }
        public static string? DeSerial { get; set; }

        /// <summary>
        /// Сериализация объекта 
        /// </summary>
        /// <param name="obj"> объект для сериализации </param>
        public static void Ser(object obj)
        {
            //Получаем тип объекта
            var type = obj.GetType();
            //Получаем поля объекта с данными
            var filds = type.GetFields();

            StringBuilder sb = new StringBuilder();

            foreach (var fild in filds)
            {
                sb.Append($"{fild.Name},{fild.GetValue(obj)}\n");
            }
            Serial = sb.ToString();

            using FileStream fs = new FileStream(@"C:\Users\dmitr\Desktop\Test.csv", FileMode.Create);
            using StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(sb);

            sb.Clear();
        }

        /// <summary>
        /// Десереализация объекта
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static void DeSer()
        {
            F f2 = new F();
            //Получаем тип объекта
            var type = f2.GetType();
            //Получаем поля объекта с данными
            var filds = type.GetFields();
            //Читаем строку из файла
            var arrayStr = File.ReadAllLines(@"C:\Users\dmitr\Desktop\Test.csv");

            StringBuilder sb = new StringBuilder();
            foreach (var fild in filds)
            {
                fild.SetValue(f2, GetValueFile(fild, arrayStr));
                sb.Append($"Name - {fild.Name} Value - {fild.GetValue(f2).ToString()}\n");
            }
            DeSerial = sb.ToString();

        }

        /// <summary>
        /// Поиск в строке по имени поля его значения
        /// </summary>
        /// <param name="field">объект</param>
        /// <param name="arrayStr">строка с данными после сериализации</param>
        /// <returns></returns>
        public static int GetValueFile(System.Reflection.FieldInfo field, string[] arrayStr)
        {
            string value = string.Empty;
            var rezultIntValue = arrayStr.Where(x => x.Contains(field.Name));
            foreach (var rezult in rezultIntValue)
            {
                value = rezult.Split(',')[1];
            }
            return Convert.ToInt32(value);
        }
    }
}
