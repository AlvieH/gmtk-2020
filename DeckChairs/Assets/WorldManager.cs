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
        var range = 3;
        newPosition.x += Random.Range(-range, range);
        //newPosition.y += Random.Range(-range, range);
        //newPosition.z += Random.Range(-range, range);
        instance.transform.position = newPosition;
    }
}
