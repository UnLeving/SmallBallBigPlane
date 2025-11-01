using System;

namespace HelpersAndExtensions.SaveSystem
{
    public interface ISavable
    {
        SerializableGuid Id { get; set; }
    }
}