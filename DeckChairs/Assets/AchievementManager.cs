using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance = null;
    public AchievementCard AchievementCard;
    public AchievementSet AllAchievements;
    public AudioClip AchievementSound;
    public Action<Achievement> OnAchievementEarned;
    public List<Achievement> EarnedAchievements;

    public HashSet<Color> ChairColorsInUse = new HashSet<Color>();

    public int ChairsMovedCount;
    public int ChairsRotatedCount;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void OnEnable()
    {
        EarnedAchievements = new List<Achievement>();
        ChairColorsInUse.Clear();
        ChairsMovedCount = 0;
        ChairsRotatedCount = 0;
    }

    private void Update()
    {
        // Add all active chair colors to cached chair colors
        var chairColorsAll = FindObjectsOfType<Paintable>(); ;
        foreach (var c in chairColorsAll)
        {
            if (c.HasBeenPainted)
            {
                ChairColorsInUse.Add(c.ChairColor);
            }
        }
        //Debug.Log(FindObjectsOfType<Paintable>().Select(x => x.ChairColor).ToArray());
        //ChairColorsInUse.UnionWith();

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
