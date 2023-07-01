using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.Events;
using Cinemachine.Utility;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;

public class HpDamable :Damable
{
    [SerializeField]
    private int maxHp ;
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    private int currentHp;
    public int CurrentHp
    {
        get { return currentHp; }
    }

    private int tempHp;
    public int TempHp
    {
        get { return tempHp; }
    }
    //ͬ����ǰѪ���Լ���ʱѪ��

    public bool notDestroyWhenDie;

    public bool resetHealthOnSceneReload;

    //[Serializable]
    public class setHpEvent : UnityEvent<HpDamable>
    { }

    public DamageEvent onDieEvent;

    public setHpEvent onHpChange=new setHpEvent();

    public ParticleSystem hurt = default;

    private Dictionary<BuffType, Buff> _buffs = new Dictionary<BuffType, Buff>();

    public GameObject electricMarkPref = default; // ������

    private uint buffindex;



    public override void takeDamage(DamagerBase damager)
    {

        if ( currentHp <= 0)
        {
           // return;
           //����Ӧ�����������0ִ�е��Ǽ��˵ķ����������ܻ�����
        }
        if (hurt)
        {
            Destroy(Instantiate(hurt, transform).gameObject, 1.0f);
        }

        base.takeDamage(damager);

        countDamageNumber(damager);
    }

    private void countDamageNumber(DamagerBase damager)
    {
        int damage = damager.getDamage(this);
        int damageForTempHp = Mathf.Clamp(damage, 0, tempHp);
        int damageForCurrentHp = damage - damageForTempHp;

        addTempHp(-damageForTempHp,damager);
        addCurrentHp(-damageForCurrentHp, damager);
        //���������Ѫ��ͳ�ƺ͸���
    }

   /* public void takeDamage(int number)
    {
        if (currentHp <= 0)
        {
            // return;
        }
        if (hurt)
        {
            Destroy(Instantiate(hurt, transform), 3.0f);
        }
        takeDamageEvent.Invoke(null, this);

        setCurrentHp(currentHp - number);
    }
   */
    public void setCurrentHp(int val,DamagerBase damager=null)
    {
        currentHp = Mathf.Clamp(val,0,MaxHp);

        onHpChange.Invoke(this);

        if (isDead())
        {
            die(damager);//��Ȼ�����������ó�����
        }
    }

    /// <summary>
    /// use this to change currentHp,number can be negative to reduce currentHp
    /// ��Ѫ������number�Ǽ�Ѫ��������ʱ��Ѫ
    /// ��ȷ��Ϊɶ��Ѫ����damable�������ǰ�Ѫ����صĶ�����������
    /// </summary>
    /// <param name="number"></param>
    /// <param name="damager"></param>
    public void addCurrentHp(int number, DamagerBase damager)
    {
        if (number == 0) return;
        setCurrentHp(currentHp + number, damager);

    }

    public void setTempHp(int val, DamagerBase damager=null)
    {
        if (val< 0) val = 0;
        tempHp = val;

        onHpChange.Invoke(this);

        if (isDead())
        {
            die(damager);
        }
    }

    /// <summary>
    /// use this to change tempHp,number can be negative to reduce tempHp
    /// ����temphp�����ʱѪ���Ĺ�����ɶ���ţ���Ҫȷ��
    /// </summary>
    /// <param name="number"></param>
    /// <param name="damager"></param>
    public void addTempHp(int number, DamagerBase damager)
    {
        if (number == 0) return;
        setTempHp(tempHp + number,damager);
    }

    /// <summary>
    /// ��ѯ����û��ûɶ��˵��
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        return CurrentHp <= 0 && TempHp <= 0;
    }

    /// <summary>
    /// ���˵Ļ�ȷ���Ƿ�ݻ���Ҷ��󣬲��Ҵ浵
    /// </summary>
    /// <param name="damager"></param>
    protected virtual void die(DamagerBase damager)
    {
        onDieEvent.Invoke(damager, this);
        damager.killDamableEvent.Invoke(damager, this);

        if (!notDestroyWhenDie)
            Destroy(gameObject);

        Debug.Log(gameObject.name + " die");

        GamingSaveObj<bool> gamingSave;
        if (TryGetComponent(out gamingSave) && !gamingSave.ban)
        {
            gamingSave.saveGamingData(true);
        }

    }

    /// <summary>
    /// ���getbuffӦ�����Ϸ���������switch������Ϊ���������������ˣ�����ֻ��electricmask��������
    /// </summary>
    /// <param name="buffType"></param>
    public void GetBuff(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.ElectricMark:
                ElectricMark eBuff = (ElectricMark)_buffs[BuffType.ElectricMark];
                eBuff.AddOneLayer();
                buffindex = ElectricMark.GetCurrentIndex();
                ElectricMark.AddTarget(buffindex, this);
                ElectricMark.counter++;
                break;
            default:
                break;
        }
    }
    
    /// <summary>
    /// ͬ�ϣ�ȡ��buffҲֻ��electricmask
    /// </summary>
    /// <param name="buffType"></param>
    public void RemoveBuff(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.ElectricMark:
                ElectricMark eBuff = (ElectricMark)_buffs[BuffType.ElectricMark];
                eBuff.ResetLayer();
                eBuff.HidePerformance();
                ElectricMark.RemoveTarget(buffindex);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ����ж�buff��������
    /// </summary>
    /// <param name="buffType"></param>
    /// <returns></returns>
    public bool CanGetBuff(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.ElectricMark:
                if (_buffs.ContainsKey(BuffType.ElectricMark))
                {
                    ElectricMark eBuff = (ElectricMark)_buffs[BuffType.ElectricMark];
                    return eBuff.GetLayerNum() < 1;
                }
                else
                {
                    _buffs.Add(BuffType.ElectricMark, new ElectricMark());
                    return false;
                }
            default:
                return false;
        }
    }

    /// <summary>
    /// �ж�����buff״̬
    /// </summary>
    /// <param name="buffType"></param>
    /// <returns></returns>
    public bool HaveBuff(BuffType buffType)
    {
        switch (buffType)
        {
            case BuffType.ElectricMark:
                if (_buffs.ContainsKey(BuffType.ElectricMark))
                {
                    ElectricMark eBuff = (ElectricMark)_buffs[BuffType.ElectricMark];
                    return eBuff.GetLayerNum() > 0;
                }
                return false;
            default:
                return false;
        }
    }

}
