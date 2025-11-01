using System;

namespace HelpersAndExtensions.SaveSystem
{
    public interface IBind<TData> where TData : ISavable
    {
        SerializableGuid Id { get; set; }
        void Bind(TData data);
    }
}