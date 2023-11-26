using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private string skillName = "DoublbeShooting";
    [SerializeField] private string description;
    [SerializeField] private Weapon weapon;
    public Button button;
}
