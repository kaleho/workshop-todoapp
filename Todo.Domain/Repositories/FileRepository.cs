using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Todo.Domain.Repositories;

namespace Todo.Domain.Repositories
{
    public class FileRepository
        : IRepository
    {
        private readonly Dictionary<int, string> _list;
        private readonly string _repositoryFile;

        public FileRepository()
        {
            _list = new Dictionary<int, string>();

            var assemblyLocation = Assembly.GetExecutingAssembly().Location;

            var currentFolder = Path.GetDirectoryName(assemblyLocation);

            _repositoryFile = Path.Combine(currentFolder, "data", "repository.json");

            if (File.Exists(_repositoryFile))
            {
                var json = File.ReadAllText(_repositoryFile);

                _list = JsonConvert.DeserializeObject<Dictionary<int, string>>(json);
            }
            else
            { 
                Directory.CreateDirectory(Path.Combine(currentFolder, "data"));
            }
        }

        public int Count => _list.Count;

        public void Add(
            int index,
            string value)
        {
            _list.Add(index, value);

            Save();
        }

        public string GetByKey(
            int key)
        {
            return _list[key];
        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(_list, Formatting.Indented);

            File.WriteAllText(_repositoryFile, json);
        }
    }
}
