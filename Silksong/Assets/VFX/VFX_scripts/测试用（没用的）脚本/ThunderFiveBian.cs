using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderFiveBian : MonoBehaviour
{
    //�����õĽű������������������˵�λ��������gameobject��λ�������ƣ�Ȼ�������inspector�����������ȼ���û��
    ThunderChainController m_ThunderChainController;
    public GameObject enemyA;
    public GameObject enemyB;
    public int thunderLevel;
    // Start is called before the first frame update
    void Start()
    {
        m_ThunderChainController = this.transform.GetComponent<ThunderChainController>();
        thunderLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        m_ThunderChainController.ThunderChainLevel = thunderLevel;
        m_ThunderChainController.startPos = enemyA.transform.position;
        m_ThunderChainController.endPos = enemyB.transform.position;
    }
}
