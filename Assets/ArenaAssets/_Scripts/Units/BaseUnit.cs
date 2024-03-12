using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{

    public string UnitName;
    public Tile OccupiedTile;
    public Faction Faction;
    public int _movementSpeed;
    public int _attackRange;
    public int _currentHealth;
    public int _maxHealth;
}


