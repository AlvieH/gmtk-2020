using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timeline : MonoBehaviour
{
    public GameObject Ship;

    float timelineWidth;
    float shipWidth;
    float progress => WorldManager.instance.ElapsedSeconds / WorldManager.instance.TotalSeconds;

    // Start is called before the first frame update
    void Start()
    {
        timelineWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
        shipWidth = Ship.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        var shipPosition = Ship.transform.position;
        var distance = timelineWidth - shipWidth;
        shipPosition.x = Mathf.Lerp(0, distance, progress);
        Ship.transform.position = shipPosition;
    }
}
