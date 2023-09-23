using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MagnetableItem : MonoBehaviour
{
    Rigidbody2D rb;//��ü�� �ӵ��� 0���� �� �� ���.

    Transform target;//�÷��̾��� ��ġ ��, Magnet�� �ݰ濡 ���Դ��� Ȯ����
    float MagneticConst;//�ڷ� ���
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        target = null;
    }
    
    //�������� �Ծ� ��Ȱ��ȭ �� ��, Ÿ���� ���ش�
    private void OnDisable()
    {
        target = null;
    }

    private void FixedUpdate()
    {
        if (target == null)//Ÿ���� ���� ��� - �ڷ� ������ ������ ���� ���.
            return;

        Vector2 directionVect = target.position - transform.position; // ���� -> �÷��̾� ���� ���ϱ�
        float distance = directionVect.magnitude; //�Ÿ� ���ϱ�
        Vector2 MagneticForce = directionVect.normalized * MagneticConst / distance; // �ڱ�� ���ϱ�
        if (MagneticForce.magnitude < 0.5f) //�ڱ���� �ʹ� �۴ٸ�(= �ӵ��� �ʹ� �����ٸ�) ���� ����(0.5)���� ����
            MagneticForce = MagneticForce.normalized * 0.5f;
        transform.Translate(MagneticForce * Time.fixedDeltaTime); //������ ��ǥ�� ���� * ������ ��ŭ �̵���Ű��
        rb.velocity = Vector2.zero; // �ӵ��� ������ �����ʵ��� �ӵ��� ����
    }

    //Player �ڽ� ������Ʈ�� Magnet�� Collider�� ����, ����ġ ���� �ڷ��� ������ �޴� �������� ���� ��� �� �Լ��� ȣ��Ǿ� target�� �ڷ��� Ȱ��ȭ�ǰ�
    //���� Player������ �����̴ٰ� Player�� �ڽ� ������Ʈ�� Collector�� Collider���� ������ �����ϵ��� ó��.
    public void ActiveMagnet(float radius)
    {
        target = GameManager.Instance.player.transform;
        this.MagneticConst = radius * 2f;
    }
}
