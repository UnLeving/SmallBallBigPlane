using System;
using SmallBallBigPlane.Infrastructure.Services.Factories;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SmallBallBigPlane.UI.Windows
{
    [CreateAssetMenu(fileName = "WindowsContainer", menuName = "ScriptableObjects/WindowsContainer", order = 0)]
    public class WindowsSO : ScriptableObject
    {
        public AssetReferenceGameObject[] windowsReferences;

        public AssetReferenceGameObject GetAssetReferenceByWindowsId(WindowId windowId)
        {
            switch (windowId) 
            {
                case WindowId.Win: return windowsReferences[0];
                case WindowId.Lose: return windowsReferences[1];
                case WindowId.Settings: return windowsReferences[2];
                case WindowId.MainMenu: return windowsReferences[3];
                default:
                    throw new ArgumentOutOfRangeException(nameof(windowId), windowId, null);
            }
        }
    }
}