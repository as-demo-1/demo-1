using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSceneLift : SaveLift
{
    public bool UpLift;
    public string pairLiftGuid;
    public string Guid;
    int pairFloor;
    private void OnValidate()
    {
        Guid = GetComponent<GuidComponent>().GetGuid().ToString();
    }
    private void Awake()
    {
        IntGamingSave gamingSave;
        if (TryGetComponent(out gamingSave))
        {
            Debug.Log("try");
            bool error;
            int savedFloor = gamingSave.loadGamingData(out error);
            pairFloor = GetPairLiftFloor(out error);
            Debug.Log(error);
            if (error) return;
            currentFloor = savedFloor;
            Debug.Log("cf:" + currentFloor + " pf" + pairFloor);
            LoadFloor(currentFloor, pairFloor);
        }
    }
    public void SaveSelfFloor(int floor)
    {
        IntGamingSave gamingSave;
        if (TryGetComponent(out gamingSave))
        {
            gamingSave.saveGamingData(floor);
        }
    }  
    public void SavePairFloor(int floor)
    {
        GameManager.Instance.gamingSave.addIntGamingData(floor, pairLiftGuid);
    }
    public void SaveDownFloor()
    {
        if (pairFloor == 0)
        {
            return;
        }
        if(transform.position.y> floorList[1].transform.position.y)
        {
            SavePairFloor(2);
            if (currentFloor == 0) SaveSelfFloor(1);
        }
        else
        {
            SavePairFloor(1);
        }
    }
    public void MoveToFloor_Multy(int target)
    {
        int dir=target>currentFloor ? 1 : -1;
        StartCoroutine(Moving_Multy(currentFloor+dir,Mathf.Abs(target-currentFloor),dir));
    }
    IEnumerator Moving_Multy(int nextFloor, int time, int dir)
    {
        SaveSelfFloor(nextFloor);
        Debug.Log("save "+nextFloor + "  " + time + "  ");
        Vector2 target = floorList[nextFloor].transform.position;
        ifMoving = true;
        Vector2 moveTarget = new Vector2(0, target.y - transform.position.y).normalized;
        rb.velocity = moveTarget * moveSpeed;
        while (Mathf.Abs(transform.position.y - target.y) > 0.1)
        {
            //transform.position = Vector2.MoveTowards(transform.position,
            //    moveTarget, moveSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        rb.velocity = Vector2.zero;
        time--;
        if (time > 0)
        {
            StartCoroutine(Moving_Multy(nextFloor + dir, time, dir));
        }
    }


    public void LoadFloor(int currentFloor,int pairFloor)
    {
        if (UpLift)//
        {
            if (pairFloor == 0)//����²�����ڵ�0�� ˵���²����û������ô�������Ҳ�������ڵ�0�㣨���ز㣩
            {
                currentFloor = 0;
                SaveSelfFloor(0);
                transform.position = new Vector2(transform.position.x, floorList[currentFloor].transform.position.y);
            }
            else if (pairFloor == 2)//����²�����ڵ�2�㣬˵���²�����Ѿ�������
            {
                //���ʱ�������ǰ����2��˵��֮ǰ���ڵڶ����ˣ�����������2����
                if (currentFloor == 2)
                {
                    transform.position = new Vector2(transform.position.x, floorList[currentFloor].transform.position.y);
                }
                //�������2��˵���������ڴ��·������ߣ����ʱ�����Ҫ��1������2
                else
                {
                    transform.position = new Vector2(transform.position.x, floorList[currentFloor].transform.position.y);
                    MoveToFloor(2);
                }
            }
            //����²���1��˵���Ӱ�·�������ˣ����ʱ����Ǵ�0����2������
            else if (pairFloor == 1)
            {
                currentFloor = 0;
                transform.position = new Vector2(transform.position.x, floorList[currentFloor].transform.position.y);
                MoveToFloor(2);
            }
        }
        else//������²����
        {
            //�ڵڼ���������õ��ڼ���
            transform.position = new Vector2(transform.position.x, floorList[currentFloor].transform.position.y);
            if (pairFloor == 2)//����ϲ�����ڵڶ��㣬��ô�²���ݿ϶��ڵڶ��㣨���ز㣩
            {
                currentFloor = 2;
                SaveSelfFloor(2);
                transform.position = new Vector2(transform.position.x, floorList[currentFloor].transform.position.y);
            }
            //�����1�Ļ���˵�������������ģ���ʱ���1�½���0
            else if ( (pairFloor==0&&currentFloor==1))
            {
                MoveToFloor(0);
            }
            //����ϲ�����1���ʱ����أ�˵����·�����ˣ����ʱ���2�½���0
            else if (pairFloor == 1)
            {
                currentFloor = 2;
                transform.position = new Vector2(transform.position.x, floorList[2].transform.position.y);
                MoveToFloor(0);
            }
        }
    }
    private int GetPairLiftFloor(out bool ifError)
    {
        return GameManager.Instance.gamingSave.getIntGamingData(pairLiftGuid, out ifError);
    }
}
