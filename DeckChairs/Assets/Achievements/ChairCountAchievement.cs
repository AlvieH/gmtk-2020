using UnityEngine;

[CreateAssetMenu(menuName = "Achievements/Chair Count")]
public class ChairCountAchievement : Achievement
{
    [Min(0)]
    public int chairCount;
    public override bool IsCompleted()
    {
        return GameObject.FindGameObjectsWithTag("Chair").Length >= chairCount;
    }

    //[HideInInspector]
    //public new string description => $"Build {chairCount} chairs";
}
