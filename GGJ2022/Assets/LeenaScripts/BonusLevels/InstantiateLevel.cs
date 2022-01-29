using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLevel : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    private void Awake()
    {
        // This would need bit more set up
        //Instantiate(levels[Random.Range(0, levels.Length)], Vector2.zero, Quaternion.identity);

       // levels[Random.Range(0, levels.Length)].SetActive(true);
    }
}
