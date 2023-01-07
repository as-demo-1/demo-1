using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ڳ����л��󱣴����״̬�ĵ���
/// ���滹���������
/// ���ߣ�С��
/// </summary>
public class SaveLift : MonoBehaviour
{
    public List<SaveLiftFloor> floorList;
    public float moveSpeed;
    [HideInInspector]public int currentFloor;
    bool ifMoving;
    SaveSystem _saveSystem;
    private void Awake()
    {
        _saveSystem = GameManager.Instance.saveSystem;
        int i = 0;
        for (; i < floorList.Count; i++)
        {
            if(MapObjSaveSystem.ContainsObject(floorList[i].guid)
                || _saveSystem.ContainDestructiblePlatformGUID(floorList[i].guid))
            {
                transform.position = new Vector2(transform.position.x, floorList[i].transform.position.y);
                currentFloor = i;
            }
        }
        //if(i==floorList.Count)
    }
    public void MoveToFloor(int n)
    {
        if (!ifMoving)
        {
            if(MapObjSaveSystem.ContainsObject(floorList[currentFloor].guid))
                MapObjSaveSystem.RemoveMapObject(floorList[currentFloor].guid);
            currentFloor = n;
            StartCoroutine(Moving(floorList[n].transform.position));
            MapObjSaveSystem.AddNewMapObject(floorList[currentFloor].guid);
        }
    }
    public void MoveToFloor(SaveLiftFloor floor)
    {
        int n= floorList.IndexOf(floor);    
        MoveToFloor(n);
    }
    IEnumerator Moving(Vector2 target)
    {
        ifMoving = true;
        while(Mathf.Abs(transform.position.y-target.y) > 0.05)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(transform.position.x, target.y), moveSpeed * Time.deltaTime);
            yield return null;  
        }
        ifMoving=false;
    }
    public void MovingUp()
    {
        if (currentFloor + 1 < floorList.Count)
        {
            MoveToFloor(currentFloor + 1);
        }
    }
    public void MovingDown()
    {
        if(currentFloor -1 >= 0)
        {
            MoveToFloor(currentFloor - 1);
        }
    }
    //public void WhenSaveMapObj()
    //{
    //    for (int i = 0; i < floorList.Count; i++)
    //    {
    //        if (_saveSystem.ContainDestructiblePlatformGUID(floorList[i].guid))
    //        {
    //            _saveSystem.RemoveAddDestructiblePlatformGUID(floorList[i].guid);
    //        }
    //    }
    //}
}
