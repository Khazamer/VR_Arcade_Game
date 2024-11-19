using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class summonEnemies : MonoBehaviour
{
    [SerializeField] int spawnRounds = 1;
    [SerializeField] int spawnNum = 1;
    [SerializeField] int spawnChance = 90;
    [SerializeField] GameObject warriorTemplate;
    [SerializeField] GameObject droneTemplate;
    public List<Transform> spawnLocations;
    
    void startUpEnemies() {
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies() {
        for (int i = 0; i < spawnRounds; i++) {
            foreach (var curTransform in spawnLocations) {
                for (int j = 0; j < spawnNum; j++) {
                    int chance = Random.Range(0, 100);

                    if (chance < spawnChance) {
                        GameObject newWarrior = Instantiate(warriorTemplate, curTransform);
                    }
                    else {
                        GameObject newDrone = Instantiate(droneTemplate, curTransform.position + (curTransform.up * 3), curTransform.rotation);
                    }

                }
            }
        }

        yield return new WaitForSeconds(10);
    }
}
