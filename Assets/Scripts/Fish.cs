using UnityEngine;

public class Fish : MonoBehaviour, IResetable
{
    [SerializeField]
    float speed;

    [SerializeField]
    Transform fin;

    Vector3 destination;

    private void Start() => PickNewDestination();

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(transform.position, destination) <= speed)
        {
            PickNewDestination();
        }

        Vector3 vectorToTarget = transform.position - destination;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        float abs = Mathf.Abs(angle);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        if (abs > 90 && abs < 270)
        {
            q *= Quaternion.Euler(180, 0, 0);
            fin.localPosition = new Vector3(fin.localPosition.x, fin.localPosition.y, 0.25f);
        }
        else
        {
            fin.localPosition = new Vector3(fin.localPosition.x, fin.localPosition.y, -0.25f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) => PickNewDestination();

    private void PickNewDestination()
    {
        // destination = new Vector3(Random.Range(-200, 200), Random.Range(-150, -20), 0);
        var player = FindObjectOfType<PlayerController>();
        destination = player.transform.position;
    }

    public void Reset() => PickNewDestination();
}
