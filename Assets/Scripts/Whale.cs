using UnityEngine;

public class Whale : MonoBehaviour, IResetable
{
    [SerializeField]
    float speed;

    [SerializeField]
    AudioSource sfx;

    [SerializeField]
    float maxSFXDelay;

    [SerializeField]
    Transform fin1;

    [SerializeField]
    Transform fin2;

    Vector3 destination;

    float currentSFXDelay;

    private void Start()
    {
        currentSFXDelay = Random.Range(maxSFXDelay / 2, maxSFXDelay);
        PickNewDestination();
    }

    // Update is called once per frame
    private void Update()
    {
        currentSFXDelay -= Time.deltaTime;
        if (currentSFXDelay <= 0.0f)
        {
            currentSFXDelay = Random.Range(maxSFXDelay / 2, maxSFXDelay);
            sfx.Play();
        }

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
            fin1.localPosition = new Vector3(fin1.localPosition.x, fin1.localPosition.y, 0.25f);
            fin2.localPosition = new Vector3(fin2.localPosition.x, fin2.localPosition.y, -0.25f);
        }
        else
        {
            fin1.localPosition = new Vector3(fin1.localPosition.x, fin1.localPosition.y, -0.25f);
            fin2.localPosition = new Vector3(fin2.localPosition.x, fin2.localPosition.y, 0.25f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) => PickNewDestination();

    private void PickNewDestination() =>
        destination = new Vector3(Random.Range(-200, 200), Random.Range(-150, -20), 0);

    public void Reset() => PickNewDestination();
}
