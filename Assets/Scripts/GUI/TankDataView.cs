using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TankDataView : MonoBehaviour
{
    [SerializeField] private TMP_Text tankHpText = null;
    [SerializeField] private TankBody tank = null;

    private void Start()
    {
        tank.hpChanged.AddListener(PrintHP);
    }

    private void PrintHP(float hp)
    {
        tankHpText.text = hp.ToString();
    }
}
