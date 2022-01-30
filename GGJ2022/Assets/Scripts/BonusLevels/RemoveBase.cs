using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBase : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
