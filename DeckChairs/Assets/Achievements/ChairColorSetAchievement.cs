using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Achievements/Chair Color Set")]
public class ChairColorSetAchievement: Achievement
{
    [Min(0)]
    public Color[] colors;

    public override bool IsCompleted()
    {
        var colorsSet = new HashSet<Color>(colors);
        return colorsSet.IsSubsetOf(AchievementManager.instance.ChairColorsInUse);
    }
}
