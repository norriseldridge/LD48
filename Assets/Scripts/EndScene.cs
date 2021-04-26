using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Text text;

    [SerializeField]
    ToggleFadeText anyKeyText;

    [SerializeField]
    float speed;

    AudioSource music;
    float alpha = 0;
    bool hasQuit = false;

    private void Awake()
    {
        music = FindObjectOfType<MusicController>().GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha += speed * Time.deltaTime;
        image.color = new Color(0, 0, 0, alpha);
        text.color = new Color(1, 1, 1, alpha);

        if (alpha >= 1.0f && !anyKeyText.gameObject.activeSelf)
        {
            anyKeyText.gameObject.SetActive(true);
        }

        if (alpha >= 1.0f && Input.anyKeyDown && !hasQuit)
        {
            StartCoroutine(FadeToQuit());
        }
    }

    IEnumerator FadeToQuit()
    {
        hasQuit = true;

        var v = music.volume;
        while(v > 0)
        {
            v -= Time.deltaTime;
            music.volume = v;
            yield return null;
        }
        
        Application.Quit();
    }
}
