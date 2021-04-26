using UnityEngine;

public class Seaweed : MonoBehaviour
{
    float rotationRange;
    float rotationSpeed;
    float resetTime;

    private void Start() => Reset();

    private void Reset()
    {
        resetTime = Random.Range(5, 10);
        rotationRange = Random.Range(4, 5);
        rotationSpeed = Random.Range(0.4f, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        resetTime -= Time.deltaTime;
        if (resetTime < 0.0f)
        {
            Reset();
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.Euler(0, 0, Mathf.Sin(Time.time * rotationSpeed) * rotationRange),
            rotationSpeed * Time.deltaTime);
    }
}
