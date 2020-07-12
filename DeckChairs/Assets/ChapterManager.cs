using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    public Text AnnouncementText;
    public Animator Animator;
    public Chapter[] Chapters;
    public HashSet<Chapter> PlayedChapters = new HashSet<Chapter>();

    float CurrentTime => WorldManager.instance.ElapsedSeconds;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayedChapters.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var chapter in Chapters)
        {
            if (PlayedChapters.Contains(chapter)) continue;
            if (CurrentTime >= chapter.StartTime)
            {
                StartCoroutine(PlayChapter(chapter));
                PlayedChapters.Add(chapter);
            }
        }
        //if (WorldManager.instance.ElapsedSeconds)
    }

    IEnumerator PlayChapter(Chapter chapter)
    {
        // Fade music and pause timeline
        GameManager.instance.FadeOutMusic();
        Debug.Log($"[ChapterManager] Playing chapter {chapter.name}");
        WorldManager.instance.IsPaused = true;

        // Play chapter event sound and shake
        GameManager.instance.PlayClip(chapter.EventSound);
        if (chapter.PreAnnouncementShakeIntensity != 0)
        {
            WorldManager.instance.Shake(chapter.EventSound.length, chapter.PreAnnouncementShakeIntensity);
        }
        yield return new WaitForSeconds(chapter.EventSound.length - 0.5f);

        // Show announcement
        Animator.SetBool("Announcing", true);
        AnnouncementText.text = chapter.AnnouncementText;
        GameManager.instance.PlayClip(chapter.AnnouncementVoiceover);

        // Wait for announcement to finish
        yield return new WaitForSeconds(chapter.AnnouncementVoiceover.length);

        // Play post-announcement sound if needed
        if (chapter.PostAnnouncementSound != null)
        {
            GameManager.instance.PlayClip(chapter.PostAnnouncementSound);
        }
        if (chapter.PostAnnouncementShakeIntensity != 0)
        {
            var remainingTime = WorldManager.instance.TotalSeconds - CurrentTime;
            WorldManager.instance.Shake(remainingTime, chapter.PostAnnouncementShakeIntensity);
        }
        WorldManager.instance.IsPaused = false;
        GameManager.instance.FadeInMusic();
        Animator.SetBool("Announcing", false);
    }
}
