using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Achievements/Chair Move")]
public class ChairMoveAchievement: Achievement
{
    [Min(0)]
    public int chairCount;
    public override bool IsCompleted()
    {
        return chairCount >= AchievementManager.instance.ChairsMovedCount;
    }

    //public override string Description => {}
}
