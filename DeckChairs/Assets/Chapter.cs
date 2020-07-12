using UnityEngine;

[CreateAssetMenu]
public class Chapter : ScriptableObject
{
    [Min(0)]
    public float StartTime;
    [TextArea]
    public string AnnouncementText;
    public AudioClip AnnouncementVoiceover;
    public AudioClip EventSound;
    public AudioClip PostAnnouncementSound;
    [Min(0)]
    public float PreAnnouncementShakeIntensity;
    [Min(0)]
    public float PostAnnouncementShakeIntensity;
    public Sprite Icon;
}
