using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    private void FixedUpdate()
    {
        //������ ĳ��Ʈ�� ��� ��� ����� ��ȯ
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }
    
    public Transform GetNearTargetFromNotHitedEnemy(List<Transform> hitedTargets)
    {
        Transform result = null;

        Transform lastesthitedTarget = hitedTargets[^1];

        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {
            if (hitedTargets.Contains(target.transform))
                continue;

            float curDiff = Vector3.Distance(lastesthitedTarget.position, target.transform.position);

            if (curDiff < 2.0f || curDiff > 6.0f)
                continue; //������ ������ �־�� �� ���� �� ���Ƽ�.

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }

        }
        return result;
    }

    /*
    //���� ������ �ʴµ� Ȥ�ó� ���� �����׼� ����� �� ���ؾ��� �� ������ ������ ���� �ڵ�.
    public Transform GetNearTargetFromEnemy(Transform hitedTarget)
    {
        Transform result = null;
        float diff = 100;

        foreach (RaycastHit2D target in targets)
        {

            float curDiff = Vector3.Distance(hitedTarget.position, target.transform.position);

            if (curDiff < 2.0f)
                continue; //������ �������� �־�� �� ���� �� ���Ƽ�.

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }

        }
        return result;
    }*/
}
