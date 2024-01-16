using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    //TODO избавиться от этого класса как такового за его бесполезностью, мб переработать, хотя смысла в этом чуть больше нуля.

    [SerializeField] private string skillName = "DoublbeShooting";
    [SerializeField] private string description;
    [SerializeField] private Weapon weapon;
    [SerializeField] private WallBehavior wall;
    public Button button;
}
