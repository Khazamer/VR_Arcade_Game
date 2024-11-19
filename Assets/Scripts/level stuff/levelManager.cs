using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class levelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Header("Level Specific")]
    [SerializeField] int numParts = 3;
    [SerializeField] List<int> enemiesPerWave = new List<int>{19, 12};
    [SerializeField] List<int> spawnCyclesPerPart = new List<int>{1, 3};
    [SerializeField] List<int> spawnNumPerPart = new List<int>{1, 4};
    [SerializeField] List<int> spawnChancePerPart = new List<int>{100, 90};
    [SerializeField] List<int> timeBetweenWavesPerPart = new List<int>{10, 10};
    [SerializeField] List<GameObject> levelParts;

    [Header("Spawning")]
    [SerializeField] int spawnRounds = 1;
    [SerializeField] int spawnNum = 1;
    [SerializeField] int spawnChance = 90;
    [SerializeField] GameObject warriorTemplate;
    [SerializeField] GameObject droneTemplate;

    // Internal tracking
    private int count = 0;
    private GameObject levelObj = null;
    private Transform levelTransform;
    private List<Transform> currObjLoc;

    // Enemy Spawning
    void summonEnemies() { //remove summon script
        StartCoroutine(spawnEnemies(count));
    }

    IEnumerator spawnEnemies(int ind) {
        for (int i = 0; i < spawnCyclesPerPart[i]; i++) {
            //levelObj = levelParts[ind];
            //currObjLoc = levelParts[ind].transform.getChil;
            levelTransform = levelParts[ind].transform;
            foreach (Transform child in levelTransform) {
                for (int j = 0; j < spawnNumPerPart[ind]; j++) {
                    int chance = Random.Range(0, 100);

                    if (chance < spawnChancePerPart[ind]) {
                        GameObject newWarrior = Instantiate(warriorTemplate, child);
                    }
                    else {
                        GameObject newDrone = Instantiate(droneTemplate, child.position + (child.up * 3), child.rotation);
                    }
                }
            }

            yield return new WaitForSeconds(timeBetweenWavesPerPart[ind]);
        }
    }
}