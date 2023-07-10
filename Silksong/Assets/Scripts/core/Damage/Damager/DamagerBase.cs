using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class LayerMaskExtensions//layerMask增加contains方法 判断gameObject是否在layerMask中
{
    public static bool Contains(this LayerMask layers, GameObject gameObject)
    {
        return 0 != (layers.value & 1 << gameObject.layer);
    }
}

/// <summary>
/// 
/// damager的抽象基类 
/// </summary>作者：青瓜

public abstract class DamagerBase : MonoBehaviour
{
    public bool ignoreInvincibility = false;//无视无敌
    public bool canDamage = true;
    public int damage;//伤害数值
    public Vector2 beatBackVector = Vector2.zero;

    protected Vector3 hitpos;

    public DamageEvent makeDamageEvent;
    public DamageEvent killDamableEvent;
    public UnityEvent<GameObject> hitAnythingEvent;


    public virtual int getDamage(DamageableBase target)//获得造成的具体伤害数值
    {
        return damage;
    }
 
    protected abstract void makeDamage(DamageableBase target);//造成伤害后的效果

    protected virtual void OnTriggerEnter2D(Collider2D collision)//根据gameobjrect的layer和project setting 确定哪些layer参与碰撞
    {
        if(!canDamage)
        {
            return;
        }
        hitAnythingEvent.Invoke(collision.gameObject);
        DamageableBase damageable = collision.GetComponent<DamageableBase>();//只有拥有Damageable组件的collider受攻击
        if (damageable )
        {
            //Debug.Log(damageable.gameObject.name + " ontrigger");
            if (!ignoreInvincibility && damageable.invulnerable)
            {
                return;
            }
            hitpos =  damageable.GetComponent<Collider2D>().ClosestPoint(this.transform.position);
            damageable.takeDamage(this);
            makeDamage(damageable);

        }
    }
}
