using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject stockUIPrefab;
    [SerializeField] private RectTransform stockTransform;

    private List<StockableUI> stock = new();

    private void Start()
    {
        InputManager.Instance.OnInventory.OnInput += ToggleMenuOpen;

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (InputManager.Instance == null) return;
        InputManager.Instance.OnInventory.OnInput -= ToggleMenuOpen;
    }

    public void AddStock(IStockable toAdd)
    {
        var instance = Instantiate(stockUIPrefab, stockTransform).GetComponent<StockableUI>();
        instance.SetStockUI(toAdd);
        stock.Add(instance);

        toAdd.OnTaken += TakeStock;
    }

    public void TakeStock(IStockable toTake)
    {
        var found = stock.FirstOrDefault(ui => ui.Stockable == toTake);
        if (found == null) return;

        toTake.OnTaken -= TakeStock;

        stock.Remove(found);
        Destroy(found.gameObject);
    }

    private void ToggleMenuOpen()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
