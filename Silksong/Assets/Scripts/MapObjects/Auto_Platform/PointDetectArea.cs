using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDetectArea : MonoBehaviour
{
    public AutoPlatformController APC;
    public bool StartPoint;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //��괥�������������ʲô���ܵ���ûɶͷ��
        if (other.tag == "Player" && StartPoint)
        {
            APC.Pub_Trigger(true);
        }
        else if(other.tag == "Player" && !StartPoint)
        {
            APC.Pub_Trigger(false);
        }
    }
}
