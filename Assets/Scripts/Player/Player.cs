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
        public int maxCoins;
    }
    
    public class Player : MonoBehaviour, IBind<PlayerData>
    {
        private Vector3 _startPosition;
        private IGameManager _gameManager;
        [field: SerializeField] 
        public string Id { get; set; }
        [SerializeField] 
        private PlayerData data;

        [Inject]
        private void Construct(IGameManager gameManager)
        {
            this._gameManager = gameManager;
        }
        
        private void Awake()
        {
            if (string.IsNullOrEmpty(Id))
                Id = Guid.NewGuid().ToString("N");
        }
        
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
        }
    }
}