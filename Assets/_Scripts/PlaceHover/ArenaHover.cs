using UnityEngine;
using UnityEngine.EventSystems;

public class ArenaHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject arenaHover;

    void Start()
    {
        arenaHover.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        arenaHover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        arenaHover.SetActive(false);
    }
}
