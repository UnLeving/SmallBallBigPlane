using System;
using HelpersAndExtensions.SaveSystem;
using Reflex.Attributes;
using SmallBallBigPlane.Collectables;
using UnityEngine;

namespace SmallBallBigPlane
{
    [Serializable]
    public class PlayerData : ISavable
    {
        [field: SerializeField] public string Id { get; set; }
        public Vector3 position;
        public Quaternion rotation;
    }
    
    public class Player : MonoBehaviour, IBind<PlayerData>
    {
        private Vector3 _startPosition;
        [Inject] private IGameManager _gameManager;
        [field: SerializeField] public string Id { get; set; }
        [SerializeField] private PlayerData data;
        
        private void Start()
        {
            _gameManager.GameRestarted += ResetPosition;
            
            _startPosition = transform.position;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out ICollectable collectable))
            {
                collectable.Collect();
            }
            
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
        
        private void ResetPosition()
        {
            transform.position = _startPosition;
        }
        
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