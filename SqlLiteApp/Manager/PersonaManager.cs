using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SqlLiteApp.Model;

namespace SqlLiteApp.Manager
{
    public class PersonaManager
    {
        static object locker = new object();
        SQLiteConnection connection;

        public SQLiteConnection Connect()
        {
            string fileName = "SqlLiteApp.db3";
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(path, fileName);
            return new SQLiteConnection(fullPath);
        }

        public IEnumerable<ClassPersona> GetAll()
        {
            try
            {
                lock (locker)
                {
                    return (from i in connection.Table<ClassPersona>() select i).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ClassPersona GetByName(string name)
        {
            try
            {
                lock (locker)
                {
                    return connection.Table<ClassPersona>().FirstOrDefault(x => x.Name == name);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ClassPersona GetById(int idPerson)
        {
            try
            {
                lock (locker)
                {
                    return connection.Table<ClassPersona>().ElementAt(idPerson);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SaveChanges(ClassPersona person)
        {
            try
            {
                lock (locker)
                {
                    return (person.Id == 0) ? connection.Insert(person) : connection.Update(person);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeteleById(int IdPerson)
        {
            try
            {
                lock (locker)
                {
                    return connection.Delete<ClassPersona>(IdPerson);
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public PersonaManager()
        {
            connection = Connect();
            connection.CreateTable<ClassPersona>();
        }
    }
}