using System;
using UnityEngine;

namespace MainLevel
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class IcePlatform : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private float meltSpeed;

        private void Start()
        {
            if (!sr) sr = GetComponent<SpriteRenderer>();
        }

        public void Melt()
        {
            Color colour = sr.material.color;
            float fade = colour.a - (meltSpeed * Time.deltaTime);
            
            colour = new Color(colour.r, colour.g, colour.b, fade);
            sr.material.color = colour;
            
            if (colour.a <= 0)
                gameObject.SetActive(false);
        }
    }
}
