using BonusLevel;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BonusResult : MonoBehaviour
{
    [SerializeField] private GameObject[] disable;
    [SerializeField] private Text uiText;
    [SerializeField] private InventoryCollection inventory;
    public int inventoryCount;

    private void Start()
    {
        if (inventory == null)
        {
            inventory = FindObjectOfType<InventoryCollection>();
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            foreach (GameObject item in disable)
            {
                item.SetActive(false);
            }
        }

        uiText.text = inventory.stars.Count.ToString();
    }
}
