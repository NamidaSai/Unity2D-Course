using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerSpawner : MonoBehaviour
{
    [SerializeField] float minTimeBetweenSpawn = 1f;
    [SerializeField] float maxTimeBetweenSpawn = 5f;
    [SerializeField] Attacker[] attackerPrefabArray = default;

    bool spawn = true;
    float timeBetweenSpawns;

    IEnumerator Start()
    {
        while (spawn)
        {
            timeBetweenSpawns = Random.Range(minTimeBetweenSpawn, maxTimeBetweenSpawn);
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnAttacker();
        }
    }

    public void StopSpawning()
    {
        spawn = false;
    }

    private void SpawnAttacker()
    {
        var selectedAttackerIndex = Random.Range(0,attackerPrefabArray.Length);
        Spawn(attackerPrefabArray[selectedAttackerIndex]);
    }

    private void Spawn(Attacker selectedAttacker)
    {
        Attacker newAttacker = Instantiate
            (selectedAttacker,
             transform.position,
             Quaternion.identity) as Attacker;
        newAttacker.transform.parent = transform;
    }
}
