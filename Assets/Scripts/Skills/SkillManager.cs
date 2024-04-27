using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public bool crossbowCanDoubleShot;
    //public bool crossbowPlusOneArrow;
    //public bool crossbowPlusTwoArrow;
    //public bool crossbowPlusThreeArrow;
    //public bool crossbowPlusFourArrow;
    public bool FanArrows;
    //public bool RicochetArrow;
    public bool cursedArrow;
    public bool holyArrow;
    public bool ColdArrow;
    public bool PhantomArrow;
    public bool PoisonArrow;
    public int CrossbowBlessing;

    [Space(20)]

    public bool fireGunleftFireEnable;
    public bool fireGunrightFireEnable;
    public bool BlackFire;

    [Space(20)]

    public bool bigYadroEnable;
    public bool smallYadroEnable;
    public bool knockbackCannonball;
    public bool holyBomb;
    public bool cursedBomb;
    public bool fireBomb;
    public bool ColdYadro;
    public bool PoisonYadro;
    public int MortarBlessing;

    [Space(20)]

    public int WallHp;
    public bool Vampirism;
    public bool Thorns;

    private void Start()
    {
        MortarBlessing = 0;
        CrossbowBlessing = 0;
        WallHp = 0;
    }
}
