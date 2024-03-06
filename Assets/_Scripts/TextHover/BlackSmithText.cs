using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlackSmithText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject blackSmithText;

    void Start()
    {
        blackSmithText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        blackSmithText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        blackSmithText.SetActive(false);
    }
}
