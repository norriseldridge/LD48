using UnityEngine;
using UnityEngine.UI;

public class ToggleFadeText : MonoBehaviour
{
    [SerializeField]
    Text text;

    [SerializeField]
    float speed;

    // Update is called once per frame
    void Update() =>
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1 + Mathf.Sin(speed * Time.time));
}
