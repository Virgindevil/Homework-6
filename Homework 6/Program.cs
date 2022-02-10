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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Menu();
        }

        static void ReadingFile()
        {
            Console.Clear();
            string path = "db.txt";
            if (File.Exists(path) == true)
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Unicode))
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
            }
            else
            {
                Console.WriteLine($"\nФайл ещё не был создан...");
            }
            

            Console.WriteLine($"\n\n\nДля выхода в меню нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void WritingFile()
        {
            string path = "db.txt";
            int id = 1;
            Console.Clear();

            if (File.Exists(path) == true)
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Unicode))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        id++;
                    }
                }
            }

            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Unicode))
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
        }

        static void Menu()
        {
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
                        ReadingFile();
                        break;

                    case "2":
                        WritingFile();
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
