using UnityEngine;

public abstract class Item : ScriptableObject
{
    public abstract void Register(PlayerStats playerStat);
    public abstract void Unregister(PlayerStats playerStat);
}
