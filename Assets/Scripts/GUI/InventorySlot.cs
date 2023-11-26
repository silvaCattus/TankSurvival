using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image icon = null;
    [SerializeField] private Color chosenColor;
    [SerializeField] private Color normalColor;

    public int id { get; private set; }

    public void Initialize(int id, Sprite sprite)
    {
        this.id = id;
        icon.sprite = sprite;
    }

    public void SetChosen()
    {
        icon.color = chosenColor;
    }

    public void SetDefault()
    {
        icon.color = normalColor;
    }
}
