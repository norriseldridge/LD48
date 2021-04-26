using System.Collections.Generic;
using UnityEngine;

public class BlueFishSchool : MonoBehaviour
{
    [SerializeField]
    GameObject source;

    [SerializeField]
    int size;

    [SerializeField]
    float distance;

    [SerializeField]
    float speed;

    [SerializeField]
    float moveSpeed;

    List<Transform> fish = new List<Transform>();
    List<float> offsets = new List<float>();
    float t;
    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        PickNewDestination();
        for (int i = 0; i < size; ++i)
        {
            fish.Add(Instantiate(source, transform).transform);
            offsets.Add(Random.Range(1.0f, 4.0f) * (i % 2 == 0 ? 1 : -1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // move the school
        if (Vector3.Distance(transform.position, destination) <= speed)
        {
            PickNewDestination();
        }

        Vector3 vectorToTarget = transform.position - destination;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * moveSpeed);
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // move all the fish
        for (int i = 0; i < size; ++i)
        {
            var d = offsets[i] * distance;
            fish[i].localPosition = new Vector3(Mathf.Sin(t + i) * d,
                Mathf.Cos(t + i) * d,
                0);
        }
        t += speed * Time.deltaTime;
    }

    private void PickNewDestination()
    {
        destination = new Vector3(Random.Range(-200, 200), Random.Range(-150, -20), 0);
    }

    public void Reset() => PickNewDestination();
}
