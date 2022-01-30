using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MouseHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioSource buttonHover;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonHover.Play();
    }
}
