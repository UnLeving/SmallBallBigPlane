using System;
using SmallBallBigPlane.Collectables;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SmallBallBigPlane.Infrastructure.Services
{
    public class GameStateService : IService
    {
        public string Id { get; set; } = "GameState";

        public event Action GameRestarted;
        public event Action GameLost;
        public event Action GameWon;

        public void PlayerFall()
        {
            GameLost?.Invoke();
        }

        public void PlayerReachFinish()
        {
            GameWon?.Invoke();
        }

        public void RestartRequested()
        {
            GameRestarted?.Invoke();
        }
    }
}