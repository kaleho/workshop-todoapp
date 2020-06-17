using ConsoleApp1.Repositories;
using System;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly IRepository _repository;

        static Program()
        {
            _repository = new FileRepository();
        }

        /// <summary>
        /// Todo Application
        /// 1. View a list of items
        /// 2. Create a item
        /// 3. Remove an item (Delete)
        /// - Prioritize item
        /// - Complete item
        /// - Set a due date on an item
        /// </summary>
        /// <param name="args"></param>
        static void Main(
            string[] args)
        {
            Initialize();

            string input;

            Console.WriteLine("Type something and hit enter to continue...");

            while (Commands.Exit != (input = Console.ReadLine()))
            {
                switch (input)
                {
                    case Commands.Create:
                        Console.WriteLine("What would you like to do?");

                        var todoInput = Console.ReadLine();

                        _repository.Add(_repository.Count, todoInput);

                        Console.WriteLine("created");
                        break;

                    case Commands.List:
                        for (var i = 0; i < _repository.Count; i++) 
                        {
                            Console.WriteLine($"{i}: {_repository.GetByKey(i)}");
                        }

                        Console.WriteLine("listed");
                        break;

                    default:
                        Console.WriteLine($"You wrote: {input}{Environment.NewLine}"); 
                        break;
                }
            }
        }

        private static void Initialize()
        {
            if (_repository.Count == 0)
            {
                for (var i = 0; i <= 2; i++)
                {
                    _repository.Add(i, $"hi {i}");
                }
            }
        }
    }
}