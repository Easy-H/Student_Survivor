using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public RuntimeAnimatorController[] animCon;

    //���� ��ġ = �տ���
    [SerializeField]
    public float attackRate; //���ݷ� ����
    public float speedRate; //�ӵ� ����
    public float defenseRate; //���� ����
    //public float magneticRate; // �ڼ� ���� - ����� �������� �̰ͻ��̶� �̰͸� �׽�Ʈ �Ǿ�������, �ٸ� �����۵� �߰��Ͽ� ������ ������ ȹ������ ���� �׽�Ʈ�� �����ؾ���
    public bool isInvincible; // ����

    private List<BuffData> buffs; //���� ���
    private WaitForSeconds wait; //���� �ð� ���� WaitForSeconds ��ü (0.1��)

    public SkillManager skillManager;


    public float spawnSkillCoolDownRate = 1f;
    public float attackSkillCoolDownRate = 1f;

    public Vector3 Scale
    {
        set
        {
            transform.localScale = value;
            for(int i = 0; i < transform.childCount; i++)
            {
                if (i == 0)
                    continue;
                Transform child = transform.GetChild(i);
                child.localScale = new Vector3(1 / value.x, 1 / value.y, 1 / value.z);
            }
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true); // ��Ȱ��ȭ�� ������Ʈ�� �����Ͽ� �����´�.
        
        attackRate = 1f;
        speedRate = 1f;
        defenseRate = 1f;
        //magneticRate = 1f;
        isInvincible = false;
        buffs = new List<BuffData>();
        wait = new WaitForSeconds(0.1f);
    }
    private void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime * speedRate;
        rigid.MovePosition(rigid.position + nextVec);
    }
    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    //�������� ���� �Ǳ� �� ����Ǵ� �Լ�
    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
            return;
        animator.SetFloat("Speed", inputVec.magnitude);
        //������ ũ��

        if (inputVec.x != 0)
        {
            spriteRenderer.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive)
            return;

        if (isInvincible)
            return;

        GameManager.Instance.health -= Time.deltaTime * 10 / defenseRate;

        if (GameManager.Instance.health < 0)
        {
            //shadow�� area�� ����ΰ� ������ ��Ȱ��ȭ
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            animator.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
    

    //Collector �Լ����� �ش� �Լ��� ȣ���Ͽ� ���� ����
    public void ActivateBuff(BuffData buff)
    {
        if (buffs.Contains(buff))
        {
            buff.ResetTime();
            //Debug.Log("���� �ð� ����");
        }

        else
        {
            //Debug.Log("���� ����");
            switch (buff.effect) //���� ȿ�� ������ ���� ������ ���ݿ� ������ ���Ѵ�.
            {
                case BuffData.BuffEffect.Attack:
                    attackRate *= buff.value;
                    break;
                case BuffData.BuffEffect.Speed:
                    speedRate *= buff.value;
                    break;
                case BuffData.BuffEffect.Defense:
                    defenseRate *= buff.value;          
                    break;
                case BuffData.BuffEffect.Magnetic:
                    GetComponentInChildren<Magnet>().MagneticRate *= buff.value;
                    break;
                case BuffData.BuffEffect.Invincible:
                    isInvincible = true;
                    break;
            }
            buffs.Add(buff); //���� ����Ʈ�� ������ �߰��Ѵ�
            StartCoroutine(BuffRoutine(buff, () =>  //���� ���ӽð��� ����ϴ� �ڷ�ƾ�� �����Ѵ�. ������ ������ ���ٽ� �Լ��� ȣ��ȴ�.
            {
                //Debug.Log("���� ���ӽð� ��");
                switch (buff.effect) //���� ������ ���� ����Ǵ� ������ ������ ��ŭ ���� ���ҽ�Ų��
                {
                    case BuffData.BuffEffect.Attack:
                        attackRate /= buff.value;                       
                        break;
                    case BuffData.BuffEffect.Speed:
                        speedRate /= buff.value;
                        break;
                    case BuffData.BuffEffect.Defense:
                        defenseRate /= buff.value;
                        break;
                    case BuffData.BuffEffect.Magnetic:
                        GetComponentInChildren<Magnet>().MagneticRate /= buff.value;
                        break;
                    case BuffData.BuffEffect.Invincible:
                        isInvincible = false;
                        break;
                }
                buffs.Remove(buff); //���� ��Ͽ��� �����Ѵ�.
                buff.ResetTime(); //������ �ٽ� ���� �������� �԰� �� ��� ���ӽð��� ���������� ����ǵ��� �ʱ�ȭ�����ش�.
            }));
        }
    }
    private IEnumerator BuffRoutine(BuffData buff, System.Action done)
    {//���� ������ ��ü���� ���� ���ӽð��� �귯�� �ð���ŭ ��� ���ҽ�Ű�ٰ�
        //���ӽð��� 0�� �� ��� ������ ������.
        float remainTime;
        while ((remainTime = buff.GetRemainTime()) >= 0f)
        {
            remainTime -= 0.1f;
            buff.SetRemainTime(remainTime);
            yield return wait;
        }
        done.Invoke();
    }


}



/*
public class Player : MonoBehaviour
{
    //public���� ���� �� Add Component ��� �� Input Vec�� ����
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }



    void Update()
    {
        //Input Ŭ���� = ����Ƽ���� �޴� ��� �Է��� �����ϴ� Ŭ����
        //Project Settint-Input Manager���� Ȯ��
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
        //�׳� GetAxis���� ������ ��. 
    }

    //������ ���ؼ��� FixedUpdate
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        //normalized = �밢������ �̵��� �� �ӵ��� �����ϴ� ���� ����
        //fixed delta time = ���� ������ �ϳ��� �Һ��� �ð�
        //delta time = update���� ���

        //�̵����
        //1.���� �ش�
        //rigid.AddForce(inputVec);

        //2.�ӵ� ����
        //rigid.velocity = inputVec;

        //3.��ġ �̵�
        //�Ű������� ������� ��ġ�� �ޱ⶧���� �����־���Ѵ�.
        rigid.MovePosition(rigid.position + nextVec);

        //������ 1. �ʹ� ����, 2.�����ӿ� ���� �̵��ӵ� �ٸ� �� ����
    }
}
*/