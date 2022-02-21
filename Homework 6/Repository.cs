using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6
{
    class Repository
    {
        #region Поля
                
        private string path; // путь к файлу с данными        
        string[] titles; // массив, храняий заголовки полей. используется в PrintDbToConsole
        public List<Worker> worker ;
        bool LoadCheck = true;
        
        #endregion

        #region Конструктор
        public Repository(string Path)
        {
            path = Path; // Сохранение пути к файлу с данными            
            titles = new string[]                   // Инициализируем заголовки
            {"ID"," Время"," ФИО"," Возраст","Рост", "Дата рождения","Место рождения"};      
            this.worker = new List<Worker>();           

        }
        #endregion

        #region Меню
        public void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n\t\t\t1 — вывести данные рабочего файла на экран. ");
                Console.WriteLine("\t\t\t2 — заполнить данные и добавить новую запись в конец рабочего файла. ");
                Console.WriteLine("\t\t\t3 — поиск записи по ID в рабочем файле. ");
                Console.WriteLine("\t\t\t4 — создать копию рабочего файла (с заменой).");
                Console.WriteLine("\t\t\t5 — удалить рабочий файл. ");
                Console.WriteLine("\t\t\t6 — редактировать запись с определённым ID в рабочем файле. ");
                Console.WriteLine("\t\t\t7 — показать данные из рабочего файла в интервале дат. ");
                Console.WriteLine("\t\t\t8 — сортировать данные по дате из рабочего файла на экране. (убывание и возрастание) ");
                Console.WriteLine("\t\t\t0 - выход.");                              
                Console.Write("\n\t\t\t\t\t\t");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowOnScreen();
                        break;

                    case "2":
                        AddPerson();
                        break;

                    case "3":
                        if (File.Exists(path) == true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID:");
                            SortID(Console.ReadLine());
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }
                        

                    case "4":
                        CopyFile();
                        break;

                    case "5":
                        DelFile();
                        break;

                    case "6":
                        if (File.Exists(path) == true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID:");
                            RedactByID(Console.ReadLine());
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }

                    case "7":
                        if (File.Exists(path) == true)
                        {
                            Console.Clear();
                            Console.WriteLine("Введите начальную дату для импорта(формат dd-mm-yyyy и/или 00:00):");
                            string date1 = Console.ReadLine();
                            Console.WriteLine("Введите конечную дату для импорта(формат dd-mm-yyyy и/или 00:00):");
                            string date2 = Console.ReadLine();
                            DataTimesSort(date1, date2);
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }

                    case "8":
                        if (File.Exists(path) == true)
                        {
                            Console.Clear();
                            DataSort();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }
                        

                    case "0":
                        return;

                    default:
                        break;
                }
            }
        }


        #endregion

        #region Методы Меню
        public void ShowOnScreen()
        {
            Console.Clear();
            if (File.Exists(path) == true)
            {
                Load();
                PrintWorker();
            }

            else
            {
                DontExist();
            }
        }
        public void AddPerson()
        {
            Console.Clear();
            if (File.Exists(path) != true)
            {
                worker.Clear();
            }
            else
            {
                if (LoadCheck == true)
                {
                    Load();
                    LoadCheck = false;
                }
            }            
                             
            

            char key = 'д';
            do
            {
                Console.Clear();
                string ID = $"{NumberId()+1}";
                string Date = $"{DateTime.Now}";
                Console.Write("Введите фамилию имя отчество: ");
                string Name = $"{Console.ReadLine()}";
                Console.Write("Введите возраст: ");
                string Age = $"{Console.ReadLine()}";
                Console.Write("Введите рост: ");
                string Height = $"{Console.ReadLine()}";
                Console.Write("Введите дату рождения: ");
                string Dbirth = $"{Console.ReadLine()}";
                Console.Write("Введите место рождения: ");
                string Pbirth = $"{Console.ReadLine()}";

                AddItem(ID, Date, Name, Age, Height, Dbirth, Pbirth);
                SaveNewPerson();
                
                Console.Write("Продожить н/д"); key = Console.ReadKey(true).KeyChar;

            } while (char.ToLower(key) == 'д');
        }
        public void SortID(string idnumber)
        {
            Load();
            int id = int.Parse(idnumber);
            int j = 0;
            if (id <= NumberId() && id > 0)
            {                
                for (int i = 0; i < worker.Count; i++)
                {
                    Console.Clear();
                    Console.WriteLine($"{titles[0]}" +
                        $" {titles[1],15}" +
                        $" {titles[2],20}" +
                        $" {titles[3],20}" +
                        $" \t{titles[4]}" +
                        $" \t{titles[5],5}" +
                        $" \t{titles[6],5}\n");
                    Console.WriteLine(worker[i].Print());
                    j++;
                    if (j == id)
                    {
                        Console.WriteLine("\nДля выхода в меню нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверно введён ID");
                Console.ReadKey();
            }
        }        
        public void CopyFile()
        {
            Console.Clear();
            if (File.Exists(path) == true)
            {
                if (File.Exists("copyfile.txt") == true)
                {
                    File.Delete("copyfile.txt");
                }
                File.Copy(path, "copyfile.txt");
                Console.WriteLine("\nФайл скопирован");
                Console.WriteLine("\nДля выхода в меню нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
            {
                DontExist();
            }
        }
        public void DelFile()
        {
            Console.Clear();
            if (File.Exists(path) == true)
            {
                File.Delete(path);
                Console.WriteLine("\nФайл удалён");
                Console.WriteLine("\nДля выхода в меню нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
            {
                DontExist();
            }
        }
        public void RedactByID(string idnumber)
        {
            int id = int.Parse(idnumber);
            Load();
            Console.Clear();
            if (id <= NumberId() && id > 0)
            {
                Worker temp = new Worker();      // Создание временной переменной
                temp.ID = $"{id}";
                temp.Date = $"{DateTime.Now}";
                Console.Write("Введите новую фамилию имя отчество: ");
                temp.Name = $"{Console.ReadLine()}";
                Console.Write("Введите новый возраст: ");
                temp.Age = $"{Console.ReadLine()}";
                Console.Write("Введите новый рост: ");
                temp.Height = $"{Console.ReadLine()}";
                Console.Write("Введите новую дату рождения: ");
                temp.Dbirth = $"{Console.ReadLine()}";
                Console.Write("Введите новое место рождения: ");
                temp.Pbirth = $"{Console.ReadLine()}";

                worker[id - 1] = temp;
                SaveNewPerson();
            }
            else
            {
                Console.WriteLine("Неверно введён ID");
                Console.ReadKey();
            }
        }
        public void DataTimesSort(string date1, string date2)
        {
            DateTime startDate = Convert.ToDateTime(date1);
            DateTime endDate = Convert.ToDateTime(date2);

            using (StreamReader sr = new StreamReader(path))
            {                
                while (!sr.EndOfStream)
                {
                    string[] data = sr.ReadLine().Split('#');
                    DateTime data1 = Convert.ToDateTime(data[1]);

                    if (data1 >= startDate && data1 <= endDate)               // Проверка на заданный диапазон дат
                    {                        
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
            Console.WriteLine("\nДля выхода в меню нажмите любую клавишу...");
            Console.ReadKey();
        }
        public void DataSort()
        {
            Load();
            Console.WriteLine("1 - В порядке убывания | 2 - В порядке возрастания");
            switch (Console.ReadLine())
            {
                case "1":
                    worker = worker.OrderByDescending(e => e.Date)                        
                        .ToList();

                    PrintWorker();
                    break;

                case "2":
                    worker = worker.OrderBy(e => e.Date)                        
                        .ToList();

                    PrintWorker();
                    break;

                default:
                    break;
            }
        }

        #endregion

        #region Второстепенные методы


        public int NumberId()
        {
            int idcount = 0;
            for (int j = 0; j < worker.Count; j++)
            {
                idcount++;
            }             
            return idcount;
        }

        public void DontExist()
        {
            Console.WriteLine($"\nФайл ещё не был создан...");
            Console.WriteLine($"\nДля возврата в меню нажмите любую клавишу...");
            Console.ReadKey();
        }

        public void AddLine(Worker contentLine)
        {
            worker.Add(contentLine);
        }

        public void AddItem(params string[] data)
        {
            AddLine(new Worker(data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
        }

        public void SaveNewPerson()
        {
            File.Delete(path);

            for (int i = 0; i < NumberId(); i++)               // Сохраняем тело файла
                {
                    string temp = String.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}",
                                                    this.worker[i].ID,
                                                    this.worker[i].Date,
                                                    this.worker[i].Name,
                                                    this.worker[i].Age,
                                                    this.worker[i].Height,
                                                    this.worker[i].Dbirth,
                                                    this.worker[i].Pbirth);
                File.AppendAllText(path, $"{temp}\n");
            }
            
        }

        public void Load()
        {
            //Очищаем список перед загрузкой нового файла
            worker.Clear();

            using (StreamReader sr = new StreamReader(path))
            {
                //string line;
                while (!sr.EndOfStream)
                {
                    string[] data = sr.ReadLine().Split('#');
                    AddLine(new Worker(data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
                }
            }
        }

        public void PrintWorker()
        {
            Console.Clear();
            Console.WriteLine($"{titles[0]}" +
                $" {titles[1],15}" +
                $" {titles[2],20}" +
                $" {titles[3],20}" +
                $" \t{titles[4]}" +
                $" \t{titles[5],5}" +
                $" \t{titles[6],5}\n");
            for (int i = 0; i < worker.Count; i++)
            {
                Console.WriteLine(worker[i].Print());
            }
            Console.WriteLine("\nДля выхода в меню нажмите любую клавишу...");
            Console.ReadKey();
        }
        

        #endregion

    }
}
