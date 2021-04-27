using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private GameObject EnemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool StopSpawning = false;
    // Start is called before the first frame update
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (StopSpawning == false)
        {
            Vector3 pos = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            GameObject newenemy = Instantiate(Enemy, pos, Quaternion.identity);
            newenemy.transform.parent = EnemyContainer.transform;
            yield return new WaitForSeconds(3.5f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (StopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 7, 0);
            int RandomPoweUp = Random.Range(0, 3);
            Instantiate(powerups[RandomPoweUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 10));
        }
    }
    public void OnPlayerDeath()
    {
        StopSpawning = true;
    }

}
 
