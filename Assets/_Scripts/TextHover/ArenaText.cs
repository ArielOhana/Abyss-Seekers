using UnityEngine;
using UnityEngine.EventSystems;

public class ArenaText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject arenaText;

    void Start()
    {
        arenaText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        arenaText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        arenaText.SetActive(false);
    }
}
