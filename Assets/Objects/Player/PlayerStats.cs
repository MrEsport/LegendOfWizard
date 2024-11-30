using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    [Header("Movement")]
    public Stat<float> RunSpeed;
    public Stat<float> SprintDelay;
    public Stat<float> SprintSpeedMultiplier;
}
