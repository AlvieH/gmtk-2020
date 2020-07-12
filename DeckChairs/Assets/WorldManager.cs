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

    //[SerializeField]
    //InteractionMode interactionMode;
    public InteractionMode InteractionMode;
    //{
    //    get => interactionMode;
    //    set
    //    {
    //        interactionMode = value;
    //        UpdateCursor();
    //    }
    //}

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
        //Debug.Log("[WorldManager] destroying chairs!");
        GameObject.FindGameObjectsWithTag("Chair").ToList().ForEach(c => Destroy(c.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
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
