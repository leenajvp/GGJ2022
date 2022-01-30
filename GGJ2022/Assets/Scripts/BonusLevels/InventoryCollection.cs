using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BonusLevel
{
    public class InventoryCollection : MonoBehaviour
    {
        [Header("Text to Update on Collection")]
        [SerializeField] private Text uiText;
        [SerializeField] AudioSource collectSound;
        public List<Collectables> collectedStars = new List<Collectables>();

        private void Update()
        {
            uiText.text = collectedStars.Count.ToString();
            Collect();
        }

        private void Collect()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);

            foreach (var col in colliders)
            {
                Collectables star = col.gameObject.GetComponent<Collectables>();

                if (star != null)
                {
                    collectSound.Play();
                    collectedStars.Add(star);
                    col.gameObject.SetActive(false);
                }
            }
        }
    }
}
