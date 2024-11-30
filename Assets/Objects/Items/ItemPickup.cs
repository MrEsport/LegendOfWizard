using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;

    public Item Item { get => item; }
}
