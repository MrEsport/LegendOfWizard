using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour, IInventory<Item>
{
    [Header("Stats")]
    [SerializeField] private PlayerStats playerStats;

    public UnityEvent<Item> OnItemAdded = new();

    private List<Item> items = new();

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.KeypadMinus)) return;
        if (items.Count == 0) return;

        int index = Random.Range(0, items.Count);
        items[index].Take();
    }

    private void OnDestroy()
    {
        items.ForEach(i => i.Unregister(playerStats));
        items.Clear();
    }

    public void Add(Item toAdd)
    {
        var itemInstance = Instantiate(toAdd);
        itemInstance.Register(playerStats);
        items.Add(itemInstance);

        itemInstance.OnTaken += TakeStock;

        OnItemAdded.Invoke(itemInstance);
    }

    public void Take(Item toTake)
    {
        if (!items.Contains(toTake)) return;

        toTake.OnTaken -= TakeStock;

        toTake.Unregister(playerStats);
        items.Remove(toTake);
    }

    private void TakeStock(IStockable stockToTake) => Take(stockToTake as Item);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<ItemPickup>(out var pickup)) return;

        Add(pickup.Item);
    }
}
