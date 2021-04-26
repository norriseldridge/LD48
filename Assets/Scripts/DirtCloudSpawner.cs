using UnityEngine;

public class DirtCloudSpawner : MonoBehaviour
{
    [SerializeField]
    DirtCloud source;

    float lastTime = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            var cloud = Instantiate(source);
            cloud.transform.position = collision.gameObject.transform.position + Vector3.back;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            if (Time.time - lastTime >= 0.75f)
            {
                lastTime = Time.time;
                var cloud = Instantiate(source);
                cloud.transform.position = collision.gameObject.transform.position + Vector3.back;
            }
        }
    }
}
