using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtalTransition : SceneTransitionPoint
{
    private bool canTrans;
    public GameObject TransitionPoint;
    private GameObject player;

    void Update()
    {
        if (canTrans)
        {
            enterEvent();       //�л���ָ������
            
            //player�ƶ���ָ��λ��
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            canTrans = true;
            player = other.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            canTrans = false;
        }
    }
}
