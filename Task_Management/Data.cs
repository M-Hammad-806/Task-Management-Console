using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Task_Management
{
    public static class Data
    {
        //public string sourceLocation = System.AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "tasks.json");


        public  static async Task<List<TaskEntity>> GetAll()
        {
            if (!File.Exists(filePath))
            {
                return new List<TaskEntity>();
            }
            
            string json = await File.ReadAllTextAsync(filePath);
            var results = JsonSerializer.Deserialize<List<TaskEntity>>
                (json,
                new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    }
                });
            return results ?? new List<TaskEntity>();
        }
         public static async Task Save(List<TaskEntity> tasks)
        {
            
            string json = JsonSerializer.Serialize(tasks,
                new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = { new JsonStringEnumConverter() }
                });
            await File.WriteAllTextAsync(filePath, json);
        }
    }
}
