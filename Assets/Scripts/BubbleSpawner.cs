using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField]
    float xRange;

    [SerializeField]
    float maxWait;

    [SerializeField]
    int maxSpawnGroup;

    float currentWait;

    private void Start()
    {
        currentWait = Random.Range(0, maxWait);
    }

    // Update is called once per frame
    private void Update()
    {
        currentWait -= Time.deltaTime;
        if (currentWait <= 0)
        {
            currentWait = Random.Range(maxWait / 2, maxWait);

            int num = Random.Range(1, maxSpawnGroup);
            for (int i = 0; i < num; ++i)
            {
                var bubble = BubblePool.Instance.Next();
                bubble.AddVariance();
                bubble.transform.position = transform.position + new Vector3(Random.Range(-xRange, xRange), 0, 0);
            }
        }
    }
}
