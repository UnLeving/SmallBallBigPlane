using System;
using Unity.Mathematics;
using UnityEngine;

namespace HelpersAndExtensions.SaveSystem.Example
{
    [Serializable]
    public class PlayerData : ISavable
    {
       [field: SerializeField] public SerializableGuid Id { get; set; }
        public Vector3 position;
        public quaternion rotation;
    }

    public class Hero : MonoBehaviour, IBind<PlayerData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set;} = Guid.NewGuid();
        [SerializeField] private PlayerData data;
        
        public void Bind(PlayerData data)
        {
            this.data = data;
            this.data.Id = Id;
            
            transform.position = data.position;
            transform.rotation = data.rotation;
        }

        private void Update()
        {
            data.position = transform.position;
            data.rotation = transform.rotation;
        }
    }
}