using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SprintItem", menuName = "Scriptable Objects/Items/SprintItem")]
public class SprintItem : Item
{
    [SerializeField, Range(0, 1f)] private float sprintDelayReduction = .5f;

    public override void Register(PlayerStats playerStat)
    {
        playerStat.SprintDelay.AddModifier(ModifySprintDelay);
    }

    public override void Unregister(PlayerStats playerStat)
    {
        playerStat.SprintDelay.RemoveModifier(ModifySprintDelay);
    }

    private float ModifySprintDelay(float baseValue)
    {
        return baseValue * (1f - sprintDelayReduction);
    }
}
