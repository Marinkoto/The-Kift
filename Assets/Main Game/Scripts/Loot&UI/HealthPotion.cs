using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealthPotion : ScriptableObject
{
    public Sprite lootSprite;
    public string name;
    public int dropChance;
    public HealthPotion(string _name,int _dropChance)
    {
        this.name = _name;
        this.dropChance = _dropChance;
    }
}
