using UnityEngine;

public class DirtCloud : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed;

    [SerializeField]
    float fadeSpeed;

    [SerializeField]
    new SpriteRenderer renderer;

    [SerializeField]
    Rigidbody2D rb;

    float alpha = 1;

    private void Start()
    {
        rb.AddForce(Vector2.up * Random.Range(1, 1.22f), ForceMode2D.Impulse);
        transform.localScale = Vector3.one * Random.Range(1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, fadeSpeed * Time.deltaTime);
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        alpha = Mathf.MoveTowards(alpha, 0, fadeSpeed * Time.deltaTime);

        if (alpha <= 0.1f)
            Destroy(gameObject);
    }
}
