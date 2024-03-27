using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject _enemyToInstantiate;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _isStopped = false;
    [SerializeField]
    private GameObject[] _powerups;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_isStopped == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 6.4f, 0);
            GameObject enemyNew = Instantiate(_enemyToInstantiate, posToSpawn, Quaternion.identity);
            enemyNew.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_isStopped == false)
        {
            Vector3 posToSpawnAt = new Vector3(Random.Range(-8f, 8f), 6.4f, 0);
            int randomPowerup = Random.Range(0, 2);
            Instantiate(_powerups[randomPowerup], posToSpawnAt, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }
    public void StopSpawning()
    {
        _isStopped = true;
    }
}
