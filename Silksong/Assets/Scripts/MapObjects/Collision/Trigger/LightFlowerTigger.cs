using UnityEngine;
/// <summary>
/// �����Ĵ�����
/// </summary>���ߣ�������
public class LightFlowerTigger : Trigger2DBase
{
    public BurstFlowerCollider flower;
    protected override void enterEvent()
    {
        if (flower)
        {
            flower.Burst();
            Debug.Log("������");
        }
    }

    protected override void exitEvent()
    {
        flower.Back();
        Debug.Log("��ȥ��");
    }
}
