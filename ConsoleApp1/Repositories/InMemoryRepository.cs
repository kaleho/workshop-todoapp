using System.Collections.Generic;

namespace ConsoleApp1.Repositories
{
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
}