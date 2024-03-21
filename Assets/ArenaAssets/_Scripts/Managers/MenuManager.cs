using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject;
    public int displayHealth;

    private void Awake() {
        Instance = this;
    }

    public void ShowSelectedHero() {
        _selectedHeroObject.GetComponentInChildren<Text>().text = displayHealth.ToString();
        _selectedHeroObject.SetActive(true);

    }
}
