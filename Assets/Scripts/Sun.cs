using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    [SerializeField]
    List<Transform> glows;

    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float scaleSpeed;

    [SerializeField]
    float scaleAmount;

    Dictionary<Transform, Vector3> glowScales = new Dictionary<Transform, Vector3>();

    private void Start()
    {
        foreach (var g in glows)
            glowScales[g] = g.localScale;
    }

    // Update is called once per frame
    private void Update()
    {
        // rotate
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

        // scale glows
        var temp = 1 + (scaleAmount * Mathf.Sin(Time.time * scaleSpeed));
        foreach (var g in glows)
        {
            g.localScale = glowScales[g] + new Vector3(temp, temp, 1);
        }
    }
}
