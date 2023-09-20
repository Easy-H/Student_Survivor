using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    //Stay�� ����
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Coin"))
            return;

        //�Ÿ����
        if(( GameManager.Instance.player.transform.position - collision.gameObject.transform.position ).magnitude < 0.5f){
            GameManager.Instance.money++;

            Debug.Log("���ΰ� �浹 �߻�");

            collision.gameObject.SetActive(false);
        }
        else
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce((GameManager.Instance.player.transform.position - collision.gameObject.transform.position).normalized * 2f, ForceMode2D.Force);
        }
    }

    private void LevelUpColliderRadius()
    {
        circleCollider.radius *= 2f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Key Input �߻�");
            LevelUpColliderRadius();
        }
    }
}
