using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum InteractionMode
{
    Normal,
    Paint,
}

public class WorldManager : MonoBehaviour
{
    public static WorldManager instance = null;
    public GameObject ChairPrefab;
    public GameObject ChairCreationPoint;
    [Min(0)]
    public float TotalSeconds;
    public float ElapsedSeconds;
    public InteractionMode InteractionMode;
    public AudioClip ChairAddedAudio;
    public AudioClip ChairPickedUpAudio;
    public AudioClip ChairPaintedAudio;

    public CameraShake UICameraShaker;
    public CameraShake WorldCameraShake;

    public bool IsPaused;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void OnEnable()
    {
        ElapsedSeconds = 0;
        ResetChairs();
        InteractionMode = InteractionMode.Normal;
    }

    void ResetChairs()
    {
        GameObject.FindGameObjectsWithTag("Chair").ToList().ForEach(c => Destroy(c.gameObject));
    }

    void Update()
    {
        // Paused during chapter announcements
        if (IsPaused) return;

        ElapsedSeconds += Time.deltaTime;

        if (ElapsedSeconds >= TotalSeconds)
        {
            ElapsedSeconds = TotalSeconds;
            GameManager.instance.TransitionToPostGame();
        }
    }

    public void CreateChair()
    {
        var instance = Instantiate(ChairPrefab, ChairCreationPoint.transform.position, Quaternion.identity);
        instance.transform.parent = transform;
        var newPosition = instance.transform.position;
        var range = 5;
        newPosition.x += Random.Range(-range, range);
        GameManager.instance.PlayClip(ChairAddedAudio);
        instance.transform.position = newPosition;
    }

    public void Shake(float duration, float intensityModifier = 1)
    {
        WorldCameraShake.shakeModifier = intensityModifier;
        WorldCameraShake.shakeDuration = duration;

        UICameraShaker.shakeModifier = intensityModifier;
        UICameraShaker.shakeDuration = duration;
    }

    public void OnChairPainted()
    {
        GameManager.instance.PlayClip(ChairPaintedAudio);
    }

    public void OnChairPickedUp()
    {
        //GameManager.instance.PlayClip(ChairPickedUpAudio);
    }
}
