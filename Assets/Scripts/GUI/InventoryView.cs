using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private Transform slotsParent = null;
    [SerializeField] private InventorySlot slotPrefab = null;

    private List<InventorySlot> slots = new List<InventorySlot>();

    public void CreateInventorySlots()
    {
        foreach (var weapon in Inventory.instance.Weapons)
        {
            var slot = Instantiate(slotPrefab, slotsParent);
            slot.Initialize(weapon.id, SpriteHandler.instance.WeaponIcons[weapon.id]);

            slots.Add(slot);
        }
    }

    public void ChangeActiveWeapon(int id)
    {
        foreach (var slot in slots)
        {
            if (slot.id == id)
                slot.SetChosen();
            else
                slot.SetDefault();
        }
    }
}
