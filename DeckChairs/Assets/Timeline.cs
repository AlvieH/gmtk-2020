using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    public GameObject Ship;
    public GameObject TimelineEvents;
    public GameObject TimelineEvent;
    public Chapter[] Chapters;

    float timelineWidth;
    float shipWidth;
    float progress => WorldManager.instance.ElapsedSeconds / WorldManager.instance.TotalSeconds;
    float initialShipX;

    // Start is called before the first frame update
    void Start()
    {
        timelineWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
        shipWidth = Ship.GetComponent<RectTransform>().sizeDelta.x;
        initialShipX = Ship.transform.position.x;
    }

    //[ContextMenu("Create timeline events")]
    void CreateTimelineEvents()
    {
        // Remove any timeline events
        int children = TimelineEvents.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            DestroyImmediate(TimelineEvents.transform.GetChild(i).gameObject);
        }

        // Create new timeline events
        foreach (var chapter in Chapters)
        {
            var instance = Instantiate(TimelineEvent, TimelineEvents.transform);
            instance.name = $"Timeline Event ({chapter.name})";
            var eventPosition = instance.transform.position;
            var eventDistanceFraction = chapter.StartTime / 120;
            // Need to set manually here since it won't be available during edit time
            timelineWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
            eventPosition.x = Mathf.Lerp(0, timelineWidth, eventDistanceFraction);
            instance.transform.position = eventPosition;
            instance.GetComponent<Image>().sprite = chapter.Icon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var shipPosition = Ship.transform.position;
        // XXX: hacky
        var distance = timelineWidth - initialShipX + shipWidth / 1.5f;
        shipPosition.x = Mathf.Lerp(initialShipX, distance, progress);
        Ship.transform.position = shipPosition;
    }
}
