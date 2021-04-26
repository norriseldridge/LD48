using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    public static BubblePool Instance { get; private set; }

    [SerializeField]
    Bubble source;

    [SerializeField]
    int maxSize;
    Queue<Bubble> bubbles = new Queue<Bubble>();

    private void Awake() => Instance = this;

    private void Start()
    {
        for (int i = 0; i < maxSize; ++i)
        {
            var bubble = Instantiate(source);
            bubble.gameObject.SetActive(false);
            bubbles.Enqueue(bubble);
        }
    }

    public Bubble Next()
    {
        var bubble = bubbles.Dequeue();
        bubble.Reset();
        bubbles.Enqueue(bubble);
        return bubble;
    }
}
