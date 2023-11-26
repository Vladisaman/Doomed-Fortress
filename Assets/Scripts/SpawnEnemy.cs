using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private int _timeDelay;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [SerializeField] private float minYForFlying;
    [SerializeField] private float maxYForFlying;

    [SerializeField] private float spawnTime2;
    [SerializeField] private float spawnTime3;
    [SerializeField] private float spawnAfterShield;
    [SerializeField] private float spawnTime4;
    [SerializeField] private float spawnTime5;
    [SerializeField] private float spawnTime6;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

     private IEnumerator SpawnEnemies()
     {
        /*-----1 WAVE-----*/
        for (int i = 0; i < 7; i++) {
            Instantiate(_enemies[0],RandomPosition(minY, maxY), Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnTime2);
        /*-----2 WAVE-----*/
        Instantiate(_enemies[1], gameObject.transform);
        yield return new WaitForSeconds(spawnTime3);

        /*-----3 WAVE-----*/
        Instantiate(_enemies[2], gameObject.transform);
        yield return new WaitForSeconds(spawnAfterShield);
        Instantiate(_enemies[3], gameObject.transform);
        yield return new WaitForSeconds(spawnTime4);

        /*-----4 WAVE-----*/
        Instantiate(_enemies[4], RandomPosition(minYForFlying, maxYForFlying), Quaternion.identity);
        Instantiate(_enemies[5], RandomPosition(minYForFlying, maxYForFlying), Quaternion.identity);
        Instantiate(_enemies[4], gameObject.transform);
        Instantiate(_enemies[5], gameObject.transform);
        yield return new WaitForSeconds(spawnTime5);

        /*-----5 WAVE-----*/
        Instantiate(_enemies[6], gameObject.transform);
        yield return new WaitForSeconds(spawnTime6);

        /*-----6 WAVE-----*/
        Instantiate(_enemies[7], gameObject.transform);


        yield return null;
     }

    private Vector3 RandomPosition(float minY, float maxY)
    {
        return new Vector3(transform.position.x, Random.Range(minY, maxY), transform.position.z);
    }
}
