using UnityEngine;

public abstract class Item : StockableObject
{
    public abstract void Register(PlayerStats playerStat);
    public abstract void Unregister(PlayerStats playerStat);
}
