using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Chair paint")]
public class ChairPaintAchievement : Achievement
{
    [Min(0)]
    public int chairCount;

    public override bool IsCompleted()
    {
        return AchievementManager.instance.ChairsPaintedCount >= chairCount;
    }
}
