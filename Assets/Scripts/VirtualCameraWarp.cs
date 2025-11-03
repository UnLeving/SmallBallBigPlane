using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Reflex.Attributes;
using UnityEngine;

namespace SmallBallBigPlane
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class VirtualCameraWarp : MonoBehaviour
    {
        [Inject] private IGameManager _gameManager;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineTransposer _transposer;
        private float originalXDamping;
        private float originalYDamping;
        private float originalZDamping;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        private void Start()
        {
            _gameManager.GameRestarted += GameManager_OnGameRestarted;

            if (_transposer != null)
            {
                originalXDamping = _transposer.m_XDamping;
                originalYDamping = _transposer.m_YDamping;
                originalZDamping = _transposer.m_ZDamping;
            }
        }

        private void OnDestroy()
        {
            //_gameManager.GameRestarted -= GameManager_OnGameRestarted;
            
            _gameManager.GameLost -= GameManager_OnGameRestarted;
        }

        private void GameManager_OnGameRestarted()
        {
            SetDampingToZero();

            StartCoroutine(RestoreDampingAfterFrame());
        }

        private void SetDampingToZero()
        {
            if (_transposer == null) return;
            
            _transposer.m_XDamping = 0f;
            _transposer.m_YDamping = 0f;
            _transposer.m_ZDamping = 0f;
        }
        
        private void RestoreDamping()
        {
            if (_transposer == null) return;
            
            _transposer.m_XDamping = originalXDamping;
            _transposer.m_YDamping = originalYDamping;
            _transposer.m_ZDamping = originalZDamping;
        }

        private IEnumerator RestoreDampingAfterFrame()
        {
            yield return null;

            RestoreDamping();
        }
    }
}