using System;
using UnityEngine;
using UnityEngine.UI;

public class Role : MonoBehaviour
{
    public Animator LetterAnimator;
    public Text RoleName;
    public Text FinalRoleName;
    public string[] RoleNames;
    [Min(1)]
    public int AchievementsPerPromotion;
    public AudioClip PromotionSound;

    int AchievementCount => AchievementManager.instance.EarnedAchievements.Count;

    public string LatestRole
    {
        get
        {
            int index = Math.Min(RoleNames.Length - 1, AchievementCount / AchievementsPerPromotion);
            var roleName = RoleNames[index];
            return roleName;
        }
    }

    public string ShownRole
    {
        get => RoleName.text;
        set
        {
            RoleName.text = value;
            FinalRoleName.text = value;
        }
    }

    private void Start()
    {
        ShownRole = LatestRole;
        AchievementManager.instance.OnAchievementEarned += (Achievement achievement) =>
        {
            if (LatestRole != ShownRole)
            {
                Promote();
            }
        };

    }

    private void Update()
    {
        // Wait until the letter entrance animation has completed 
        // updating the ShownRole
        var animatorState = LetterAnimator.GetCurrentAnimatorStateInfo(0);
        if (animatorState.IsName("Promotion Exit"))
        {
            ShownRole = LatestRole;
        }
    }

    public void Promote()
    {
        LetterAnimator.SetTrigger("Enter");
        GameManager.instance.PlayClip(PromotionSound);
    }
}
