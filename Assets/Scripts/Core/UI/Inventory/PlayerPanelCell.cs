using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Inventory;

namespace UI.Inventory
{
    public class PlayerPanelCell : MonoBehaviour
    {
        public PlayerItem _playerItem;
        public RawImage icon;
        public RectTransform rectTransform;
        
        void Start()
        {
            icon = GetComponent<RawImage>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void Init(PlayerItem playerItem)
        {
            _playerItem = playerItem;
            icon.texture = _playerItem.item.slotIcon;
        }

        public void TakeOff()
        {
            _playerItem = null;
            icon.texture = InventoryPlayerPanelRenderer.instance.defaultIcon.texture;
        }
    }
}
