using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : Enemy
{
    [SerializeField] Enemy Mob;
    CurrentWeakness currentWeakness;
    Spawner spawner;

    [SerializeField] GameObject arrowResistancePNG;
    [SerializeField] GameObject bombResistancePNG;
    [SerializeField] GameObject firegunResistancePNG;

    public int castWaitAmount = 10;


    // Start is called before the first frame update
    void Start()
    {
        currentWeakness = CurrentWeakness.none;
        StartCoroutine(AbilityCastCoroutine());
        spawner = (Spawner)FindObjectOfType(typeof(Spawner));

        arrowResistancePNG.gameObject.SetActive(false);
        bombResistancePNG.gameObject.SetActive(false);
        firegunResistancePNG.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.position.x <= 0.8)
        {
            speed = 0;
            animator.SetBool("IsStanding", true);
        } 
        else
        {
            Move();
            animator.SetBool("IsStanding", false);
        }

        CheckDeath();
        healthBar.value = health;

        if(isAlive == false)
        {
            spawner.BossKilled();
        }
    }

    private void SummonMobs()
    {
        animator.SetTrigger("CastSummon");

        Instantiate(Mob, transform.position + new Vector3(1, 1, 0), Quaternion.identity);
        Instantiate(Mob, transform.position + new Vector3(1, -1, 0), Quaternion.identity);
        Instantiate(Mob, transform.position + new Vector3(-1, 1, 0), Quaternion.identity);
        Instantiate(Mob, transform.position + new Vector3(-1, -1, 0), Quaternion.identity);
    }

    private void CycleResistance()
    {
        animator.SetTrigger("CastSpell");

        arrowDamageResist = DamageResistance.IMMUNE;
        bombDamageResist = DamageResistance.IMMUNE;
        fireDamageResist = DamageResistance.IMMUNE;

        int weakness = Random.Range(1, 4);
        var v = System.Enum.GetValues(typeof(CurrentWeakness));
        currentWeakness = (CurrentWeakness) v.GetValue(weakness);

        switch (currentWeakness)
        {
            case CurrentWeakness.ballista:
                arrowDamageResist = DamageResistance.WEAK;

                arrowResistancePNG.gameObject.SetActive(false);
                bombResistancePNG.gameObject.SetActive(true);
                firegunResistancePNG.gameObject.SetActive(true);
                break;
            case CurrentWeakness.mortar:
                bombDamageResist = DamageResistance.WEAK;

                arrowResistancePNG.gameObject.SetActive(true);
                bombResistancePNG.gameObject.SetActive(false);
                firegunResistancePNG.gameObject.SetActive(true);
                break;
            case CurrentWeakness.firegun:
                fireDamageResist = DamageResistance.WEAK;

                arrowResistancePNG.gameObject.SetActive(true);
                bombResistancePNG.gameObject.SetActive(true);
                firegunResistancePNG.gameObject.SetActive(false);
                break;
        }
    }

    private void BuffEnemies()
    {
        animator.SetTrigger("CastSpell");

        Object[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != this)
            {
                enemy.BuffSpeed();
            }
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