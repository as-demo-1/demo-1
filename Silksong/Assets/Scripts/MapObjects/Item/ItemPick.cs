using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Opsive.UltimateInventorySystem.DatabaseNames.DemoInventoryDatabaseNames;
using Opsive.UltimateInventorySystem.Core.AttributeSystem;

public class ItemPick : MonoBehaviour
{
	// Start is called before the first frame update

	//ȷ����ײ��ȡ
	private CircleCollider2D circleCollider2D;


	[SerializeField] private LayerMask playerLayerMask;
	[SerializeField] private LayerMask groundLayerMask;

	public GameObject text;
	public ItemObject item;

	private bool isPicking = false;

	[SerializeField]
	private KeyCode pickKey = KeyCode.E;

	void Start()
    {
		circleCollider2D = GetComponent<CircleCollider2D>();
		item.Item.TryGetAttributeValue<Sprite>("Icon", out var icon);
		GetComponent<SpriteRenderer>().sprite = icon;

    }

	//��������״��ײ��
	private void OnTriggerEnter2D(Collider2D collision)
	{
		text.SetActive(true);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(Input.GetKeyDown(pickKey))
		{
			if (!isPicking)
			{
				Debug.Log("����");
				GameManager.Instance.inventory.GetItemCollection("Main").AddItem(item.Item, 1);
				Debug.Log("ӵ�У�"+GameManager.Instance.inventory.GetItemCollection("Main").GetItemAmount(item.Item));
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
