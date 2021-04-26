using UnityEngine;

public class JetBubbleSpawner : MonoBehaviour
{
    [SerializeField]
    float maxDelay;

    float currentDelay = 0;

    public void Spawn()
    {
        currentDelay -= Time.deltaTime;
        if (currentDelay <= 0.0f)
        {
            currentDelay = Random.Range(maxDelay / 2, maxDelay);
            var bubble = BubblePool.Instance.Next();
            bubble.AddVariance();
            bubble.transform.position = transform.position;
        }
    }
}
