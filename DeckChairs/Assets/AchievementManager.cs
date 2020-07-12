using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance = null;
    public AchievementCard AchievementCard;
    public AchievementSet AllAchievements;
    public AudioClip AchievementSound;
    public Action<Achievement> OnAchievementEarned;
    public List<Achievement> EarnedAchievements;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void OnEnable()
    {
        EarnedAchievements = new List<Achievement>();
    }

    private void Update()
    {
        // Check if new achivements have been earned and grant them
        foreach (var achievement in AllAchievements.Items)
        {
            // Skip already-earned acheivements 
            if (EarnedAchievements.Contains(achievement)) continue;
            if (achievement.IsCompleted())
            {
                EarnedAchievements.Add(achievement);
                CelebrateAchievement(achievement);
                OnAchievementEarned?.Invoke(achievement);
            }
        }
    }

    void CelebrateAchievement(Achievement achievement)
    {
        Debug.Log($"[AchievementManager] Celebrating achievement {achievement.title}");
        //int cardsOnScreen = GameObject.FindObjectsOfType<AchievementCard>().Length;
        var instance = Instantiate(AchievementCard, transform);
        GameManager.instance.PlayClip(AchievementSound);
        //instance.transform.SetParent();
        //var position = instance.transform.position;
        //position.y = cardsOnScreen * instance.GetComponent<RectTransform>().sizeDelta.y;
        //instance.transform.position = position;
        instance.GetComponent<AchievementCard>().UseForAchivement(achievement);
    }

}
