using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Todo.Dialect
{
    public class TaskList
    {
        private readonly Dictionary<int, Task> _items;

        public TaskList(
            string title)
        {
            ValidateTitle(title);

            _items = new Dictionary<int, Task>();

            Title = title;
        }

        public string Title { get; private set; }

        private void ValidateTitle(
            string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException($"TaskList {nameof(title)} was missing or invalid: {title}.", nameof(title));
            }
        }

        public ReadOnlyDictionary<int,Task> Items => 
            new ReadOnlyDictionary<int, Task>(_items);

        public void Add(
            string text)
        {
            var newTask = new Task(text);

            _items.Add(_items.Count, newTask);
        }

        public void SetTitle(
            string title)
        {
            ValidateTitle(title);

            Title = title;
        }
    }
}