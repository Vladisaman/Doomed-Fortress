using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : Enemy
{
    [SerializeField] Enemy Mob;
    CurrentWeakness currentWeakness;

    private int castWaitAmount = 10;


    // Start is called before the first frame update
    void Start()
    {
        currentWeakness = CurrentWeakness.none;
        StartCoroutine(AbilityCastCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SummonMobs()
    {
        Instantiate(Mob, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        Instantiate(Mob, transform.position + new Vector3(1, -1, 0), Quaternion.identity);
        Instantiate(Mob, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);
        Instantiate(Mob, transform.position + new Vector3(-1, -1, 0), Quaternion.identity);
    }

    private void CycleResistance()
    {
        arrowDamageResist = DamageResistance.IMMUNE;
        bombDamageResist = DamageResistance.IMMUNE;
        fireDamageResist = DamageResistance.IMMUNE;

        int weakness = Random.Range(2, 4);
        var v = System.Enum.GetValues(typeof(CurrentWeakness));
        currentWeakness = (CurrentWeakness) v.GetValue(weakness);

        switch (currentWeakness)
        {
            case CurrentWeakness.ballista:
                arrowDamageResist = DamageResistance.WEAK;
                break;
            case CurrentWeakness.mortar:
                bombDamageResist = DamageResistance.WEAK;
                break;
            case CurrentWeakness.firegun:
                fireDamageResist = DamageResistance.WEAK;
                break;
        }
    }

    private void BuffEnemies()
    {
        Object[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (Enemy enemy in enemies)
        {
            enemy.BuffSpeed();
        }
    }

    private IEnumerator AbilityCastCoroutine()
    {
        while (isAlive)
        {
            SummonMobs();
            Debug.Log("1");
            yield return new WaitForSeconds(castWaitAmount);
            BuffEnemies();
            Debug.Log("2");
            yield return new WaitForSeconds(castWaitAmount);
            CycleResistance();
            Debug.Log("3");
            yield return new WaitForSeconds(castWaitAmount);
        }
    }
}

public enum CurrentWeakness{
    none,
    ballista,
    mortar,
    firegun,
}