using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum GameState
{
    PreGame,
    InGame,
    PostGame,
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField]
    GameState gameState;
    public GameState GameState
    {
        get => gameState;
        private set => gameState = value;
    }

    public GameObject PreGameContainer;
    public GameObject InGameContainer;
    public GameObject PostGameConatiner;

    public AudioSource MusicAudioSource;
    public AudioSource EffectsAudioSource;

    public Texture2D CursorTexture;
    public Texture2D PickCursorTexture;

    public AudioClip MainMusic;
    public AudioClip EndMusic;
    public float MusicFadeDuration;

    public bool IsPickingObject
    {
        set
        {
            SetCursor(value ? PickCursorTexture : CursorTexture);
        }
    }

    GameObject[] AllContainers => new GameObject[] {
        PreGameContainer,
        InGameContainer,
        PostGameConatiner
    };

    void HideAllContainers() => AllContainers.ToList().ForEach(x => x.SetActive(false));

    public void TransitionToPreGame()
    {
        HideAllContainers();
        PreGameContainer.SetActive(true);
        MusicAudioSource.clip = MainMusic;
        MusicAudioSource.Play();
        GameState = GameState.PreGame;
        SetCursor(CursorTexture);
    }

    public void SetCursor(Texture2D texture)
    {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void TransitionToInGame()
    {
        if (MusicAudioSource.clip != MainMusic)
        {
            MusicAudioSource.clip = MainMusic;
            MusicAudioSource.Play();
        }
        HideAllContainers();
        InGameContainer.SetActive(true);
        GameState = GameState.InGame;
    }

    public void TransitionToPostGame()
    {
        HideAllContainers();
        PostGameConatiner.SetActive(true);
        MusicAudioSource.clip = EndMusic;
        MusicAudioSource.Play();
        GameState = GameState.PostGame;
        SetCursor(CursorTexture);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlayClip(AudioClip clip)
    {
        EffectsAudioSource.PlayOneShot(clip);
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToPreGame();
        //TransitionToInGame();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeMusic(MusicFadeDuration, 0));
    }

    public void FadeInMusic()
    {
        StartCoroutine(FadeMusic(MusicFadeDuration, 1));
    }

    IEnumerator FadeMusic(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = MusicAudioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            MusicAudioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
