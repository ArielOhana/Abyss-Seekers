using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] private GameObject _selectedHeroObject, _tileObject, _tileUnitObject;
    private int displayHealth = 255;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        displayHealth = UnitManager.Instance.hits * 85;
    }

    /*public void ShowTileInfo(Tile tile) {

        if (tile == null) {
            _tileObject.SetActive(false);
            _tileUnitObject.SetActive(false);
            return;
        }

        _tileObject.GetComponentInChildren<Text>().text = tile.TileName;
        _tileObject.SetActive(true);

        if (tile.OccupiedUnit) {
            _tileUnitObject.GetComponentInChildren<Text>().text = tile.OccupiedUnit.UnitName;
            _tileUnitObject.SetActive(true);
        }
    }*/

    public void ShowSelectedHero(BaseHero hero) {
        var x = UnitManager.Instance.hits;
        /*if(hero == null) {
            _selectedHeroObject.SetActive(false);
            return;
        }*/
        _selectedHeroObject.GetComponentInChildren<Text>().text = displayHealth.ToString();
        _selectedHeroObject.SetActive(true);

    }
}
