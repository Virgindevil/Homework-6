using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_6
{
    struct Worker
    {        

        #region Автосвойства
                
        public string ID { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// ФИО
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// Рост
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// Дата Рождения
        /// </summary>
        public string Dbirth { get; set; }
        /// <summary>
        /// Место Рождения
        /// </summary>
        public string Pbirth { get; set; }

        #endregion        

        #region Конструктор
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="date">Дата создания</param>
        /// <param name="name">ФИО</param>
        /// <param name="age">Возраст</param>
        /// <param name="height">Рост</param>
        /// <param name="dbirth">Дата рождения</param>
        /// <param name="pbirth">Место рождения</param>
        public Worker(string id, string date, string name, string age, string height, string dbirth, string pbirth)
        {
            this.ID = id;
            this.Date = date;
            this.Name = name;
            this.Age = age;
            this.Height = height;
            this.Dbirth = dbirth;
            this.Pbirth = pbirth;
        }
        #endregion

        public string Print()
        {
            return $"{this.ID} {this.Date} {this.Name} \t{this.Age} \t{this.Height} \t{this.Dbirth,5} \t{this.Pbirth,5}";
        }
    }
}
