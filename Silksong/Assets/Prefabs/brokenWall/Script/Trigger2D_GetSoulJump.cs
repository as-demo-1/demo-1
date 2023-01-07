 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger2D_GetSoulJump : Trigger2DBase
{
    public GameObject tip;
    public KeyCode EnterKey;
    string guid;
    private void Awake()
    {
        SaveSystem _saveSystem = GameManager.Instance.saveSystem;
        guid = GetComponent<GuidComponent>().GetGuid().ToString();
        if (MapObjSaveSystem.ContainsObject(guid) || _saveSystem.ContainDestructiblePlatformGUID(guid))
        {
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        //base.OnTriggerEnter2D(collision);
        tip.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(EnterKey))
        {
            MapObjSaveSystem.AddNewMapObject(guid);
            SoulJump.ifGetSoulJump = true;
            Destroy(gameObject);
            
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        tip.SetActive(false);
    }
}
