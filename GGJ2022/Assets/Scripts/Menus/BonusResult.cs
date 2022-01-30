using BonusLevel;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BonusResult : MonoBehaviour
{
    [Header("Items to Disaple end of game")]
    [SerializeField] private GameObject[] disable;

    [Header("Final Score Display")]
    [Tooltip("Total of Collected stars")]
    [SerializeField] private Text collectedStarsText;
    [SerializeField] private InventoryCollection player;
    private int inventoryCount;

    private void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<InventoryCollection>();
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

        collectedStarsText.text = player.collectedStars.Count.ToString();
    }
}
