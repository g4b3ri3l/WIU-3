using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject SawFish;
    [SerializeField] private GameObject SwordFish;
    [SerializeField] private GameObject SeaHorse;

    [SerializeField] private float interval = 3.5f;
    [SerializeField] private int maxEnemies = 10; // Maximum number of enemies to spawn
    private int enemyCount = 0; // Counter to keep track of the number of spawned enemies

    public int phase = 1;
    [SerializeField] private PlayableDirector timeline;

    private void OnEnable()
    {
        if (timeline.state != PlayState.Playing)
        {
            if (phase == 1)
                StartCoroutine(spawnEnemy(interval, SawFish));
        }
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        while (enemyCount < maxEnemies) // Check if the number of spawned enemies is less than the maximum
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-133f, -75f), Random.Range(-138f, -122f), 0), Quaternion.identity);
            enemyCount++; // Increment the counter each time an enemy is spawned
        }

        phase = 2;

    }

}
