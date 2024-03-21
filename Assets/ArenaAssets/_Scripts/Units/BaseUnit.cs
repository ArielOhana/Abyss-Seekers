using Assets;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Assets;

public class BaseUnit : MonoBehaviour
{
    public GameObject floatingPoints;

    public Stats stats;
    public string UnitName;
    public Tile OccupiedTile;
    public int _attackRange;
    public int _movementSpeed;
    public int _currentHealth;
    public int _maxHealth;
    public Faction Faction;
    

}


