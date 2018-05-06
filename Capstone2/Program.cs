using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Capstone2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>
            {
                new Task ("Sam", "Do art homework", "5/6/12"),
                new Task ("George", "Garbage", "5/7/12"),
                new Task ("Sam", "Set up meeting", "5/8/12"),
                new Task ("Sam", "Do Laundry", "5/9/12"),
                new Task ("Sam", "Set up a trip to see grandma", "5/10/12"),
                new Task ("John", "Study hard for demolition day", "5/11/12")
            };

            bool truth = true;
            while (truth)
            {
                Console.WriteLine("Welcome to the Task Manager!");
                Console.WriteLine("\t1. List tasks"); //done
                Console.WriteLine("\t2. Add task"); //done
                Console.WriteLine("\t3. Delete task"); //done
                Console.WriteLine("\t4. Mark task complete"); //done
                Console.WriteLine("\t5. Display tasks for a specific member"); //done
                Console.WriteLine("\t6. Display tasks due before a date"); //done
                Console.WriteLine("\t7. Edit a task description"); //done
                Console.WriteLine("\t8. Quit");//done
                Console.Write("What would you like to do? (Insert 1-8): ");


                string input = Console.ReadLine().ToLower();
                bool moveAlong = int.TryParse(input, out int moveAlongNum);
                if (moveAlong)
                {
                    if(moveAlongNum == 1)
                    {
                        ListTask(tasks);
                    }
                    else if(moveAlongNum == 2)
                    {
                        Console.WriteLine(AddTask(tasks));

                    }
                    else if (moveAlongNum == 3)
                    {
                        bool boool = true;
                        while (boool)
                        {
                            var man = DeleteTask(tasks);
                            if ( man == "Number entered was outside the range" )
                            {
                                Console.WriteLine("Please try again");
                                continue;
                            }
                            else if (man == "N")
                            {
                                boool = false;
                            }
                            else if (man == "Invalid input.")
                            {
                                Console.WriteLine("\n\n\n\n\n\n\t\t\t\t\tInvalid input, we'll return you to the main menu.");
                                boool = false;
                            }
                            else
                            {
                                boool = false;
                            }
                        }
                    }
                    else if (moveAlongNum == 4)
                    {
                        MarkTaskPrompt(tasks);
                    }
                    else if (moveAlongNum == 5)
                    {
                        SpecificMember(tasks);
                    }
                    else if (moveAlongNum == 6)
                    {
                        SpecificDate(tasks);
                    }
                    else if (moveAlongNum == 7)
                    {
                        EditTaskPrompt(tasks);
                    }
                    else if (moveAlongNum == 8)
                    {
                        Console.Write("Are you sure you want to quit? (Enter Y/N): ");
                        string quitter = Console.ReadLine().ToUpper();
                        bool smove = int.TryParse(quitter, out int smoveNum);
                        if (!smove && quitter == "Y")
                        {
                            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t    Thank you for using our product, have a great day!");
                            truth = false; 
                        }
                        else if(quitter != "N")
                        {
                            Console.WriteLine("\n\n\nNot valid, we'll return you back to the Main Menu.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\n\t\t\t\tNot a valid option. We'll provide the menu options again. \n\n");
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input, please try again!\n");
                }
                Console.WriteLine("\n\n\n");
            }
        }

        public static void SpecificDate(List<Task> tasks)
        {
            Console.WriteLine("\nWhat is the date that you want to be searched?(MM/DD/YYYY) Tasks due on or before this date will be presented.");
            string input = Console.ReadLine();
            bool worked = int.TryParse(input, out int x);
            if (worked || input == "" || Regex.IsMatch(input, @"\s"))
            {
                Console.WriteLine("\n\n\nInvalid input, that is not a date.");
                return;
            }

            bool dateTimeWorked = DateTime.TryParse(input, out DateTime dateTime);
            if (!dateTimeWorked)
            {
                Console.WriteLine("\n\n\nInvalid input.\n");
            }
            else if (dateTimeWorked)
            {
                foreach (Task item in tasks)
                {
                    int i = 0;
                    i++;
                    if (item.Date <= dateTime)
                    {
                        Console.Write($"\t");
                        Console.Write($"{item.TaskCompletion}\t");
                        Console.Write($"{item.Date}\t");
                        Console.Write($"{item.TeamMemberName}\t\t");
                        Console.Write($"{item.Description}\t");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                } 
            }
            else
            {
                Console.WriteLine("\n\nInvalid input, returning to main menu!");
            }
        }

        public static void EditTaskPrompt(List<Task> tasks)
        {
            Console.WriteLine($"Which task do you want to edit complete 1-{tasks.Count}?");
            string input = Console.ReadLine();
            bool worked = int.TryParse(input, out int x);
            if (!worked || x > tasks.Count || x <= 0 )
            {
                Console.WriteLine("\n\n\nInput was invalid or not in range");
            }
            else
            {
                Console.WriteLine($"Are you sure you want to edit this task below complete? ");
                Console.Write($"\t");
                Console.Write($"{tasks[x - 1].TaskCompletion}\t");
                Console.Write($"{tasks[x - 1].Date}\t");
                Console.Write($"{tasks[x - 1].TeamMemberName}\t\t");
                Console.Write($"{tasks[x - 1].Description}\t (Press Y / N): ");

                string sure = Console.ReadLine().ToUpper();
                bool sured = int.TryParse(sure, out int sureNum);
                if (sured)
                {
                    Console.WriteLine("\n\n\nIncorrect input. Task cannot be just numbers.\n");
                }
                else
                {
                    if (sure == "Y")
                    {
                        MarkTask(tasks[x - 1]);
                        EditTask(tasks[x - 1]);
                        Console.WriteLine("Task was successfully edited complete!\n");
                    }
                    else if (sure == "N")
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid input option.\n");
                    }
                }
            }
        }

        public static void EditTask(Task task)
        {
            Console.WriteLine("What do you want the task to be changed to?");
            string input = Console.ReadLine();
            bool worked = int.TryParse(input, out int x);
            if (worked)
            {
                Console.WriteLine("input was invalid.");
            }
            else
            {
                task.Description = input;
            }
        }

        public static void SpecificMember(List<Task> tasks)
        {
            Console.WriteLine("What is the name of the member that you want to be searched?");
            string input = Console.ReadLine();
            foreach (Task item in tasks)
            {
                int i = 0;
                i++;
                if(item.TeamMemberName == input)
                {
                    //Console.WriteLine(tasks[i]);
                    
                    Console.Write($"\t");
                    Console.Write($"{item.TaskCompletion}\t");
                    Console.Write($"{item.Date}\t");
                    Console.Write($"{item.TeamMemberName}\t\t");
                    Console.Write($"{item.Description}\t");
                    Console.WriteLine();
                    
                    //Console.WriteLine($"True at {i}.");
                }
                else
                {
                    Console.WriteLine();
                }
                //tasks.FindAll(item.TeamMemberName);
                //item.TeamMemberName = input;

            }
        }

        public static void MarkTaskPrompt(List <Task> tasks)
        {
            bool boool = true;
            while (boool)
            {
                Console.WriteLine($"Which task do you want to mark complete 1-{tasks.Count}?");
                string input = Console.ReadLine();
                bool worked = int.TryParse(input, out int x);
                if (!worked)
                {
                    Console.WriteLine("\nInput was invalid. Try again.\n");
                }
                else
                {
                    if (x <= tasks.Count && x > 0)
                    {
                        Console.WriteLine($"Are you sure you want to mark the task below complete? ");
                        Console.Write($"\t");
                        Console.Write($"{tasks[x - 1].TaskCompletion}\t");
                        Console.Write($"{tasks[x - 1].Date}\t");
                        Console.Write($"{tasks[x - 1].TeamMemberName}\t\t");
                        Console.Write($"{tasks[x - 1].Description}\t (Press Y / N): ");

                        string sure = Console.ReadLine().ToUpper();
                        bool sured = int.TryParse(sure, out int sureNum);
                        if (sured)
                        {
                            Console.WriteLine("Incorrect input."); 
                        }
                        else
                        {
                            if (sure == "Y")
                            {
                                MarkTask(tasks[x - 1]);
                                Console.WriteLine("\n\nTask was successfully marked complete!");
                                boool = false;
                            }
                            else if (sure == "N")
                            {
                                Console.WriteLine();
                                boool = false;
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Try again."); 
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nInput was out of range.Try again.\n");
                    }
                } 
            }
        }

        public static void MarkTask(Task task)
        {
            ////List<Task> xed = new List<Task>();
            //List<Task> task = new List<Task>();
            //tasks.CopyTo(0,task,0, 1);
            //Console.WriteLine(task);
            ////tasks.RemoveAt(0);
            task.TaskCompletion = true;
        }
        
        public static string DeleteTask(List<Task> tasks)
        {
            Console.WriteLine($"Which task number to you want to delete 1-{tasks.Count}?\n");
            string delete = Console.ReadLine();
            bool complete = int.TryParse(delete, out int number);
            if (number <= tasks.Count && number > 0)
            {
                if (!complete)
                {
                    return "Input was not valid!\n";
                }
                else
                {
                    Console.WriteLine($"Are you sure you want to delete the task below? ");
                    Console.Write($"\t");
                    Console.Write($"{tasks[number - 1].TaskCompletion}\t");
                    Console.Write($"{tasks[number - 1].Date}\t");
                    Console.Write($"{tasks[number - 1].TeamMemberName}\t\t");
                    Console.Write($"{tasks[number - 1].Description}\t (Press Y / N): ");

                    string sure = Console.ReadLine().ToUpper();
                    bool sured = int.TryParse(sure, out int sureNum);
                    if (sured)
                    {
                        return "Invalid input.";
                    }
                    else
                    {
                        if (sure == "Y")
                        {
                            tasks.RemoveAt(number - 1);
                            return $"\nDeletion of task {number} was successful\n"; 
                        }
                        else if (sure == "N")
                        {
                            return "N";
                        }
                        else
                        {
                            return "Invalid input.";
                        }
                    }
                        
                } 
            }
            else
            {
                return "Number entered was outside the range";
            }
        }

        public static string AddTask(List<Task> tasks)
        {
            Console.Write("Please enter the name of the Team Member: ");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter the task you want completed");
            string task = Console.ReadLine();
            Console.Write("On what date must this task be complete by?: ");
            string date = Console.ReadLine();
            bool complete = double.TryParse(date, out double completionDays);
            if (complete)
            {
                return "Not a valid input for days";
            }
            else
            {
                tasks.Add(new Task(name, task, date));
                return "Task successfully added!";
            }

        }
        
        //put task into the method and for(each) loop in main to loop through?
        public static void ListTask(List<Task> tasks)
        {
            Console.Write($"\t");
            Console.Write("Task\t");
            Console.Write("Done?\t");
            Console.Write($"Due Date\t\t");
            Console.Write($"Team Member\t");
            Console.Write($"Description\t");
            Console.WriteLine();
            Console.WriteLine();

            if(tasks.Count == 0)
            {
                Console.WriteLine("\n\n\n\n\n\n\t\t\t\t\tThere are currently no tasks on the TaskList.");
            }

            int i = 1;
            foreach (Task item in tasks)
            {
                Console.Write($"\t");
                Console.Write($"{i++})\t");
                Console.Write($"{item.TaskCompletion}\t");
                Console.Write($"{item.Date}\t");
                Console.Write($"{item.TeamMemberName}\t\t");
                Console.Write($"{item.Description}\t");
                Console.WriteLine();
            } 
        }
    }
}
