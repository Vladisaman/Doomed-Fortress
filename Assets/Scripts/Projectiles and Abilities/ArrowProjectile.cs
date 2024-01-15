using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    public Action OnArrowHit;
    private SkillManager skillManager;
    private bool DidRicochet;
    [SerializeField] GameObject holyAoe;
    [SerializeField] GameObject cursedAoe;

    private readonly string NAME_OF_WEAPON = "Ballista";
    private bool hasEntered = false;
    List<GameObject> targetList;

    private void Start()
    {
        targetList = new List<GameObject>();
        skillManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null && !hasEntered) {

            enemy.TakeDamage(damage, NAME_OF_WEAPON);

            if (enemy.GetComponent<Shield>() == null && enemy.GetComponent<StoneEnemy>() == null) {
                OnArrowHit?.Invoke();
            }
            hasEntered = true;

            if (skillManager.holyArrow)
            {
                Instantiate(holyAoe, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
            }

            if (skillManager.cursedArrow)
            {
                Instantiate(cursedAoe, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity).GetComponent<CursedAoeScript>().FollowedObject = enemy.transform;
            }
        }

        if (skillManager.RicochetArrow && hasEntered)
        {
            if (!DidRicochet)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                if (enemies.Length > 0)
                {
                    for (int i = 0; i < (3 > enemies.Length ? 3 : enemies.Length); i++)
                    {
                        float minDist = float.MaxValue;
                        GameObject closestObj = null;
                        foreach (GameObject obj in enemies)
                        {
                            if (Vector2.Distance(transform.position, obj.transform.position) < minDist)
                            {
                                closestObj = obj;
                                minDist = Vector2.Distance(transform.position, obj.transform.position);
                            }
                            targetList.Add(closestObj);
                        }
                    }
                    DidRicochet = true;
                }
            }
        } 
        else if (!skillManager.RicochetArrow && hasEntered)
        {
            Destroy(gameObject);
        }


        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Rock"))
        {
            Destroy(collision.gameObject);
        }
    }

    private void Update()
    {
        if (DidRicochet)
        {
            if (targetList.Count > 0)
            {
                Vector2.Lerp(transform.position, targetList[targetList.Count - 1].transform.position, Time.deltaTime);
                if (Vector2.Distance(transform.position, targetList[targetList.Count - 1].transform.position) <= 0.01)
                {
                    targetList.RemoveAt(targetList.Count - 1);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}