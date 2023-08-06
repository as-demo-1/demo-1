using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //ȷ����ײ��ȡ
    private BoxCollider2D boxCollider2D;
    //ȷ��������Χ
    private CircleCollider2D circleCollider2D;
    private Rigidbody2D rigidbody2D;

    private float speed = 20f;//��ȡʱ�����ٶ�
    private bool isAttracted = false;//�Ƿ�Ŀ������
    private Vector3 targetPosition;//Ŀ��λ��
    private GameObject targetGameObject;//Ŀ�����
    private float jumpForce = 300f;

    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private LayerMask groundLayerMask;

    public int bounceCount;//�������� unity�༭�ɼ�

    [SerializeField] private int m_BounceCount;
    [SerializeField] private float colliderRadius;//��ȡ����ײ�д�С


    float launchTime = 1.0f;
    bool launch = false;

    private int m_MoneyNum = 0;       //�����Ǯ������
    


    void OnEnable()
    {
        targetGameObject = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        circleCollider2D.radius = colliderRadius;
        m_BounceCount = bounceCount;
    }
     

    private void FixedUpdate()
    {
        LaunchCoin();
        Bounce();
        attract();
    }

    //��������״��ײ��
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (circleCollider2D.IsTouchingLayers(playerLayerMask))//����
        {
            isAttracted = true;
            rigidbody2D.gravityScale = 0;
        }
    }
  
    ///����÷�����ײ��
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (boxCollider2D.IsTouchingLayers(playerLayerMask))
        {
            RecycleCoin();
            //TODO:������ӽ�Ǯ,Ӧ�������������ɷ��¼�
            EventManager.Instance.Dispatch<int>(EventType.onMoneyChange,m_MoneyNum);

        }
    }

    //������
    void LaunchCoin() 
    {
        if (!launch) {
            float _randomx = Random.Range(-1.0f,1.0f);
            float _randomy = Random.Range(1.0f, 1.5f);
            Vector2 dir = new Vector2(_randomx, _randomy);
            rigidbody2D.AddForce(dir * jumpForce);
            launch = true;
        }
    }

    /// <summary>
    /// ��ҵ���
    /// </summary>
    private void Bounce()
    {
        if (IsGround() && !isAttracted && m_BounceCount >= 0)
        {
            if (rigidbody2D.velocity.x != 0)    //��������ʱ�����Ĺ���
            {
                rigidbody2D.velocity = Vector2.zero;
            }
            rigidbody2D.AddForce(Vector2.up * jumpForce);
            m_BounceCount -= 1;
        }
    }
    private bool IsGround()
    {
        return boxCollider2D.IsTouchingLayers(groundLayerMask);
    }

    /// <summary>
    /// ��ұ�����
    /// </summary>
    private void attract()
    {
        if (isAttracted)
        {
            GetTargetPos();
            transform.Translate((-transform.position + targetPosition) * Time.fixedDeltaTime * speed, Space.World);
        }
    }

    private void GetTargetPos() 
    {
        targetPosition = targetGameObject.transform.position;
    }

    /// <summary>
    /// ���ս��Ԥ��
    /// </summary>
    private void RecycleCoin() 
    {
        isAttracted = false;
        m_BounceCount = bounceCount;
        rigidbody2D.gravityScale = 3;
        launch = false;
		CoinManager.Instance.RecycleCoinsPrefabs(this.gameObject);
    }

    /// <summary>
    /// ���ý�ҵ�Ǯ��
    /// </summary>
    /// <param name="moneyNum"></param>
    public void SetCoinMoneyNum(int moneyNum) 
    {
        m_MoneyNum = moneyNum;
    }

}
