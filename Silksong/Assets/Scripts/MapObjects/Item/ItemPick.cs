using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Opsive.UltimateInventorySystem.DatabaseNames.DemoInventoryDatabaseNames;

public class ItemPick : MonoBehaviour
{
	// Start is called before the first frame update

	//ȷ����ײ��ȡ
	private CircleCollider2D circleCollider2D;


	[SerializeField] private LayerMask playerLayerMask;
	[SerializeField] private LayerMask groundLayerMask;

	public GameObject text;
	public ItemObject item;
	private Inventory inventory;

	private bool isPicking = false;

	void Start()
    {
		circleCollider2D = GetComponent<CircleCollider2D>();
		inventory = InventorySystemManager.GetInventoryIdentifier(1).Inventory;
	}

	//��������״��ײ��
	private void OnTriggerEnter2D(Collider2D collision)
	{
		text.SetActive(true);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			if (isPicking)
			{
				Debug.Log("����");
				inventory.GetItemCollection("Main").AddItem(item.Item, 1);
				isPicking = true;
				Destroy(this.gameObject);
			}
			
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		text.SetActive(false);
	}



	// Update is called once per frame
	void Update()
    {
        
    }
}
