using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Splines;

public class levelManager : MonoBehaviour
{
    [Header("Level Specific")]
    [SerializeField] string levelName;
    [SerializeField] int numParts = 3;
    [SerializeField] List<int> enemiesPerPart; // total enemies
    [SerializeField] List<int> spawnWavesPerPart; // number of waves
    [SerializeField] List<int> spawnNumPerWave; // number of enemies per wave (might change into a list of lists down the line) [THIS IS AT EACH SPAWN POINT NOT TOTAL]
    [SerializeField] List<int> spawnChancePerPart; // chance of spawning a warrior or drone
    [SerializeField] List<int> timeBetweenWavesPerPart; // interval between waves
    [SerializeField] List<int> preBattleTimePerPart = new List<int>{0,0,0}; // time before the fighting starts
    [SerializeField] List<int> postBattleTimePerPart = new List<int>{0,0,0}; // time after the fighting is done before movement
    //Would have made displays multi worded but double lists were giving me issues
    [SerializeField] List<string> preBattleWordsPerPart = new List<string>{"","",""}; // words to show before fighting
    [SerializeField] List<string> postBattleWordsPerPart = new List<string>{"","",""}; // words to show after fighting
    [SerializeField] List<GameObject> levelParts; // gameobjects of each part of level containing spawn locations and/or text
    [SerializeField] List<SplineContainer> levelSplines; // splines to move between different parts of each level
    [SerializeField] GameObject itemContainer; // object holding all items

    [Header("Spawning")]
    [SerializeField] GameObject warriorTemplate;
    [SerializeField] GameObject droneTemplate;

    [Header("Extra")]
    [SerializeField] GameObject playerOrigin;
    [SerializeField] TMP_Text wordDisplay;

    // Internal tracking
    private int levelPart = 0;
    private Transform levelTransform;
    private int enemiesLeft = -1;
    private SplineAnimate splineAnimation;
    private SplineContainer currSpline;
    private bool showWords;
    private float wordTimer;
    private float wordDisplayTime;
    private GameObject origin;
    // tracking of weapons and locations so they can be put back where they should be
    List<GameObject> weaponObjects = new List<GameObject>();
    List<Vector3> weaponLocations = new List<Vector3>();

    // Enemy Spawning
    void summonEnemies() { //remove summon script
        //Debug.Log("summon");
        //Debug.Log(levelPart);
        StartCoroutine(spawnEnemies(levelPart));
    }

    IEnumerator spawnEnemies(int ind) {
        //yield return new WaitForSeconds(splineAnimation.Duration + preBattleTimePerPart[ind]);

        for (int i = 0; i < spawnWavesPerPart[ind]; i++) {
            Debug.Log("wave");
            //levelObj = levelParts[ind];
            //currObjLoc = levelParts[ind].transform.getChil;
            levelTransform = levelParts[ind].transform;
            foreach (Transform child in levelTransform) {
                Debug.Log("child");
                for (int j = 0; j < spawnNumPerWave[ind]; j++) { // may change later to randomize or split num of spawns between the spawners since that is a bit confusing in the system
                    Debug.Log("spawning");
                    Debug.Log(levelTransform.transform.position);
                    Debug.Log(child.transform.position);
                    Debug.Log(child.transform.localPosition);

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

    void nextPart() {
        if (levelPart == 0) {
            playerOrigin.transform.position = levelParts[levelPart].transform.position;// + new Vector3(0, 1, 0);

            Invoke("summonEnemies", preBattleTimePerPart[0]);

            if (preBattleWordsPerPart[0] != "") { // may change down the line if I only use words at the beginning of the level
                togglePreWords();
            }
        }
        else if (levelPart == numParts) { //need to add some sort of wait for this but otherwise it works
            //gameObject.GetComponent<levelManager>().enabled = false;

            // remove weapons and put them back
            for (int i = 0; i < weaponObjects.Count(); i++) {
                weaponObjects[i].transform.position = weaponLocations[i];
                weaponObjects[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                weaponObjects[i].transform.parent = itemContainer.transform;
            }           

            origin.GetComponent<gameManager>().restartLevelSelect(); 

            gameObject.SetActive(false);
        }
        else {
            currSpline = levelSplines[levelPart - 1];

            splineAnimation.Container = currSpline; //add some sort of indicator

            splineAnimation.Restart(true);

            Invoke("summonEnemies", splineAnimation.Duration + preBattleTimePerPart[levelPart]);

            if (preBattleWordsPerPart[levelPart] != "") {
                Invoke("togglePreWords", splineAnimation.Duration);
            }
        }

        enemiesLeft = enemiesPerPart[levelPart];
    }

    void togglePreWords() {
        wordDisplay.SetText(preBattleWordsPerPart[levelPart]);
        wordDisplayTime = preBattleTimePerPart[levelPart];
        showWords = true;
    }

    void togglePostWords() {
        wordDisplay.SetText(postBattleWordsPerPart[levelPart]);
        wordDisplayTime = postBattleTimePerPart[levelPart];
        showWords = true;
    }

    public void levelStart() {
        levelPart = 0;

        Debug.Log("Started");
        splineAnimation = playerOrigin.GetComponent<SplineAnimate>();

        Debug.Log("0");
        origin = FindObjectOfType<XROrigin>().gameObject;

        Debug.Log("1");

        enemiesLeft = -1;
        globals.numKills = 0;

        Debug.Log("2");

        // get weapon locations and names
        foreach(Transform child in itemContainer.transform) { // may move to its own start loop since we only need it once but idk if it resets random variables
            Debug.Log("3");
            weaponObjects.Add(child.gameObject);
            Debug.Log("4");
            weaponLocations.Add(child.transform.position);
            Debug.Log("5");
        }

        Debug.Log("6");

        Invoke("nextPart", 0.1f); // all scripts are initally disabled for levels and we have a check (should probs do that myself tho)
    }

    void Update() {
        //Debug.Log("in game");
        //Debug.Log(globals.numKills);
        // Show enemies left - NEEDS SOME WORK
        //int left = enemiesLeft - globals.numKills;
        //Debug.Log(left);

        if (globals.numKills == enemiesLeft) {
            Invoke("nextPart", postBattleTimePerPart[levelPart]);

            if (postBattleWordsPerPart[levelPart] != "") {
                togglePostWords();
            }

            globals.numKills = 0;
            enemiesLeft = -1;

            levelPart ++;
        }

        // show words down here for timing purposes - OLD CODE FOR MULTI WORDS
        /*
        if (showWords) {
            wordTimer += Time.deltaTime;

            wordDisplay.SetText(currWords[wordIndex]);
            
            if ((wordIndex + 1) * timePerWord < wordTimer) {
                wordIndex += 1;
                if (wordIndex > currWords.Count) {
                    showWords = false;
                    wordDisplay.SetText("");
                    wordIndex = 0;
                    wordTimer = 0;
                }
            }
        }
        */

        if (showWords) {
            wordTimer += Time.deltaTime;
            if (wordTimer > wordDisplayTime) {
                wordDisplay.SetText("");
                wordTimer = 0;
                showWords = false;
            }
        }
    }
}