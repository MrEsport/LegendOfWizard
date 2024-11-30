using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public Stat<float> MoveSpeed;
}
