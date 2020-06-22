using System;

namespace Todo.Dialect
{
    public class Task
    {
        public Task(
            string text,
            bool isCompleted = false)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException($"Task {nameof(text)} was missing or invalid: {text}.", nameof(text));
            }

            Text = text;

            IsCompleted = isCompleted;
        }

        public bool IsCompleted { get; }

        public string Text { get; }
    }
}