// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;
//
// /// <summary>
// /// ������ͼƬUI����ʾ��װ�������
// /// </summary>
// public class CharmImage : MonoBehaviour, ISelectHandler
// {
//     // [SerializeField]
//     // private GameObject equipIcon;
//     // [SerializeField]
//     // private GameObject lockIcon;
//     // [SerializeField]
//     // private GameObject selectIcon;
//     [SerializeField]
//     private Image charmSprite;
//
//     private CharmUIPanel charmUIPanel;
//
//     private bool isSelected;
//
//     private bool slotEmpty = true;
//     public bool SlotEmpty { get => slotEmpty; set => slotEmpty = value; }
//
//     public Charm charm;
//
//     public CharmSlot slot;
//     
//
//     public void OnSelect(BaseEventData eventData)
//     {
//         Debug.Log("select");
//         charmUIPanel.ChangeSelect(this);
//     }
//     public void Init(Charm _charm, CharmUIPanel _charmUIPanel)
//     {
//         charm = _charm;
//         charmUIPanel = _charmUIPanel;
//         charmSprite.sprite = _charm.charmImage;
//     }
// }
//
