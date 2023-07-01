using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ļ�����ͳһ�˶�Ӧ����Ĵ�������������ײ������˳�
/// ����ǿ�����ƽ̨���������Ƿ����鲢�Ҷ�Ӧ�ľ����Ƿ���ʾ
/// </summary>
public class BrokenPlatform : Trigger2DBase
{
    public float brokeTime;
    public float recreatTime;
    public bool notRecreat;
    public Animator animator;
    protected override void enterEvent()
    {
        
        StartCoroutine(platfromBroke());
    }

    IEnumerator platfromBroke()
    {
        animator.Play("broken");
        animator.speed = 1 / (brokeTime * 2);
        yield return new WaitForSeconds(brokeTime);
        hide();
        yield return new WaitForSeconds(recreatTime);
        show();
    }

    void hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (var c in GetComponents<Collider2D>())
        {
            c.enabled = false;
        }

    }

    void show()
    {
        if (notRecreat) return;
        GetComponent<SpriteRenderer>().enabled = true;
        foreach (var c in GetComponents<Collider2D>())
        {
            c.enabled = true;
        }
    }
}
