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
        public List<Worker> worker;

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
                if (File.Exists(path))
                {
                    LoadList();
                }                
                Console.Clear();
                Console.WriteLine("\n\t\t\t1 — вывести данные рабочего файла на экран. ");
                Console.WriteLine("\t\t\t2 — создать записть и добавить новую запись в конец рабочего файла. ");
                Console.WriteLine("\t\t\t3 — поиск записи по ID в рабочем файле. ");
                Console.WriteLine("\t\t\t4 — удалить строчку, введя её ID. ");
                Console.WriteLine("\t\t\t5 — редактировать запись с определённым ID в рабочем файле. ");
                Console.WriteLine("\t\t\t6 — показать данные из рабочего файла в интервале дат. ");
                Console.WriteLine("\t\t\t7 — сортировать данные по дате из рабочего файла на экране. (убывание и возрастание) ");
                Console.WriteLine("\t\t\t0 - выход.");                              
                Console.Write("\n\t\t\t\t\t\t");
                switch (Console.ReadLine())
                {
                    case "1":
                        ShowListOnScreen();
                        break;

                    case "2":
                        CreateAndAddPerson();
                        break;

                    case "3":
                        if (File.Exists(path))
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID:");
                            ShowIDLine(Console.ReadLine());
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }
                    
                    case "4":
                        if (File.Exists(path))
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID:");
                            DeleteLine(Console.ReadLine());
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }
                      

                    case "5":
                        if (File.Exists(path))
                        {
                            Console.Clear();
                            Console.WriteLine("Введите ID:");
                            RedactByIDNumber(Console.ReadLine());
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }

                    case "6":
                        if (File.Exists(path))
                        {
                            Console.Clear();
                            Console.WriteLine("Введите начальную дату для импорта(формат dd-mm-yyyy и/или 00:00):");
                            string date1 = Console.ReadLine();
                            Console.WriteLine("Введите конечную дату для импорта(формат dd-mm-yyyy и/или 00:00):");
                            string date2 = Console.ReadLine();
                            ShowDataBetweenTwoTimes(date1, date2);
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            DontExist();
                            break;
                        }

                    case "7":
                        if (File.Exists(path))
                        {
                            Console.Clear();
                            SortDataByTime();
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

        /// <summary>
        /// выводит на экран записи из списка
        /// </summary>
        public void ShowListOnScreen()
        {
            Console.Clear();
            if (File.Exists(path))
            {                
                PrintWorker();
            }

            else
            {
                DontExist();
            }
        }


        /// <summary>
        /// метод добавления записи с автосохранением в файл
        /// </summary>
        public void CreateAndAddPerson()
        {
            Console.Clear();
            int id = 0;
            if (!File.Exists(path)) //Дополнительная проверка на записи в списке
            {
                worker.Clear(); //отчистка списка, если в нём что-то есть, а файла нет
            }
            else
            {
                id = int.Parse(worker[worker.Count-1].ID);
            }

            char key = 'д';
            do
            {
                Console.Clear();
                string ID = $"{id + 1}";
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

                AddData(ID, Date, Name, Age, Height, Dbirth, Pbirth);
                SaveNewList();
                
                Console.Write("Продожить н/д"); key = Console.ReadKey(true).KeyChar;

            } while (char.ToLower(key) == 'д');
        }


        /// <summary>
        /// сортировка по ID и вывод на экран нужной строки
        /// </summary>
        /// <param name="idnumber"></param>
        public void ShowIDLine(string idnumber)
        {
            int id = int.Parse(idnumber);
            if (id <= worker.Count && id > 0)
            {
                Console.Clear();
                Console.WriteLine($"{titles[0]}" +
                        $" {titles[1],15}" +
                        $" {titles[2],20}" +
                        $" {titles[3],20}" +
                        $" \t{titles[4]}" +
                        $" \t{titles[5],5}" +
                        $" \t{titles[6],5}\n");
                Console.WriteLine(worker[id-1].Print());
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Неверно введён ID");
                Console.ReadKey();
            }
        }       


       /// <summary>
       /// Удаляет строчку
       /// </summary>
       /// <param name="linenumber">номер строчки</param>
        public void DeleteLine(string linenumber)
        {
            for (int i = 0; i < worker.Count; i++)
            {
                if (linenumber == worker[i].ID)
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
                    Console.WriteLine("\nДанная строчка будет удалена...");
                    Console.ReadKey();

                    Console.WriteLine("Вы хотите продолжить? 1 - да | 2 - нет");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            worker.RemoveAt(i);
                            File.Delete(path);
                            SaveNewList();
                            Console.WriteLine("\nСтрочка удалена");
                            Console.ReadKey();
                            return;

                        case "2":

                            return;

                        default:
                            return;
                    }
                }
            }
            Console.WriteLine("Неверно введён ID");
            Console.ReadKey();
        }


        /// <summary>
        /// редактировать запись с нужным номером ID
        /// </summary>
        /// <param name="idnumber"></param>
        public void RedactByIDNumber(string linenumber)
        {
            int line = int.Parse(linenumber);        
            if (line <= worker.Count && line > 0)
            {

                Console.Clear();
                Console.WriteLine($"{titles[0]}" +
                        $" {titles[1],15}" +
                        $" {titles[2],20}" +
                        $" {titles[3],20}" +
                        $" \t{titles[4]}" +
                        $" \t{titles[5],5}" +
                        $" \t{titles[6],5}\n");
                Console.WriteLine(worker[line - 1].Print());
                Console.WriteLine("\nДанные до редактирования\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();

            }
            Console.Clear();
            if (line <= worker.Count && line > 0)
            {
                Worker temp = new Worker();      // Создание временной переменной
                temp.ID = $"{line}";
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

                worker[line - 1] = temp;
                SaveNewList();
            }
            else
            {
                Console.WriteLine("Неверно введён ID");
                Console.ReadKey();
            }
        }


        /// <summary>
        /// вывод на экран записей в диапазоне между двумя датами
        /// </summary>
        /// <param name="date1">нижний диапазон</param>
        /// <param name="date2">верхний диапазон</param>
        public void ShowDataBetweenTwoTimes(string date1, string date2)
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


        /// <summary>
        /// сортировка записей по дате (убывание и возрастание)
        /// </summary>
        public void SortDataByTime()
        {
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

        /// <summary>
        /// строки если файл не существует
        /// </summary>
        public void DontExist()
        {
            Console.WriteLine($"\nФайл ещё не был создан...");
            Console.WriteLine($"\nДля возврата в меню нажмите любую клавишу...");
            Console.ReadKey();
        }


        /// <summary>
        /// метод добавления записи в список
        /// </summary>
        /// <param name="contentLine"></param>
        public void AddLine(Worker contentLine)
        {
            worker.Add(contentLine);
        }


        /// <summary>
        /// метод добавления данных сотрудника для внесения в список
        /// </summary>
        /// <param name="data"></param>
        public void AddData(params string[] data)
        {
            AddLine(new Worker(data[0], data[1], data[2], data[3], data[4], data[5], data[6]));
        }


        /// <summary>
        /// сохранение записей в рабочий файл
        /// </summary>
        public void SaveNewList()
        {
            File.Delete(path);

            for (int i = 0; i < worker.Count; i++)               // Сохраняем тело файла
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


        /// <summary>
        /// загрузить данные из файла в список
        /// </summary>
        public void LoadList()
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


        /// <summary>
        /// вывести на экран список с записями
        /// </summary>
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
