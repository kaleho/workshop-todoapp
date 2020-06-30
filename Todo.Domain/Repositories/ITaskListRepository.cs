using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Todo.Dialect;

namespace Todo.Domain.Repositories
{
    public interface ITaskListRepository
    {
        int Count { get; }

        void Add(
            int key,
            TaskList taskList);

        TaskList GetByKey(
            int key);
    }

    public class FileTaskListRepository
        : ITaskListRepository
    {
        private readonly Dictionary<int, TaskList> _list;
        private readonly string _repositoryFile;

        public FileTaskListRepository()
        {
            _list = new Dictionary<int, TaskList>();

            var assemblyLocation = Assembly.GetExecutingAssembly().Location;

            var currentFolder = Path.GetDirectoryName(assemblyLocation);

            _repositoryFile = Path.Combine(currentFolder, "data", "repository.json");

            if (File.Exists(_repositoryFile))
            {
                var json = File.ReadAllText(_repositoryFile);

                _list = JsonConvert.DeserializeObject<Dictionary<int, TaskList>>(json);
            }
            else
            { 
                Directory.CreateDirectory(Path.Combine(currentFolder, "data"));
            }
        }

        public int Count { get; }

        public void Add(int key, TaskList taskList)
        {
            throw new System.NotImplementedException();
        }

        public TaskList GetByKey(int key)
        {
            throw new System.NotImplementedException();
        }
    }
}