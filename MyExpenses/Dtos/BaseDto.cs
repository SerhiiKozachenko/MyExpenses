using System;

namespace MyExpenses.Dtos
{
    public class BaseDto
    {
        [SQLite.PrimaryKey]
        public String Id { get; set; } 
    }
}