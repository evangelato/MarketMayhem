using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _coinInventory;
    [SerializeField]
    private Text _ammoText;
    public void UpdateAmmo(int count)
    {
        _ammoText.text = "Ammo: " + count.ToString();
    }

    public void UpdateInventory(bool hasCoin)
    {
        _coinInventory.SetActive(hasCoin);
    }
}
