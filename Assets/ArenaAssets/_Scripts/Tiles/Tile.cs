using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ArrowTranslator;

public abstract class Tile : MonoBehaviour
{
    public BaseUnit baseUnit;


    public int G;
    public int H;

    public int F { get { return G + H; } }

    public Tile previous;
    public Vector2 gridLocation;
    public List<Sprite> Arrows;
    private List<Tile> attackTiles = new List<Tile>();


    public string TileName;
    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] private GameObject _inRangeHighight;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _attackableUnit;
    [SerializeField] public bool _isWalkable;
    
    

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;

    public virtual void Init(int x, int y)
    {
        
    }

    void OnMouseEnter()
    {
       //MenuManager.Instance.ShowTileInfo(this);
        
        if (UnitManager.Instance.SelectedHero != null && UnitManager.Instance.inRangeTiles.Contains(this) && !UnitManager.Instance.isMoving) {
            UnitManager.Instance.ShowPath(this);
        }

    }

    void OnMouseExit()
    {
        //MenuManager.Instance.ShowTileInfo(null);
        if (UnitManager.Instance.SelectedHero != null && !UnitManager.Instance.isMoving) {
            UnitManager.Instance.HidePath();
        }
    }

    private void OnMouseDown() {
        if (GameManager.Instance.GameState != GameState.HeroesTurn || UnitManager.Instance.isMoving) { return; }

        if (OccupiedUnit != null) { //if there is a unit on this tile
            if (OccupiedUnit.Faction == Faction.Hero) { //if the unit on this tile is hero
                UnitManager.Instance.SetSelectedHero((BaseHero)OccupiedUnit); //select the hero
                UnitManager.Instance.GetInRangeTiles();
                UnitManager.Instance.CheckAttackRange(UnitManager.Instance.SelectedHero.OccupiedTile, this, UnitManager.Instance.SelectedHero._movementSpeed, UnitManager.Instance.SelectedHero._attackRange);

            } else {
                if (UnitManager.Instance.SelectedHero != null) {//checking if we already have a selected unit
                    if (UnitManager.Instance.inAttackRangeTiles.Contains(this)) {
                        StartCoroutine(MoveAttack());
                    }
                }
            }
        } else { // there is no unit on selected tile
            if (UnitManager.Instance.SelectedHero != null) { // we have selected unit
                if (UnitManager.Instance.inRangeTiles.Contains(this)) {
                    UnitManager.Instance.HidePath();
                    StartCoroutine(Move(UnitManager.Instance.SelectedHero.OccupiedTile, this, UnitManager.Instance.SelectedHero, false));
                    //UnitManager.Instance.MoveAlongPath(UnitManager.Instance.SelectedHero.OccupiedTile, this, UnitManager.Instance.SelectedHero, false);


                    
                }

            }
        }
    }

    private void FinishHeroTurn() {
        UnitManager.Instance.SetSelectedHero(null); // deselect unit
        UnitManager.Instance.GetInRangeTiles();
        HideAttackableUnit();
        GameManager.Instance.ChangeState(GameState.EnemiesTurn);
    }
    IEnumerator MoveAttack() {
        UnitManager.Instance.MoveAlongPath(UnitManager.Instance.SelectedHero.OccupiedTile, this, UnitManager.Instance.SelectedHero, true);
        yield return new WaitUntil(() => !UnitManager.Instance.isMoving);

        var enemy = (BaseEnemy)OccupiedUnit; // if clicked on OccupiedUnit and we have selected a hero, attack
        UnitManager.Instance._enemies.Remove(enemy);

        Destroy(enemy.gameObject); // enemy.takeDamage
                                   //UnitManager.Instance.SelectedHero(enemy)
        if(UnitManager.Instance._enemies.Count == 0) {
            SceneManager.LoadScene("AftrerWin");
        }
        FinishHeroTurn();
    }
    IEnumerator Move(Tile start, Tile end, BaseUnit unit, bool toAttackNow) {
        UnitManager.Instance.MoveAlongPath(start, end, unit, toAttackNow);
        yield return new WaitUntil(() => !UnitManager.Instance.isMoving);
        FinishHeroTurn();
    }


        public void SetUnit(BaseUnit unit)
    {
        if(unit.OccupiedTile != null)//goin to the units occupied tile, and setting its occupied to null
        {
            unit.OccupiedTile.OccupiedUnit = null;
        }
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
        if(unit.Faction == Faction.Ememy && !UnitManager.Instance._enemies.Contains((BaseEnemy)unit)) {
            UnitManager.Instance._enemies.Add((BaseEnemy)unit);
        }
        if (unit.Faction == Faction.Hero && UnitManager.Instance.TheHero == null) {
            UnitManager.Instance.TheHero = (BaseHero)unit;
        }
    }

    public void ShowInRangeTiles() {
        _inRangeHighight.SetActive(true);
    }
    public void HideInRangeTiles() {
        _inRangeHighight.SetActive(false);
    }

    public void ShowAttackableUnit() {
        _attackableUnit.SetActive(true);
    }

    public void HideAttackableUnit() {
        foreach (var item in UnitManager.Instance.inAttackRangeTiles) {
            item._attackableUnit.SetActive(false);
        }
        
    }
    public void SetArrowSprite(ArrowDirection d) {
        var arrow = GetComponentsInChildren<SpriteRenderer>()[2];
        if(d == ArrowDirection.None) {
            arrow.color = new Color(1, 1, 1, 0);
        } else {
            arrow.color = new Color(1, 1, 1, 1);
            arrow.sprite = Arrows[(int)d];
            arrow.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }
    }
}
