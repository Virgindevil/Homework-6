using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int id = 1;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            DirectoryInfo directoryInfo = new DirectoryInfo(@"c:\Homework6");
            if (directoryInfo.Exists != true)
            {
                Directory.CreateDirectory(@"c:\Homework6");
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"\t\t\t\t\t1 — вывести данные на экран. ");
                Console.WriteLine("\t\t\t2 — заполнить данные и добавить новую запись в конец файла. ");
                Console.WriteLine("\t\t\t\t\t\t0 - выход.");                
                Console.Write("\n\t\t\t\t\t\t");
                switch (Console.ReadLine())
                {
                    case "1":

                        Console.Clear();

                        using (StreamReader sr = new StreamReader(@"c:\Homework6\db.txt", Encoding.Unicode))
                        {
                            string line;
                            
                            Console.WriteLine($"{"ID"} {" Время",15} {" ФИО",20} {" Возраст",20} {"\tРост"} {"\tДата рождения",5} {"\tМесто рождения",5}");
                            Console.WriteLine();
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] data = line.Split('#');
                                Console.WriteLine($" {data[0]}" +
                                                  $"  {data[1]}" +
                                                  $"  {data[2]}" +
                                                  $"\t{data[3]}" +
                                                  $"\t{data[4]}" + 
                                                  $"\t{data[5]}" +
                                                  $"\t{data[6]}");
                                
                            }
                        }

                        Console.WriteLine($"\n\n\nДля выхода в меню нажмите любую клавишу...");
                        Console.ReadKey();
                        break;

                    case "2":

                        Console.Clear();

                        if (File.Exists(@"c:\Homework6\db.txt") == true)
                        {
                            using (StreamReader sr = new StreamReader(@"c:\Homework6\db.txt", Encoding.Unicode))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    id++;
                                }
                            }
                        }

                        using (StreamWriter sw = new StreamWriter(@"c:\Homework6\db.txt", true, Encoding.Unicode))
                        {
                            char key = 'д';

                            do
                            {
                                Console.Clear();
                                string note = string.Empty;
                                note += $"{id}#";
                                id++;

                                string now = DateTime.Now.ToString();
                                note += $"{now}#";

                                Console.Write("Введите фамилию имя отчество: ");
                                note += $"{Console.ReadLine()}#";

                                Console.Write("Введите возраст: ");
                                note += $"{Console.ReadLine()}#";

                                Console.Write("Введите рост: ");
                                note += $"{Console.ReadLine()}#";

                                Console.Write("Введите дату рождения: ");
                                note += $"{Console.ReadLine()}#";

                                Console.Write("Введите место рождения: ");
                                note += $"{Console.ReadLine()}";

                                sw.WriteLine(note);

                                Console.Write("Продожить н/д"); key = Console.ReadKey(true).KeyChar;

                            } while (char.ToLower(key) == 'д');
                        }
                        break;

                    case "0":
                        return;

                    default:
                        break;
                }
            }
        }
    }
}
