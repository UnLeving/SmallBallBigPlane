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
        private IGameManager _gameManager;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineTransposer _transposer;
        private float originalXDamping;
        private float originalYDamping;
        private float originalZDamping;
        
        
        [Inject]
        private void Construct(IGameManager gameManager)
        {
            this._gameManager = gameManager;
        }

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
            else
            {
                Debug.LogError("Transposer not found!");
            }
        }

        private void OnDestroy()
        {
            _gameManager.GameRestarted -= GameManager_OnGameRestarted;
        }

        private void GameManager_OnGameRestarted()
        {
            SetDampingToZero();

            StartCoroutine(RestoreDampingAfterFrame());
        }

        private void SetDampingToZero()
        {
            _transposer.m_XDamping = 0f;
            _transposer.m_YDamping = 0f;
            _transposer.m_ZDamping = 0f;
        }
        
        private void RestoreDamping()
        {
            _transposer.m_XDamping = originalXDamping;
            _transposer.m_YDamping = originalYDamping;
            _transposer.m_ZDamping = originalZDamping;
        }

        private IEnumerator RestoreDampingAfterFrame()
        {
            yield return null;
            yield return null;

            RestoreDamping();
        }
    }
}