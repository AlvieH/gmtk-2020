using UnityEngine;

[CreateAssetMenu]
public class Chapter : ScriptableObject
{
    [Min(0)]
    public float StartTime;
    [TextArea]
    public string AnnouncementText;
    public AudioClip AnnouncementVoiceover;
    public Sprite Icon;
}
