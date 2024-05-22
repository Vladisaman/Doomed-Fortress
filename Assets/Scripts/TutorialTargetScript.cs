using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTargetScript : MonoBehaviour
{
    Enemy enemy;
    public TutorialScript script;
    bool once;

    // Start is called before the first frame update
    void Start()
    {
        once = false;
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.health <= 0 && once == false)
        {
            once = true;
            script.currentMobKillAmount+=1;
            script.currEnemiesKillCount += 1;
        }
    }
}
