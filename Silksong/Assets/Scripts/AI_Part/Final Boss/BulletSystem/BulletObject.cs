using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Create BulletAsset")]
public class BulletObject : ScriptableObject

{
    [Header("���ƶ���ĳ�ʼ����")]
    [Header("������ڵ�ʱ�䣨�룩�ڱ�����֮ǰ")]
    public float lifeCycle = 5;
    [Header("����ĳ�ʼ���ٶ�")]
    public float linearVelocity = 0;
    [Header("����ļ��ٶ�")]
    public float acceleration = 0;
    [Header("����ĳ�ʼ���ٶ�")]
    public float angularVelocity = 0;
    [Header("����ĽǼ��ٶ�")]
    public float angularAcceleration = 0;
    [Header("���������ٶ�")]
    public float maxVelocity = int.MaxValue; 

    [Header("����������")]
    [Header("����������ĳ�ʼ��ת")]
    public float initRotation = 0;
    [Header("����������ĳ�ʼ���ٶ�")]
    public float senderAngularVelocity = 0;
    [Header("����������������ٶ�")]
    public float senderMaxAngularVelocity = int.MaxValue;
    [Header("����������ļ��ٶ�")]
    public float senderAcceleration = 0;
    [Header("�����ӵ�������")]
    public int count = 0;
    [Header("ÿ�����ɵ��ӵ�֮��ĽǶ�")]
    public float lineAngle = 30;
    [Header("ÿ�����ɵ��ӵ�֮���ʱ��")]
    public float interval = 0.1f;

    [Header("prefab")]
    public GameObject prefab;
}
