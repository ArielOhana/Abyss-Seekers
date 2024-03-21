using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ArrowTranslator;
using static UnityEngine.UI.CanvasScaler;
using Assets;
using System;
using Context;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance;

    private List<ScriptbleUnit> _units;
    public List<BaseEnemy> _enemies;
    public BaseHero TheHero;

    public BaseHero SelectedHero;

    [SerializeField] private float _unityMoveSpeed;
    private PathFinder pathFinder;
    private RangeFinder rangeFinder;
    private ArrowTranslator arrowTranslator;
    private List<Tile> path = new List<Tile>();
    private List<Tile> pathToShow = new List<Tile>();
    private BaseUnit unitToMove;
    private bool toAttack = false;
    private bool isFirst = true;
    private int calc = 0;
    public bool isMoving = false;
    public bool hasReachedTarget = true;
    public List<Tile> inRangeTiles = new List<Tile>();
    public List<Tile> inAttackRangeTiles = new List<Tile>();
    
    

    private void Awake()
    {
        Instance = this;

        _units = Resources.LoadAll<ScriptbleUnit>("units").ToList();
    }
    private void Start() {
        arrowTranslator = new ArrowTranslator();
        pathFinder = new PathFinder();
        rangeFinder = new RangeFinder();
    }
    private void Update() {
        if (isMoving) {
            MoveUnit();
        }
    }

    public void GetInRangeTiles() {
        if(SelectedHero == null) {
            foreach (var item in inRangeTiles) {
                item.HideInRangeTiles();
            }
        } else {
            inRangeTiles = rangeFinder.GetTilesInRange(SelectedHero.OccupiedTile, SelectedHero._movementSpeed);
            foreach (var item in inRangeTiles) {
                item.ShowInRangeTiles();
            }
        }
    }

    public void CheckAttackRange(Tile start, Tile end, int movementSpeed, int attackRange) {
        inAttackRangeTiles = rangeFinder.GetTilesInAttackRange(start, movementSpeed, attackRange);
        foreach (var item in inAttackRangeTiles) {
            if (item.OccupiedUnit != null) {
                if (item.OccupiedUnit.Faction == Faction.Ememy) {
                    item.ShowAttackableUnit();
                }
            }
        }
    }


    private void MoveUnit() {
        if (isFirst) {
            calc = 0;
            if (toAttack) { 
                calc = unitToMove._attackRange;
            } else {
                if(path.Count > unitToMove._movementSpeed)
                calc = path.Count - unitToMove._movementSpeed;
            }
            isFirst = false;
        }
        if (path.Count > calc) {
            // Calculate the distance between the unit and the next point in the path
            float distance = Vector2.Distance(unitToMove.transform.position, path[0].transform.position);

            // If the distance is smaller than a certain threshold, consider the unit as reached the target
            if (distance < 0.001f) {
                path[0].SetUnit(unitToMove);
                path.RemoveAt(0);
                if (path.Count == calc) {
                    resetMovement();
                    return;
                }
            }
            if (path[0].OccupiedUnit != null)
            {
                resetMovement();
                hasReachedTarget = false; 
                return;
            }
            // Move the unit towards the next point in the path
            unitToMove.transform.position = Vector2.MoveTowards(unitToMove.transform.position,
                path[0].transform.position,
                _unityMoveSpeed * Time.deltaTime
                );
        } else {
            resetMovement();
        }
    }

    private void resetMovement()
    {
        isFirst = true;
        toAttack = false;
        calc = 0;
        isMoving = false;
    }

    public void MoveAlongPath(Tile start, Tile end, BaseUnit unit, bool toAttackNow) {
        path = pathFinder.FindPath(start, end, toAttackNow);
        unitToMove = unit;
        toAttack = toAttackNow;
        isMoving = true;
        calc = 0;
    }


    public void EnemiesTurn() {
        StartCoroutine(SendAllEnemies());

        GameManager.Instance.GameState = GameState.HeroesTurn;
    }

    IEnumerator SendAllEnemies() {
        foreach (var enemy in _enemies){
            inAttackRangeTiles = rangeFinder.GetTilesInAttackRange(enemy.OccupiedTile, enemy._movementSpeed, enemy._attackRange);
            if (inAttackRangeTiles.Contains(TheHero.OccupiedTile)) {
                hasReachedTarget = true;
                toAttack = true;
                unitToMove = enemy;
                path = pathFinder.FindPath(unitToMove.OccupiedTile, TheHero.OccupiedTile, toAttack);
                calc = 0;
                isMoving = true;
                yield return new WaitUntil(() => !isMoving);
                if (hasReachedTarget){
                    StartCoroutine(MoveAttack(TheHero));
                }
            } else {
                unitToMove = enemy;
                path = pathFinder.FindPath(unitToMove.OccupiedTile, TheHero.OccupiedTile, true);
                toAttack = false;
                calc = 0;
                isMoving = true;
                yield return new WaitUntil(() => !isMoving);
            }
        }

    }


    IEnumerator MoveAttack(BaseUnit unitToAttack) {
        if(unitToAttack.Faction == Faction.Hero) {
            var DmgTaken = AttackAction(unitToMove.stats, unitToAttack.stats);
            GameObject points = Instantiate(unitToMove.floatingPoints, unitToAttack.transform.position, Quaternion.identity);
            points.transform.GetChild(0).GetComponent<TextMesh>().text = DmgTaken > 0 ? DmgTaken.ToString() : "BLOCKED!";
            if (unitToAttack._currentHealth < unitToAttack._maxHealth)
            {
                unitToAttack._currentHealth -= AttackAction(unitToMove.stats, unitToAttack.stats);

            } else
            {
                unitToAttack._currentHealth = unitToAttack._maxHealth - DmgTaken;
            }
            MenuManager.Instance.displayHealth = unitToAttack._currentHealth;
            MenuManager.Instance.ShowSelectedHero();
            if (unitToAttack._currentHealth < 1) {
                SceneManager.LoadScene("AfterLose");
                yield return new WaitUntil(() => !isMoving);

            }
        }


    }

    public void SpawnHeros()
    {
        var randomPrefab = getRandomUnit<BaseHero>(Faction.Hero);
        var spawnedHero = Instantiate(randomPrefab);
        spawnedHero._maxHealth = Context.MainMenu.currentHero.Stats.MaxHealth;
        spawnedHero._currentHealth = spawnedHero._maxHealth;
        spawnedHero._movementSpeed = Context.MainMenu.currentHero.Stats.MovementSpeed;
        spawnedHero._attackRange = Context.MainMenu.currentHero.Inventory.CurrentWeapon.Range;
        spawnedHero.stats = Context.MainMenu.currentHero.Stats;
        TheHero = spawnedHero;
        MenuManager.Instance.displayHealth = spawnedHero._maxHealth;
        MenuManager.Instance.ShowSelectedHero();
        var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();
        randomSpawnTile.SetUnit(spawnedHero);
        
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemiesToSpawn = Context.MainMenu.currentHero.SpawnEnemies();
        for (int i = 0; i < enemiesToSpawn.Count(); i++)
        {

            var randomPrefab = getRandomUnit<BaseEnemy>(Faction.Ememy);
            var spawnedEnemy = Instantiate(randomPrefab);
            spawnedEnemy.stats = new Stats(0,0,0,0,0,0,0,0,0,0);
            spawnedEnemy.stats.Dmg = enemiesToSpawn[i].Damage;
            spawnedEnemy.stats.ArmourPenetration = enemiesToSpawn[i].ArmourPenetration;
            spawnedEnemy.stats.CriticalChance = enemiesToSpawn[i].CriticalChance;
            spawnedEnemy.stats.HitRate = enemiesToSpawn[i].HitRate;
            spawnedEnemy.stats.MaxHealth = enemiesToSpawn[i].MaxHealth;
            spawnedEnemy.stats.Armour = enemiesToSpawn[i].Armour;
            spawnedEnemy.stats.EvadeRate = enemiesToSpawn[i].EvadeRate;
            spawnedEnemy._movementSpeed = enemiesToSpawn[i].MovementSpeed;
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }

        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private T getRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => UnityEngine.Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
    }
    public void ShowPath(Tile end) {
        pathToShow = pathFinder.FindPath(SelectedHero.OccupiedTile, end, false);
        for (int i = 0; i < pathToShow.Count; i++)
        {
            var previousTile = i > 0 ? pathToShow[i - 1] : SelectedHero.OccupiedTile;
            var futureTile = i < pathToShow.Count - 1 ? pathToShow[i + 1] : null;

            var arrowDir = arrowTranslator.TranslateDirection(previousTile, pathToShow[i], futureTile);

            pathToShow[i].SetArrowSprite(arrowDir);
        }
    }

    public void HidePath() {
        foreach (var item in inRangeTiles) {
            item.SetArrowSprite(ArrowDirection.None);
        }
    }
    public int AttackAction(Stats StatsAtc, Stats StatsDef)
    {
        double dmgTaken = 0;
        double randomHitAtc = UnityEngine.Random.Range(0, StatsAtc.HitRate);
        double randomEvadeDef = UnityEngine.Random.Range(0, StatsDef.EvadeRate);
        if (randomHitAtc > randomEvadeDef)
        {
            int crit = UnityEngine.Random.Range(0, StatsAtc.CriticalChance);
            if (crit == 1)
            {
                dmgTaken = StatsAtc.Dmg * 2;
            }
            else
            {
                dmgTaken = StatsAtc.Dmg;
            }
            double armourPenetration = StatsAtc.ArmourPenetration / 100.0; // Convert armor penetration to percentage
            double armorAfterPenetration = StatsDef.Armour * (1 - armourPenetration);
            dmgTaken -= armorAfterPenetration;
        }
        Debug.Log("dmg taken " + dmgTaken);
        Debug.Log("dmg is " + StatsAtc.Dmg);
        if(dmgTaken < 0)
        {
            dmgTaken = 0;
        }
        return (int)Math.Round(dmgTaken);
    }
}
