using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Chair Count")]
public class ChairCountAchievement : Achievement
{
    [Min(0)]
    public int chairCount;
    public override bool IsCompleted()
    {
        return GameObject.FindGameObjectsWithTag("chair").Length >= chairCount;
    }

    //public override string Description => {}
}
