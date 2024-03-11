using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject saveText;

    void Start()
    {
        saveText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        saveText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        saveText.SetActive(false);
    }
}
