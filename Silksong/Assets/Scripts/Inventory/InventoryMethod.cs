using Opsive.UltimateInventorySystem.Core.AttributeSystem;
using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;
using UnityEngine;

static class InventoryMethod
{

	/// <summary>
	/// ��ӡ��Ʒ������ֵ
	/// </summary>
	/// <param name="inventory"></param>
	/// <param name="itemName"></param>
	/// <param name="attributeName"></param>
	public static void GetItemAttributeValue(Inventory inventory, string itemName, string attributeName)
	{
		var item = GetItem(inventory, itemName);
		var value = GetItemAttributeAsObject(item, attributeName);
		Debug.Log(value);
	}


	/// <summary>
	/// �����Ʒ
	/// </summary>
	/// <param name="inventory"></param>
	/// <param name="itemName"></param>
	static Item GetItem(Inventory inventory, string itemName)
	{
		Item item;

		//�����Ʒ����
		ItemDefinition itemDefinition = InventorySystemManager.GetItemDefinition(itemName);
		//�п�
		if (itemDefinition == null)
		{
			Debug.LogError("��ϵͳľ������Ұ:" + itemName);
		}

		//��ô���������֮ƥ��ĵ�һ����Ʒ
		var itemInfo = inventory.GetItemInfo(itemDefinition);
		//�п�
		if (itemInfo.HasValue == false)
		{
			Debug.LogError("�ÿ��ľ������Ұ:" + itemName);
			//����������Ҳ���Դ�Ĭ����Ʒ�������
			item = itemDefinition.DefaultItem;
		}
		else
		{
			item = itemInfo.Value.Item;
		}
		return item;
	}

	/// <summary>
	/// �����Ʒ����
	/// </summary>
	/// <param name="item"></param>
	/// <param name="attributeName"></param>
	/// <returns></returns>
	public static object GetItemAttributeAsObject(Item item, string attributeName)
	{
		//����δ֪���ʶ�ʹ��object
		var attribute = item.GetAttribute(attributeName);
		if (attribute == null)
		{
			Debug.LogError($"��Ʒ '{item.name}' ľ���ҵ�����Ϊ '{attributeName}'������");
			return null;
		}
		var attributeValue = attribute.GetValueAsObject();
		return attributeValue;
	}
	/// <summary>
	/// ��ȡfloat��������
	/// </summary>
	/// <param name="item"></param>
	/// <param name="attributeName"></param>
	/// <returns></returns>
	public static float GetItemAttributeAsFloat(Item item, string attributeName)
	{
		var floatAttribute = item.GetAttribute<Attribute<float>>(attributeName);

		if (floatAttribute == null)
		{
			Debug.LogError($"��Ʒ '{item.name}' ľ���ҵ�float���͵� '{attributeName}'������");
			return float.NaN;
		}

		return floatAttribute.GetValue();
	}

	/// <summary>
	/// ��ȡstring��������
	/// </summary>
	/// <param name="item"></param>
	/// <param name="attributeName"></param>
	/// <returns></returns>
	public static string GetItemAttributeAsString(Item item, string attributeName)
	{
		var stringAttribute = item.GetAttribute<Attribute<string>>(attributeName);

		if (stringAttribute == null)
		{
			Debug.LogError($"��Ʒ '{item.name}' ľ���ҵ�string���͵� '{attributeName}'������");
			return null;
		}

		return stringAttribute.GetValue();
	}

	/// <summary>
	/// ��ȡint��������
	/// </summary>
	/// <param name="item"></param>
	/// <param name="attributeName"></param>
	public static void GetItemAttributeAsInt(Item item, string attributeName)
	{
		var intAttribute = item.GetAttribute<Attribute<int>>(attributeName);

		if (intAttribute == null)
		{
			Debug.LogError($"��Ʒ '{item.name}' ľ���ҵ�int���͵� '{attributeName}'������");
			return;
		}

		intAttribute.GetValue();
	}

	/// <summary>
	/// ��ȡsprite��������
	/// </summary>
	/// <param name="item"></param>
	/// <param name="attributeName"></param>
	/// <returns></returns>
	public static Sprite GetItemAttributeAsSprite(Item item, string attributeName)
	{
		var intAttribute = item.GetAttribute<Attribute<Sprite>>(attributeName);

		if (intAttribute == null)
		{
			Debug.LogError($"��Ʒ '{item.name}' ľ���ҵ�Sprite���͵� '{attributeName}'������");
			return null;
		}

		return intAttribute.GetValue();
	}

	/// <summary>
	/// ������Ʒ����
	/// </summary>
	/// <param name="inventory"></param>
	/// <param name="itemName"></param>
	/// <param name="attributeName"></param>
	/// <param name="value"></param>
	public static void SetItemAttributeValue(Inventory inventory, string itemName, string attributeName, string value)
	{
		var item = GetItem(inventory, itemName);
		SetItemAttributeAsObject(item, attributeName, value);

	}


	static void SetItemAttributeAsObject(Item item, string attributeName, string attributeValueAsStringObject)
	{
		if (item.IsMutable == false)
		{
			Debug.Log("���ɱ���Ʒ");
			return;
		}

		var itemAttribute = item.GetAttribute(attributeName);
		if (itemAttribute == null)
		{
			Debug.Log($"��Ʒ��û��'{item.name}'����");
			return;
		}

		if (itemAttribute.AttachedItem == null)
		{
			Debug.Log($"'{attributeName}'����һ������");
			return;
		}
		//��֪���������ͣ���string����
		itemAttribute.SetOverrideValueAsObject(attributeValueAsStringObject);
	}


	static void SetItemAttributeAsInt(Item item, string attributeName, int attributeValue)
	{
		var intAttribute = item.GetAttribute<Attribute<int>>(attributeName);

		if (intAttribute == null)
		{
			Debug.Log($"��Ʒ��û��int���͵�'{item.name}'����");
			return;
		}

		intAttribute.SetOverrideValue(attributeValue);
	}

	static void SetItemAttributeAsString(Item item, string attributeName, string attributeValue)
	{
		var stringAttribute = item.GetAttribute<Attribute<string>>(attributeName);

		if (stringAttribute == null)
		{
			Debug.Log($"��Ʒ��û��string���͵�'{item.name}'����");
			return;
		}

		stringAttribute.SetOverrideValue(attributeValue);
	}

	static void SetItemAttributeAsFloat(Item item, string attributeName, float attributeValue)
	{
		var floatAttribute = item.GetAttribute<Attribute<float>>(attributeName);

		if (floatAttribute == null)
		{
			Debug.Log($"��Ʒ��û��float���͵�'{item.name}'����");
			return;
		}

		floatAttribute.SetOverrideValue(attributeValue);
	}
}
