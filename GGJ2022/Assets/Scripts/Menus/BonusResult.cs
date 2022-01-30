using BonusLevel;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BonusResult : MonoBehaviour
{
    [Header("Items to Disaple end of game")]
    [SerializeField] private GameObject[] disable;

    [Header("List of Stars in Level")]
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Text numberOfStarsText;

    [Header("Final Score Display")]
    [Tooltip("Total of Collected stars")]
    [SerializeField] private Text collectedStarsText;
    [SerializeField] private InventoryCollection player;
    private int inventoryCount;

    private void Start()
    {
        numberOfStarsText.text = ("/") + stars.Length.ToString();

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

        collectedStarsText.text = player.stars.Count.ToString();
    }
}
