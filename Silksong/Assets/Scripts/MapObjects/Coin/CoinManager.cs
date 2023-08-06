using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager:MonoSingleton<CoinManager>
{
    //���Ԥ�Ƶ�·��
    private readonly static string coinPrefabPath = "coin";

    private List<GameObject> coinsPool = null;

    private int largeCoinMoneyNum = 5;
    private int middleCoinMoneyNum = 2;
    private int smallCoinMoneyNum = 1;

    public override void Init()
    {
        coinsPool = new List<GameObject>();
    }

    /// <summary>
    /// ���Դӻ�������ý�ҵ�gameobject,������û�о�����һ��
    /// </summary>
    /// <returns></returns>
    private GameObject TryGetCoinPrefabsFormPool() 
    {
        GameObject coin = null;
        if (coinsPool.Count > 0)
        {
            coin = coinsPool[0];
            coinsPool.RemoveAt(0);
            coin.transform.localScale = Vector3.one;
        }
        else 
        {
            coin = (GameObject)Instantiate(Resources.Load(coinPrefabPath));        
        }
        return coin;
    }

    /// <summary>
    /// ����ָ����Ŀ�Ľ��
    /// </summary>
    /// <param name="obj">���ɵ�λ��</param>
    /// <param name="num">����</param>
    public void GenerateCoinsByNum(GameObject obj,int num) 
    {
        int maxLargeCoin = num / largeCoinMoneyNum;
        int largeCoin = Random.Range(1,maxLargeCoin);

        num -= largeCoin * largeCoinMoneyNum;

        int maxMiddleCoin = num / middleCoinMoneyNum;
        int middleCoin = Random.Range(1,maxMiddleCoin);

        num -= middleCoin * middleCoinMoneyNum;

        int smallCoin = num;

        GenerateCoins(obj,largeCoin,middleCoin,smallCoin);
    }

    /// <summary>
    /// ���ɽ��
    /// </summary>
    /// <param name="obj">�����gameobject��λ������</param>
    /// <param name="largeCoins">���ͽ�ҵ�����</param>
    /// <param name="middleCoins">���ͽ�ҵ�����</param>
    /// <param name="smallCoins">С�ͽ�ҵ�����</param>
    public void GenerateCoins(GameObject obj, int largeCoins, int middleCoins, int smallCoins)
    {
        for (int i = 0; i < largeCoins; ++i) 
        {
            GameObject coin = TryGetCoinPrefabsFormPool();
            coin.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            coin.transform.position = obj.transform.position;
            coin.SetActive(true);
            coin.GetComponent<Coin>().SetCoinMoneyNum(largeCoinMoneyNum);
        }
        for (int i = 0; i < middleCoins; ++i)
        {
            GameObject coin = TryGetCoinPrefabsFormPool();
            coin.transform.localScale = Vector3.one;
            coin.transform.position = obj.transform.position;
            coin.SetActive(true);
            coin.GetComponent<Coin>().SetCoinMoneyNum(middleCoinMoneyNum);
        }
        for (int i = 0; i < smallCoins; ++i)
        {
            GameObject coin = TryGetCoinPrefabsFormPool();
            coin.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            coin.transform.position = obj.transform.position;
            coin.SetActive(true);
            coin.GetComponent<Coin>().SetCoinMoneyNum(smallCoinMoneyNum);
        }
    }


    public void RecycleCoinsPrefabs(GameObject coin)
    {
        coinsPool.Add(coin);
        coin.SetActive(false);
        coin.transform.parent = this.transform;
        coin.transform.localScale = Vector3.zero;
    }

}
