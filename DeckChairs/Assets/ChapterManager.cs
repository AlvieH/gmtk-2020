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
        foreach(var chapter in Chapters)
        {
            if (PlayedChapters.Contains(chapter)) continue;
            if (CurrentTime >= chapter.StartTime )
            {
                StartCoroutine(PlayChapter(chapter));
                PlayedChapters.Add(chapter);
            }
        }
        //if (WorldManager.instance.ElapsedSeconds)
    }

    IEnumerator PlayChapter(Chapter chapter)
    {
        GameManager.instance.FadeOutMusic();
        WorldManager.instance.IsPaused = true;
        Debug.Log($"[ChapterManager] Playing chapter {chapter.name}");
        Animator.SetBool("Announcing", true);
        AnnouncementText.text = chapter.AnnouncementText;
        GameManager.instance.PlayClip(chapter.AnnouncementVoiceover);
        yield return new WaitForSeconds(chapter.AnnouncementVoiceover.length);
        WorldManager.instance.IsPaused = false;
        GameManager.instance.FadeInMusic();
        Animator.SetBool("Announcing", false);
    }
}
