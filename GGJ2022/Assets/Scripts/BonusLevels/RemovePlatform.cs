using BonusLevel;
using System.Collections;
using UnityEngine;

public class RemovePlatform : MonoBehaviour
{
    [SerializeField] protected SwapCharacter player;
    [Header("Check if Platform is Hot")]
    [SerializeField] protected bool hot;

    private void Start()
    {
        if (player == null)
        {
            FindObjectOfType<SwapCharacter>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckPlayerMode();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
    }

    private void CheckPlayerMode()
    {
        //If platform is hot and player is cube stop movement
        if (hot)
        {
            if (player.isBall)
                StartCoroutine(Timer());

            else
            {
                player.disabled = true;
                //melt cube
            }
        }

        //If platform is cold and player is ball stop movement and melt platform
        else
        {
            if (!player.isBall)
                StartCoroutine(Timer());

            else
            {
                player.disabled = true;
                gameObject.SetActive(false);
            }
        }
    }
}
