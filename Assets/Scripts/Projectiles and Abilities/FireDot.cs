using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDot : Projectile
{
    private Enemy parentEnemy;
    private int ticks;
    private float delay;
    private string NAME_OF_WEAPON = "FireGun";

    FireGun fireGun;

    private void Start()
    {
        fireGun = GameObject.Find("FireGun").GetComponent<FireGun>();

        damage = fireGun.dotDamage;
        ticks = fireGun.dotTicks;
        delay = fireGun.dotDelay;
        parentEnemy = GetComponent<Enemy>();
        StartCoroutine(DotDamage());
    }


    private IEnumerator DotDamage()
    {
        for (int i = 0; i < ticks; i++) {
            parentEnemy.TakeDamage(damage, NAME_OF_WEAPON);
            yield return new WaitForSeconds(delay);
        }

        Destroy(this);
    }

}