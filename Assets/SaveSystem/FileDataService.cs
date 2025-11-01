using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HelpersAndExtensions.SaveSystem
{
    public class FileDataService: IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;
        private string _fileExtension;

        public FileDataService(ISerializer serializer)
        {
            this._serializer = serializer;
            this._dataPath = Application.persistentDataPath;
            this._fileExtension = "json";
        }

        private string GetPathToFile(string fileName)
        {
            return Path.Combine(_dataPath, string.Concat(fileName, ".", _fileExtension));
        }
        
        public void Save(GameData data, bool overwrite = true)
        {
            string fileLocation = GetPathToFile(data.Name);
            
            if (File.Exists(fileLocation) && !overwrite)
            {
                throw new IOException($"File {data.Name}.{_fileExtension} already exists and overwrite is false");
            }
            
            File.WriteAllText(fileLocation, _serializer.Serialize(data));
        }

        public GameData Load(string name)
        {
            string fileLocation = GetPathToFile(name);
            
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException($"File {name}.{_fileExtension} not found");
            }
            
            return _serializer.Deserialize<GameData>(File.ReadAllText(fileLocation));
        }

        public void Delete(string name)
        {
            string fileLocation = GetPathToFile(name);
            
            if (!File.Exists(fileLocation))
            {
                throw new FileNotFoundException($"File {name}.{_fileExtension} not found");
            }
            
            File.Delete(fileLocation);
        }

        public void DeleteAll()
        {
            foreach (var file in Directory.GetFiles(_dataPath))
            {
                File.Delete(file);
            }
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string path in Directory.EnumerateFiles(_dataPath))
            {
                if (Path.GetExtension(path) != _fileExtension) continue;
                
                yield return Path.GetFileNameWithoutExtension(path);
            }
        }
    }
}