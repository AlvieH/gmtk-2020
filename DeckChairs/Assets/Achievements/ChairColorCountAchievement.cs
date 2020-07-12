using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Achievements/Chair Color Count")]
public class ChairColorCountAchievement: Achievement
{
    [Min(0)]
    public int colorCount;

    public override bool IsCompleted()
    {
        return AchievementManager.instance.ChairColorsInUse.Count >= colorCount;
    }
}
