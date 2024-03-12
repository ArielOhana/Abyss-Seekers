using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ArrowTranslator;
using static UnityEditor.Progress;
using static UnityEngine.UI.CanvasScaler;

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
    public List<Tile> inRangeTiles = new List<Tile>();
    public List<Tile> inAttackRangeTiles = new List<Tile>();
    public int hits = 3;

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
        //if(unitToMove.Faction == Faction.Ememy)
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
                    isFirst = true;
                    toAttack = false;
                    calc = 0;
                    isMoving = false; // Stop moving if there are no more points in the path
                    return;
                }
            }

            // Move the unit towards the next point in the path
            unitToMove.transform.position = Vector2.MoveTowards(unitToMove.transform.position,
                path[0].transform.position,
                _unityMoveSpeed * Time.deltaTime
                );
        } else {
            isFirst = true;
            toAttack = false;
            calc = 0;
            isMoving = false; // Stop moving if there are no more points in the path
        }
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
                toAttack = true;
                unitToMove = enemy;
                path = pathFinder.FindPath(unitToMove.OccupiedTile, TheHero.OccupiedTile, toAttack);
                calc = 0;
                isMoving = true;
                StartCoroutine(MoveAttack(TheHero));
                yield return new WaitUntil(() => !isMoving);
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
            unitToAttack._currentHealth = ((int)hits * 80);
        }
        if (hits == 0) {
            yield return new WaitUntil(() => !isMoving);
            var hero = (BaseHero)unitToAttack; // if clicked on OccupiedUnit and we have selected a hero, attack

            SceneManager.LoadScene("AfterLose");
        }
        else {
            hits--;
        }
            
    }


        //create list of all enemies spawned
        //repeat for all Enemies foreach loop
        //find path to hero,
        //check if path.Count > move + range
        //true => just move,
        //false => moveAttack,


    public void SpawnHeros()
    {
        var heroCount = 1;

        for (int i = 0; i < heroCount; i++)
        {
            var randomPrefab = getRandomUnit<BaseHero>(Faction.Hero);
            var spawnedHero = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetHeroSpawnTile();

            randomSpawnTile.SetUnit(spawnedHero);
        }
        GameManager.Instance.ChangeState(GameState.SpawnEnemies);
    }

    public void SpawnEnemies()
    {
        var enemyCount = 3;

        for (int i = 0; i < enemyCount; i++)
        {
            var randomPrefab = getRandomUnit<BaseEnemy>(Faction.Ememy);
            var spawnedEnemy = Instantiate(randomPrefab);
            var randomSpawnTile = GridManager.Instance.GetEnemySpawnTile();

            randomSpawnTile.SetUnit(spawnedEnemy);
        }
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    private T getRandomUnit<T>(Faction faction) where T : BaseUnit
    {
        return (T)_units.Where(u => u.Faction == faction).OrderBy(o => Random.value).First().UnitPrefab;
    }

    public void SetSelectedHero(BaseHero hero)
    {
        SelectedHero = hero;
        MenuManager.Instance.ShowSelectedHero(hero);
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
        /*foreach (var item in pathToShow) {
            
            item.SetArrowSprite(ArrowTranslator.ArrowDirection.None);
            Debug.Log("pathShow" + item.gridLocation);
        }*/
    }
}
