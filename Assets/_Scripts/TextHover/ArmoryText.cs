using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmoryText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject armoryText;

    void Start()
    {
        armoryText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        armoryText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        armoryText.SetActive(false);
    }
}
