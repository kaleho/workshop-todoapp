using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Todo.Dialect;

namespace Todo.Domain.Repositories
{
    public class FileTaskListRepository
        : ITaskListRepository
    {
        private readonly Dictionary<int, TaskList> _list;
        private readonly string _dataFolder;
        private readonly string _indexFile;
        private readonly Dictionary<int, string> _index;

        public FileTaskListRepository()
        {
            _list = new Dictionary<int, TaskList>();

            var assemblyLocation = Assembly.GetEntryAssembly().Location;

            var currentFolder = Path.GetDirectoryName(assemblyLocation);

            _dataFolder = Path.Combine(currentFolder, "data");

            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
            }

            _indexFile = Path.Combine(_dataFolder, "index.json");

            if (File.Exists(_indexFile))
            {
                var json = File.ReadAllText(_indexFile);

                _index = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);
            }
        }

        public int Count => 
            _index.Count;

        public void Add(
            int key, 
            TaskList taskList)
        {
            if (_index.ContainsKey(key) || 
                _index.Values.Any(
                    x => x.Equals(
                        taskList.Title, 
                        StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new Exception($"Task list cannot be created with {taskList.Title}");
            }

            var file = Path.Combine(_dataFolder, $"{key}.json");

            if (File.Exists(file))
            {
                throw new Exception("Task list file already exists.");
            }

            File.WriteAllText(
                file, 
                JsonConvert.SerializeObject(taskList, Formatting.Indented));

            _index.Add(key, taskList.Title);

            File.WriteAllText(
                _indexFile, 
                JsonConvert.SerializeObject(_index, Formatting.Indented));
        }

        public TaskList GetByKey(
            int key)
        {
            var file = Path.Combine(_dataFolder, $"{key}.json");

            if (!File.Exists(file))
            {
                throw new Exception("Task list does not exist.");
            }

            var taskList =
                JsonConvert.DeserializeObject<TaskList>(
                    File.ReadAllText(file));

            return taskList;
        }

        public void Update(
            int key, 
            TaskList taskList)
        {
            if (!_index.ContainsKey(key))
            {
                throw new Exception("Cannot update task list that has not previously been added.");
            }

            File.WriteAllText(
                Path.Combine(_dataFolder, $"{key}.json"), 
                JsonConvert.SerializeObject(taskList, Formatting.Indented));

            if (_index.ContainsKey(key) && !_index[key].Equals(taskList.Title))
            {
                _index[key] = taskList.Title;
 
                File.WriteAllText(
                    _indexFile, 
                    JsonConvert.SerializeObject(_index, Formatting.Indented));
            }
        }
    }
}