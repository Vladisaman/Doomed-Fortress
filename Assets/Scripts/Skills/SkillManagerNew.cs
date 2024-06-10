using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagerNew : MonoBehaviour
{
    public Skill[] AllSkills;
    public List<Skill> ActiveSkills;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Skill
{
    public string name;
    int cost;
    public Rarity rarity;
    public WeaponType weaponType;
    
    public Skill()
    {
        switch (rarity)
        {
            case Rarity.COMMON:
                cost = 50;
                break;
            case Rarity.RARE:
                cost = 75;
                break;
            case Rarity.EPIC:
                cost = 150;
                break;
            case Rarity.LEGENDARY:
                cost = 200;
                break;
        }
    }

    public int GetCost()
    {
        return cost;
    }
}

public enum WeaponType
{
    CROSSBOW,
    MORTAR,
    FIREGUN,
    WALL
}

public enum Rarity
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}



public enum SkillType
{

}