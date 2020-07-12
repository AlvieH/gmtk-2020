using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Chair Rotate")]
public class ChairRotateAchievement : Achievement
{
    [Min(0)]
    public int chairCount;
    public override bool IsCompleted()
    {
        return AchievementManager.instance.ChairsRotatedCount >= chairCount;
    }

    //public override string Description => {}
}
