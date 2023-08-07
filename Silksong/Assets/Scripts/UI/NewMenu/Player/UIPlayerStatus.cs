using DG.Tweening;
using Opsive.UltimateInventorySystem.Core.InventoryCollections;
using Opsive.UltimateInventorySystem.Core;
using Opsive.UltimateInventorySystem.Exchange;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Opsive.UltimateInventorySystem.DatabaseNames.DemoInventoryDatabaseNames;


public class UIPlayerStatus : MonoBehaviour
{

	public HpDamable representedDamable;

	private int moneyNum = 0;       //�����ã�Ӧ�ô��������õ��������

	private int addAll = 0;     //���ӵ�����

	public float changeTime = 3.0f;     //���ּ�¼��ʱ��
	private float changeTimeRecord = 3.0f;
	private bool changeTimeStart = false;
	private bool changeTimeOver = false;

	public float zeroTime = 1.5f;       //���ֹ����ʱ��
	private float zeroTimeRecord = 1.5f;
	private float perReduce = 0;

	public GameObject hpItem; 
	public GameObject hpBg;//Ѫ����ͷ
	public Transform content;
	public List<GameObject> hpItems;
	public Slider slider;

	public Text TextCoin;
	public Text TextAddCoin;

	private Inventory inventory;
	private CurrencyCollection ownerCurrencyCollection;
	Currency gold;

	// Start is called before the first frame update
	void Start()
    {
		DontDestroyOnLoad(gameObject);
		EventManager.Instance.Register<int>(EventType.onMoneyChange, ChangeMoneyNum);

		inventory = InventorySystemManager.GetInventoryIdentifier(GameManager.Instance.saveSystem.SaveData.inventoryIndex).Inventory; 
		var currencyOwner = InventorySystemManager.GetInventoryIdentifier(GameManager.Instance.saveSystem.SaveData.inventoryIndex).CurrencyOwner;
		ownerCurrencyCollection = currencyOwner.CurrencyAmount;
		gold = InventorySystemManager.GetCurrency("��ʯ");
		moneyNum = ownerCurrencyCollection.GetAmountOf(gold);
		TextCoin.text = moneyNum.ToString();

	}

	// Update is called once per frame
	void Update()
    {
		UpdateMoney();
	}
	#region Hp
	public void setRepresentedDamable(HpDamable hpDamable)//����Ѫ������
	{
		if (representedDamable == null)
		{
			Debug.Log(hpDamable);
			representedDamable = hpDamable;
			representedDamable.onHpChange.AddListener(ChangeHitPointUI);
		}
		else
		{
			deleteAllHpIcon();
		}
		int maxHp = representedDamable.MaxHp;
		hpBg.transform.Translate(maxHp * 45,0,0);//DOMoveX(maxHp*45,1);
		for(int i = 0;i< maxHp;i++)
		{
			GameObject go = Instantiate(hpItem, content);
			hpItems.Add(go);
		}
	}

	public void ChangeHitPointUI(HpDamable damageable)//Ѫ���䶯ʱ����
	{
		for (int i = 0; i < hpItems.Count; i++)
		{
			hpItems[i].transform.GetChild(1).transform.GetComponent<Image>().DOColor(damageable.CurrentHp >= i + 1 ? Color.white : Color.black, 1f);
		}
	}

	private void deleteAllHpIcon()
	{
		
	}


	#endregion

	#region Mana
	public void ChangeManaValue(PlayerCharacter playerCharacter)
	{
		slider.maxValue = playerCharacter.getMaxMana();
		slider.minValue = 0;
		slider.DOValue(playerCharacter.Mana, 0.5f);
	}

	public void ChangeManaMax(PlayerCharacter playerCharacter)
	{
		slider.maxValue = playerCharacter.getMaxMana();
		slider.maxValue = 0;
		slider.DOValue(playerCharacter.Mana, 0.5f);
	}
	#endregion

	#region Money
	void UpdateMoney()
	{
		if (changeTimeStart)    //���ӽ�ҵ���Ϊ��ʼ
		{
			changeTimeRecord -= Time.deltaTime;
			if (changeTimeRecord <= 0)  //��¼ʱ�����
			{
				changeTimeOver = true;
				changeTimeStart = false;
				PrepareCountDown();
			}
		}

		if (changeTimeOver)     //���ӽ�ҵ���Ϊ�����ˣ����Ѿ���һ��ʱ��û�л�ȡ������ˣ���ʼ�������ӽ��
		{
			zeroTimeRecord -= Time.deltaTime;
			if (zeroTimeRecord >= 0)
			{
				ChangeAllNum();
			}
			else
			{
				changeTimeOver = false;
				ForceZero();
				ResetStatus();
			}
		}
	}
	
	void ChangeMoneyNum(int changeNum)
	{
		changeTimeRecord = changeTime;  //�ֻ�����µĽ�ң�����ʱ��
		if (changeNum >= 0)
		{
			ownerCurrencyCollection.AddCurrency(gold, changeNum);
			changeTimeStart = true;
			addAll += changeNum;
			TextAddCoin.DOFade(1, 1);
			TextAddCoin.text = "+" + addAll.ToString();

		}
		else
		{
			ownerCurrencyCollection.RemoveCurrency(gold, -changeNum);
			moneyNum += changeNum;
			TextCoin.text = moneyNum.ToString();

		}
		//TextAddCoin.color.g = 1;
		
	}

	void PrepareCountDown()
	{
		perReduce = addAll / zeroTime;
	}

	void ChangeAllNum()
	{
		float detal = perReduce * (zeroTime - zeroTimeRecord);
		TextCoin.text = ((int)(moneyNum + detal)).ToString();
		TextAddCoin.text = "+" + ((int)(addAll - detal)).ToString();
	}

	/// <summary>
	/// ���һ֡ǿ�ƹ���
	/// </summary>
	void ForceZero()
	{
		moneyNum += addAll;
		TextCoin.text = moneyNum.ToString();
		TextAddCoin.text = "+0";
	}

	void ResetStatus()
	{
		addAll = 0;
		changeTimeRecord = changeTime;
		changeTimeStart = false;
		changeTimeOver = false;
		zeroTimeRecord = zeroTime;       //���ֹ����ʱ��
		TextAddCoin.DOFade(0, 1);
	}
	#endregion
}
