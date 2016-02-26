using System;
using System.IO;
using Windows.Storage;
using SQLite;

namespace MyExpenses.StartUp
{
    public class DbConfig
    {
        public const String DB_NAME = "MyExpenses.sqlite";

        public static String DB_PATH = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, DB_NAME));

        public static void Init()
        {
            var dbHelper = new DataBaseHelper();
            if (!dbHelper.CreateIfNotExists(DB_PATH))
            {
                throw new InvalidDataException("Error on db create");
            }
        } 
    }
}