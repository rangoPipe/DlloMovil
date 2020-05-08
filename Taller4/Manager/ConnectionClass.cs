using Logic.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Manager
{
    public class ConnectionClass
    {
        static object locker = new object();
        SQLiteConnection connection;

        public SQLiteConnection Connect()
        {
            try
            {
                string fileName = "SqlLiteApp.db3";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string fullPath = Path.Combine(path, fileName);
                var data = new SQLiteConnection(fullPath);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PersonClass GetById(int idPerson)
        {
            try
            {
                lock (locker)
                {
                    return connection.Table<PersonClass>().FirstOrDefault(x => x.Id == idPerson);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SaveChanges(PersonClass person)
        {
            try
            {
                lock (locker)
                {
                    return connection.Insert(person);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public ConnectionClass()
        {
            connection = Connect();
            connection.CreateTable<PersonClass>();
        }
    }
}
