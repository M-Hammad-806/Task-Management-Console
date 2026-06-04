using System.Runtime.CompilerServices;

namespace Task_Management
{
    internal class Program
    {
        static private List<TaskEntity> Tasks;
        static async Task Main(string[] args)
        {
            while (true)
            {
                await MainMenu();
            }
        }
        static async Task MainMenu()
        {
        MainUI:
            await LoadTasks();
            Console.Clear();
            Console.WriteLine("[1]. View Tasks");
            Console.WriteLine("[2]. Add a Task");
            Console.WriteLine("[3]. Update a Task");
            Console.WriteLine("[4]. Remove a Task");
            Console.WriteLine("[5]. Search in Tasks");
            Console.WriteLine("[6]. Update a Status");
            int result;
            int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out result);
            await ActionManager(result);

           

        }
        static async Task ActionManager(int userSelectedOption)
        {
            switch (userSelectedOption)
            {
                case 1:
                    {
                            Console.Clear();
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            Console.WriteLine($"[{i + 1}]. {Tasks[i].Title}");
                            Console.WriteLine($"Description: {Tasks[i].Description}");
                            Console.WriteLine($"Status: {Tasks[i].Status}");
                            Console.WriteLine("");
                        }
                        Console.WriteLine("Press any key to return to MainMenu!");
                        Console.ReadKey(true);
                        return;
                    }
                case 2:
                    {
                        Console.WriteLine("Task Title: ");
                        var title = Console.ReadLine();
                        Console.WriteLine("Description: ");
                        string desc = Console.ReadLine();
                        TaskEntity task = new TaskEntity();
                        task.Title = title;
                        task.Description = desc;
                        task.CreatedAt= DateTime.Now;
                        task.Status = Status.Pending;
                        Tasks.Add(task);
                        await Save();
                        Console.WriteLine("Task Added Successfully!");
                        await Task.Delay(500);
                        return;
                    }
                case 3:
                    {
                            Console.Clear();
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            Console.WriteLine($"[{i + 1}]. {Tasks[i].Title}");
                            Console.WriteLine($"Description: {Tasks[i].Description}");
                            Console.WriteLine($"Status: {Tasks[i].Status}");
                            Console.WriteLine("");
                            }
                        Console.WriteLine("Enter the task number to update it.");
                        int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int taskNumber);
                        Console.Clear();
                        Console.WriteLine($"Old Title: {Tasks[taskNumber - 1].Title}");
                        Console.Write("New Title: ");
                        var title = Console.ReadLine();
                        Console.WriteLine($"Old Description: {Tasks[taskNumber - 1].Description}");
                        Console.Write("New Description: ");
                        var desc = Console.ReadLine();
                        Console.WriteLine($"Old Status : {Tasks[taskNumber - 1].Status}");
                        Console.WriteLine($"Select Status: 1 (Pending) 2 (In Progress) 3 (Finished)");
                        int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int statusNumber);
                        Status status = (Status)(statusNumber-1);
                        Tasks[taskNumber - 1].Title = title;
                        Tasks[taskNumber - 1].Description = desc;
                        Tasks[taskNumber - 1].Status = status;
                        Console.WriteLine("Task Updated Successfully!");
                        await Save();
                        await Task.Delay(500);
                        return;
                    }
                case 4:
                    {
                            Console.Clear();
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            Console.WriteLine($"[{i + 1}]. {Tasks[i].Title}");
                            Console.WriteLine($"Description: {Tasks[i].Description}");
                            Console.WriteLine($"Status: {Tasks[i].Status}");
                            Console.WriteLine("");
                        }

                        Console.WriteLine("Enter the task number to remove it from the list.");
                        int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int taskNumber);
                        Tasks.RemoveAt((taskNumber - 1));
                        Console.WriteLine("Task removed Successfully!");
                        await Save();
                        await Task.Delay(500);
                        return;
                    }
                case 5:
                    {
                        Console.Clear();
                        Console.WriteLine("Search by A keyword from title or description");
                        Console.Write($"Search : ");
                        var search = Console.ReadLine();
                        var tasks = Tasks.FindAll(
                            t => t.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                            t.Description.Contains(search, StringComparison.OrdinalIgnoreCase));
                        Console.Clear();
                        Console.WriteLine($"Results Founds: {tasks.Count}");
                        for(int i = 0; i < tasks.Count; i++)
                        {
                            Console.WriteLine($"[{i + 1}]. {tasks[i].Title}");
                            Console.WriteLine($"Description: {tasks[i].Description}");
                            Console.WriteLine($"Status: {tasks[i].Status}");
                        }
                        Console.WriteLine("Press any key to return to MainMenu!");
                        Console.ReadKey(true);
                        return;
                    }
                case 6:
                    {
                        Console.Clear();
                        for (int i = 0; i < Tasks.Count; i++)
                        {
                            Console.WriteLine($"[{i + 1}]. {Tasks[i].Title}");
                            Console.WriteLine($"Description: {Tasks[i].Description}");
                            Console.WriteLine($"Status: {Tasks[i].Status}");
                            Console.WriteLine("");
                        }
                        Console.WriteLine("Enter the task number to update it.");
                        int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int taskNumber);
                        Console.Clear();
                        Console.WriteLine($"Old Status : {Tasks[taskNumber - 1].Status}");
                        Console.WriteLine($"Select Status: 1 (Pending) 2 (In Progress) 3 (Finished)");
                        int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int statusNumber);
                        Status status = (Status)(statusNumber - 1);
                        Tasks[taskNumber - 1].Status = status;
                        Console.WriteLine("Task Updated Successfully!");
                        await Save();
                        await Task.Delay(500);
                        return;
                    }
            }
        }
        
        static async Task LoadTasks()
        {
            Tasks = await Data.GetAll();
        }
        static async Task Save()
        {
            await Data.Save(Tasks);
        }
    }
}
