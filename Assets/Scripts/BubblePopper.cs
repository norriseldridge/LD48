using UnityEngine;

public class BubblePopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var bubble = collision.gameObject.GetComponent<Bubble>();
        if (bubble != null)
        {
            bubble.Pop();
        }
    }
}
