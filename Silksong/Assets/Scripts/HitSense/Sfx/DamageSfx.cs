using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSfx : damageEventSetter
{
    public SfxSO sfxSO;

    public override void damageEvent(DamagerBase damager, DamageableBase damageable)
    {
        if (damageTiming!=DamageTiming.onMakeDamage)//��ʱֻ��Ҫ�ܻ���Ч
            //��������Ƕ�ȡ��Ӧλ�ò��������ܻ���Ч������к�����������Ч���Բο�
        {
            Vector2 hittedPosition = Vector2.zero;
            hittedPosition = GetComponent<Collider2D>().bounds.ClosestPoint(damager.transform.position);
            SfxManager.Instance.creatHittedSfx(hittedPosition, hittedPosition - (Vector2)transform.position, sfxSO);
        }
    }
}
