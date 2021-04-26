using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float range;

    Vector3 anchor;

    private void Awake() =>
        anchor = transform.position;

    // Update is called once per frame
    private void Update()
    {
        var x = range * Mathf.Sin(speed * Time.time);
        var y = range * 0.5f * Mathf.Cos(speed * Time.time);
        transform.position = anchor + new Vector3(x, y, 0);
    }
}
