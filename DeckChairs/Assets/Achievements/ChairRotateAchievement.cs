using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Chair Rotate")]
public class ChairRotateAchievement : Achievement
{
    [Min(0)]
    public int chairCount;
    public override bool IsCompleted()
    {
        return chairCount >= AchievementManager.instance.ChairsRotatedCount;
    }

    //public override string Description => {}
}
