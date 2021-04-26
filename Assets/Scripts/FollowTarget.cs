using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float speed;

    [SerializeField]
    float back;

    private void Update() => Follow();

    protected void Follow()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            target.position + (Vector3.back * back),
            speed * Time.deltaTime);

        var dist = Vector3.Distance(transform.position, target.position + (Vector3.back * back));
        if (dist > 2)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                target.position + (Vector3.back * back), dist - 2);
        }
    }
}
