using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace HelpersAndExtensions.SaveSystem
{
    public interface IDataService
    {
        void Save(GameData data, bool overwrite = true);
        UniTask<GameData> Load(string name);
        void Delete(string name);
        void DeleteAll();
        IEnumerable<string> ListSaves();
    }
}