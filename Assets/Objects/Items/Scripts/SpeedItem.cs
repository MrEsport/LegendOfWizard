using UnityEngine;

[CreateAssetMenu(fileName = "SpeedItem", menuName = "Scriptable Objects/Items/SpeedItem")]
public class SpeedItem : Item
{
    [SerializeField] float bonusValue;

    public override void Register(PlayerStats playerStat)
    {
        playerStat.MoveSpeed.AddModifier(ModifySpeed);
    }

    public override void Unregister(PlayerStats playerStat)
    {
        playerStat.MoveSpeed.RemoveModifier(ModifySpeed);
    }

    private float ModifySpeed(float baseValue)
    {
        return baseValue + bonusValue;
    }
}
