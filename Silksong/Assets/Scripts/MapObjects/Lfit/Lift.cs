using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// ����1.0
/// </summary>���ߣ����
public class Lift : MonoBehaviour
{
    public int maxFloor;//��1��ʼ����

    private LiftFloorGear[] gears;//liftΪ�õ��ݵ�liftFloorGear��startʱ�󶨵�������  ���ӵͲ㵽�߲�˳��

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public float currentFloor;//��ǰ�� ��Ϊx.5�����ʾ����x��x+1��֮���ƶ�

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public int targetFloor;//����Ҫ���Ĳ�

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public int midTargetFloor;//���ڲ��Ŀ���֮����м�� �����ڿ����ƶ��м�¼���ݵ�ǰλ��


    private float midFloorHeight;//�м��ĵ���y��߶�
    private float liftFloorDistance;

    public float speed;
    private float arriveDistance;//��������Ŀ�ĵؾ���С�ڴ�ֵʱ �ж�����

    // private PlayerController player;
    private Rigidbody2D playerRigid;

    private Rigidbody2D rigid;
    private BoxCollider2D floorCollider;//���ݵ������ײ��

#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public bool playerIsOnLift = false;//����Ƿ��ڵ����� ����ͬ���ٶ�

    void Awake()
    {
        gears = new LiftFloorGear[maxFloor];
        rigid = GetComponent<Rigidbody2D>();
        floorCollider = GetComponent<BoxCollider2D>();

    }
    void Start()
    {
        GameObject playerobj = GameObject.FindGameObjectWithTag("Player");
        if (playerobj)
        {
            //player = playerobj.GetComponent<PlayerController>();
            playerRigid = playerobj.GetComponent<Rigidbody2D>();
        }
        arriveDistance = speed * Time.fixedDeltaTime;
        liftFloorDistance = floorCollider.offset.y;
        liftFloorDistance += floorCollider.bounds.extents.y;
    }

    public void setFloorGear(LiftFloorGear gear)
    {
        gears[gear.floor - 1] = gear;
    }

    private float getFloorPosition()
    {
        return transform.position.y + liftFloorDistance;
    }

    private void Update()
    {
        playerIsOnLift = (floorCollider.IsTouchingLayers(1 << LayerMask.NameToLayer("Player")) && playerRigid.transform.position.y > getFloorPosition());

        if (rigid.velocity.y != 0)//�������ƶ�
        {
            float distance = getFloorPosition() - midFloorHeight;
            //Debug.Log(distance);
            if (Mathf.Abs(distance) < arriveDistance)//�ж�����
            {
                // Debug.Log("lift arrive a floor");
                currentFloor = midTargetFloor;//������ĳһ��

                if (midTargetFloor == targetFloor)//����Ŀ�Ĳ�
                {
                    rigid.MovePosition(new Vector3(transform.position.x, transform.position.y - distance, transform.position.z));
                    //�ϸ�������  �����ҵ���ײ������Բ���Բ��ϸ���� ��Ҳ������       
                    rigid.velocity = Vector2.zero;
                    if (playerIsOnLift)
                    {
                        playerRigid.velocity = new Vector2(playerRigid.velocity.x, 0);
                        playerRigid.MovePosition(new Vector2(playerRigid.transform.position.x, playerRigid.transform.position.y - distance));
                        //Debug.Log("stop");
                    }
                }
                else//�����ƶ� ����¥��
                {
                    if (rigid.velocity.y > 0)
                        moveUp();
                    else moveDown();
                }

            }
        }
    }
    void FixedUpdate()
    {

    }



    /// <summary>
    /// ���ݿ��ؿ��Ƶ��ݵĽӿں���
    /// </summary>
    public void setTargetFloor(int floor)//����ʱ�Ѿ���֤floorһ���Ϸ��Ҳ�����currentfloor
    {

        targetFloor = floor;

        float distance = floor - currentFloor;
        float moveSpeed;
        if (distance > 0)
        {
            moveSpeed = speed;
            moveUp();
        }
        else
        {
            moveSpeed = -speed;
            moveDown();
        }

        rigid.velocity = new Vector2(0, moveSpeed);
        if (playerIsOnLift)
        {
            playerRigid.velocity = new Vector2(playerRigid.velocity.x, moveSpeed);
            //Debug.Log("with");
        }

    }    //Ŀǰ��һ�ι����򵽶������δ������ ������ܻ��Ļ���ΪĿ��

    public void moveUp()//����һ��
    {
        midTargetFloor = (int)Mathf.Floor(currentFloor) + 1;//����ȡ����+1 ��ʾ��һ�� 
        currentFloor = midTargetFloor - 0.5f;//��ʾ������mid���˶�
        midFloorHeight = gears[midTargetFloor - 1].floorHeight;//��Ӧ¥��ĵ���λ��
    }

    public void moveDown()
    {
        midTargetFloor = (int)Mathf.Ceil(currentFloor) - 1;//����ȡ����-1 ��ʾ��һ�� 
        currentFloor = midTargetFloor + 0.5f;//��ʾ������mid���˶�
        midFloorHeight = gears[midTargetFloor - 1].floorHeight;//��Ӧ¥��ĵ���λ��
    }
}

