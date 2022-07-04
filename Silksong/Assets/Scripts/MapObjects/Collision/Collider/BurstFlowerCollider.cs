using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///���ϸ�������� ֹͣ��ȡ���˺�
/// </summary>���ߣ����
public class BurstFlowerCollider : Collider2DBase
{
    public GameObject damager;
    public float speed;
    public Vector3 destination;
    public Vector3 homeposition;
    private float distance;
    protected override void enterEvent()//����ground layer
    {
        damager.SetActive(false);
        //Debug.Log("����ȥ��");
        //GetComponent<Rigidbody2D>().transform.position = Vector2.MoveTowards(gameObject.transform.localPosition, homeposition, speed);
    }

    public void Burst()//��ʼ����
    {
        damager.SetActive(true);
        distance = Vector2.Distance(gameObject.transform.position, destination);
        //GetComponent<Rigidbody2D>().gravityScale = speed;
        transform.localPosition = Vector2.MoveTowards(gameObject.transform.localPosition, destination, speed);
        //transform.localPosition = Vector2.MoveTowards(gameObject.transform.localPosition, homeposition, speed);

    }
    public void Back()//��ʼ����
    {
        damager.SetActive(false);
        distance = Vector2.Distance(gameObject.transform.position, destination);
        //GetComponent<Rigidbody2D>().gravityScale = speed;
        //transform.localPosition = Vector2.MoveTowards(gameObject.transform.localPosition, destination, speed);
        transform.localPosition = Vector2.MoveTowards(gameObject.transform.localPosition, homeposition, speed);

    }
}
