using UnityEngine;

[CreateAssetMenu(fileName = "SpeedItem", menuName = "Scriptable Objects/Items/SpeedItem")]
public class SpeedItem : Item
{
    [SerializeField] float bonusValue;

    public override void Register(PlayerStats playerStat)
    {
        playerStat.RunSpeed.AddModifier(ModifySpeed);
    }

    public override void Unregister(PlayerStats playerStat)
    {
        playerStat.RunSpeed.RemoveModifier(ModifySpeed);
    }

    private float ModifySpeed(float baseValue)
    {
        return baseValue + bonusValue;
    }
}
