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

    // Start is called before the first frame update
    void Start()
    {
        timelineWidth = transform.GetComponent<RectTransform>().sizeDelta.x;
        shipWidth = Ship.GetComponent<RectTransform>().sizeDelta.x;
    }

    [ContextMenu("Create timeline events")]
    void CreateTimelineEvents()
    {
        // Remove any timeline events
        int children = TimelineEvents.transform.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            DestroyImmediate(TimelineEvents.transform.GetChild(i).gameObject);
        }
        foreach (var chapter in Chapters)
        {
            var instance = Instantiate(TimelineEvent, TimelineEvents.transform);
            instance.GetComponent<Image>().sprite = chapter.Icon;
        }
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
