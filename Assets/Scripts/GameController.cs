using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    Playing,
    GameOver
}

public class GameController : MonoBehaviour
{
    const string END_SCENE = "End";

    public static GameController Instance { get; private set; }

    [SerializeField]
    new Camera camera;

    [SerializeField]
    TitleScreen titleScreen;

    [SerializeField]
    PlayerController player;

    [SerializeField]
    Message message;

    [Header("Intro Sequence")]
    [SerializeField]
    float panSpeed;

    [Header("Game Play")]
    [SerializeField]
    AudioSource ambient;

    [SerializeField]
    AudioSource music;

    [SerializeField]
    AudioMixer sfxMixer;

    private GameState state = GameState.Menu;
    private bool introStarted = false;

    private void Awake() => Instance = this;

    private void Start()
    {
        Cursor.visible = false;
        ambient.volume = 0;
        camera.GetComponent<CameraFollow>().enabled = false;
        player.enabled = false;
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.Menu:
                UpdateMenu();
                break;

            case GameState.Playing:
                UpdateGamePlay();
                break;
        }
    }

    private void UpdateMenu()
    {
        if (Input.anyKeyDown && !introStarted)
        {
            StartCoroutine(IntroSequence());
        }
    }

    IEnumerator IntroSequence()
    {
        introStarted = true;

        // start the game logic here
        titleScreen.FadeText();

        // pan to player
        while (camera.transform.position.y > player.transform.position.y)
        {
            camera.transform.position += Vector3.down * panSpeed * Time.deltaTime;
            yield return null;
        }

        // start following the player
        camera.GetComponent<CameraFollow>().enabled = true;

        // enabled the player
        player.enabled = true;

        // prompt the player about controls
        message.PushMessage("Use WASD to move.");

        state = GameState.Playing;
    }

    private void UpdateGamePlay()
    {
        if (ambient.volume < 0.3f)
            ambient.volume = Mathf.Lerp(ambient.volume, 0.3f, Time.deltaTime);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(END_SCENE, LoadSceneMode.Additive);
        state = GameState.GameOver;
        StartCoroutine(OutroSequence());
    }

    IEnumerator OutroSequence()
    {
        // disable the player
        player.enabled = false;

        // fade ambient
        var v = ambient.volume;
        while (v > 0.0f)
        {
            ambient.volume = v;
            v -= Time.deltaTime;
            yield return null;
        }

        // fade sfx group
        var fadeSpeed = 10;
        if (sfxMixer.GetFloat("volume", out float sfxVolume))
        {
            while (sfxVolume > -80.0f)
            {
                sfxMixer.SetFloat("volume", sfxVolume);
                sfxVolume -= fadeSpeed * Time.deltaTime;
            }
        }
    }
}
