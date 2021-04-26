using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField]
    new Light2D light;

    [SerializeField]
    float speed;

    void Update() => light.intensity = 1 + Mathf.Sin(Time.time * speed);
}
