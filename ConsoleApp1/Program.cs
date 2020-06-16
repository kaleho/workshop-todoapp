using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly IRepository _repository;

        static Program()
        {
            _repository = new VerboseInMemoryRepository();
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
            for (var i = 0; i <= 2; i++)
            {
                _repository.Add(i, $"hi {i}");
            }
        }
    }

    public interface IRepository
    {
        int Count { get; }

        void Add(
            int index,
            string value);

        string GetByKey(
            int key);
    }

    public class InMemoryRepository
        : IRepository
    {
        private readonly Dictionary<int, string> _list;

        public InMemoryRepository()
        {
            _list = new Dictionary<int, string>();
        }

        public int Count => _list.Count;

        public void Add(
            int index, 
            string value)
        {
            _list.Add(index, value);
        }

        public string GetByKey(
            int key)
        {
            return _list[key];
        }
    }

    public class VerboseInMemoryRepository
        : IRepository
    {
        private readonly Dictionary<int, string> _list;

        public VerboseInMemoryRepository()
        {
            _list = new Dictionary<int, string>();
        }

        public int Count => _list.Count;

        public void Add(
            int index, 
            string value)
        {
            Console.WriteLine("You added stuff");

            _list.Add(index, value);
        }

        public string GetByKey(
            int key)
        {
            Console.WriteLine("You listed stuff");

            return _list[key];
        }
    }
}