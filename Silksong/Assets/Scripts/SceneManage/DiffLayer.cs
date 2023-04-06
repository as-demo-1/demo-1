using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ƶ�ʱͼƬ����ƶ������쾰��Ч����
/// </summary>ʹ�÷�����scene�ж�Ӧ�����εĸ�������ش˽ű�����������ͳһ���ոþ�������ƶ���
public class DiffLayer : MonoBehaviour
{
   [SerializeField,Tooltip("ˮƽ�ƶ����ʣ�����Ϊ����,Զ��Ϊ����")]
    float horizontalMultiplier = 0.0f;

    [SerializeField, Tooltip("��ֱ�ƶ����ʣ�����Ϊ����,Զ��Ϊ����")]
    float verticalMultiplier = 0.0f;

    private Transform cameraTransform;

    //[SerializeField,Tooltip("ָ��һ�����λ�ã���λ��������û������ƶ�,�����ƫ���λ��ʱ�����忪ʼ����ƶ�")]
    Vector3 absCameraPos;

    private Vector3 startPos;

    void Start()
    {
        SetupStartPositions();
    }

    void SetupStartPositions()
    {
         cameraTransform = Camera.main.transform;
       // cameraTransform = GameObject.Find("Main Camera").transform;
        //print(cameraTransform.parent.name);
        startPos = transform.position;
        absCameraPos = transform.position;
    }

    void LateUpdate()
    {
        UpdateParallaxPosition();
    }

    void UpdateParallaxPosition()
    {
        var position = startPos;
        position.x += horizontalMultiplier * (cameraTransform.position.x - absCameraPos.x);
        position.y += verticalMultiplier * (cameraTransform.position.y - absCameraPos.y);

        transform.position = position;//
    }

}
