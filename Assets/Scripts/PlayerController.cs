using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField]
    Transform visual;

    [SerializeField]
    JetBubbleSpawner spawner;

    [SerializeField]
    AudioClip startMoveSFX;

    [SerializeField]
    AudioClip movingSFX;

    [SerializeField]
    AudioSource sfxSource;

    [SerializeField]
    AudioSource movingSfxSource;

    [Header("Movement")]
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    float jetForce;

    [SerializeField]
    float maxSpeed;

    bool flipped = false;

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * jetForce, ForceMode2D.Force);
            spawner.Spawn();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down * jetForce, ForceMode2D.Force);
            spawner.Spawn();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * jetForce, ForceMode2D.Force);
            SetFlip(false);
            spawner.Spawn();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * jetForce, ForceMode2D.Force);
            SetFlip(true);
            spawner.Spawn();
        }

        if (Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.D))
        {
            PlaySFX(sfxSource, startMoveSFX);
        }

        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.D))
        {
            PlaySFX(movingSfxSource, movingSFX);
        }

        // clamp speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void PlaySFX(AudioSource source, AudioClip clip)
    {
        if (!source.isPlaying || source.clip != clip)
        {
            source.clip = clip;
            source.pitch = Random.Range(0.8f, 1);
            source.Play();
        }
    }

    private void SetFlip(bool flip)
    {
        if (flipped == flip) return;

        flipped = flip;
        if (flipped)
        {
            StartCoroutine(Flip(0, 180));
        }
        else
        {
            StartCoroutine(Flip(180, 0));
        }
    }

    IEnumerator Flip(float from, float to)
    {
        visual.rotation = Quaternion.Euler(0, from, 0);
        if (from < to)
        {
            for (float i = from; i < to; i += 2)
            {
                visual.rotation = Quaternion.Euler(0, i, 0);
                yield return null;
            }
        }

        if (from > to)
        {
            for (float i = from; i > to; i -= 2)
            {
                visual.rotation = Quaternion.Euler(0, i, 0);
                yield return null;
            }
        }

        visual.rotation = Quaternion.Euler(0, to, 0);
    }
}
