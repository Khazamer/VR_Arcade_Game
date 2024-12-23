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
    [SerializeField] GameObject nextLevelPart;
    [SerializeField] List<int> enemiesPerPart; // total enemies
    [SerializeField] List<int> spawnWavesPerPart; // number of waves
    [SerializeField] List<int> spawnNumPerWaveAtEachPoint; // number of enemies per wave (might change into a list of lists down the line) [THIS IS AT EACH SPAWN POINT NOT TOTAL]
    [SerializeField] List<int> spawnChancePerPart; // chance of spawning a warrior or drone
    [SerializeField] List<int> timeBetweenWavesPerPart; // interval between waves
    [SerializeField] List<float> preBattleTimePerPart = new List<float>{0.1f,0.1f,0.1f}; // time before the fighting starts
    [SerializeField] List<float> postBattleTimePerPart = new List<float>{0.1f,0.1f,0.1f}; // time after the fighting is done before movement
    //Would have made displays multi worded but double lists were giving me issues
    [SerializeField] List<string> preBattleWordsPerPart = new List<string>{"","",""}; // words to show before fighting
    [SerializeField] List<string> postBattleWordsPerPart = new List<string>{"","",""}; // words to show after fighting
    [SerializeField] List<GameObject> levelParts; // gameobjects of each part of level containing spawn locations and/or text
    [SerializeField] List<SplineContainer> levelSplines; // splines to move between different parts of each level
    [SerializeField] List<GameObject> weapons; // list of weapons to spawn in
    //[SerializeField] GameObject itemContainer; // object holding all items
    [SerializeField] GameObject moveCube; // cube indicating movement
    [SerializeField] ParticleSystem moveEffect;
    [SerializeField] GameObject itemStorage; // weapons that were added in are stored here as children to be removed

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
    private GameObject mainCamera;
    // tracking of weapons and locations so they can be put back where they should be
    //private List<GameObject> weaponObjects = new List<GameObject>();
    //private List<Vector3> weaponLocations = new List<Vector3>();
    //private List<GameObject> enemies = new List<GameObject>();

    // Enemy Spawning
    void summonEnemies() { //remove summon script
        //Debug.Log("summon");
        //Debug.Log(levelPart);
        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies() {
        //yield return new WaitForSeconds(splineAnimation.Duration + preBattleTimePerPart[ind]);

        int ind = levelPart;

        for (int i = 0; i < spawnWavesPerPart[ind]; i++) {
            //Debug.Log("wave");
            //levelObj = levelParts[ind];
            //currObjLoc = levelParts[ind].transform.getChil;
            levelTransform = levelParts[ind].transform;
            foreach (Transform child in levelTransform) {
                //Debug.Log("child");
                for (int j = 0; j < spawnNumPerWaveAtEachPoint[ind]; j++) { // may change later to randomize or split num of spawns between the spawners since that is a bit confusing in the system
                    //Debug.Log("spawning");
                    Debug.Log(levelTransform.transform.position);
                    Debug.Log(child.transform.position);
                    Debug.Log(child.transform.localPosition);

                    int chance = Random.Range(0, 100);

                    if (chance < spawnChancePerPart[ind]) {
                        GameObject newWarrior = Instantiate(warriorTemplate, child);
                        //enemies.Append(newWarrior);
                    }
                    else {
                        GameObject newDrone = Instantiate(droneTemplate, child.position + (child.up * 4), child.rotation);
                        //enemies.Append(newDrone);
                    }

                    //wordDisplay.SetText(enemies.Count().ToString());
                }
            }

            yield return new WaitForSeconds(timeBetweenWavesPerPart[ind]);
        }
    }

    void nextPart() {
        if (levelPart == 0) {
            playerOrigin.transform.position = levelParts[levelPart].transform.position;// + new Vector3(0, 0.5f, 0);

            Invoke("summonEnemies", preBattleTimePerPart[0] + 1f);

            Invoke("spawnWeapon", 1f);

            if (preBattleWordsPerPart[0] != "") { // may change down the line if I only use words at the beginning of the level
                togglePreWords();
            }
        }
        else if (levelPart == numParts) { //need to add some sort of wait for this but otherwise it works
            // if there is only 1 part to the level
            if (nextLevelPart == null) {
                //gameObject.GetComponent<levelManager>().enabled = false;

                /*
                // remove weapons and put them back
                for (int i = 0; i < weaponObjects.Count(); i++) {
                    weaponObjects[i].transform.position = weaponLocations[i];
                    weaponObjects[i].transform.rotation = Quaternion.Euler(0, 0, 0);
                    weaponObjects[i].transform.parent = itemContainer.transform;
                }  
                */         

                origin.GetComponent<gameManager>().restartLevelSelect(); 

                //Invoke("disableObject", 3f);
                disableObject();
            }
            else {
                //disableObject();

                nextLevelPart.SetActive(true);
                nextLevelPart.GetComponent<levelManager>().levelStart();

                origin.GetComponent<playerHealth>().resetHealth();

                gameObject.SetActive(false);
            }
        }
        else {
            currSpline = levelSplines[levelPart - 1];

            splineAnimation.Container = currSpline; //add some sort of indicator

            splineAnimation.Restart(true);

            Invoke("summonEnemies", splineAnimation.Duration + preBattleTimePerPart[levelPart] + 1f);

            Invoke("spawnWeapon", 1f);
            
            //moveCube.GetComponent<doTween>().moveCube();

            if (preBattleWordsPerPart[levelPart] != "") {
                Invoke("togglePreWords", splineAnimation.Duration);
            }
        }

        enemiesLeft = enemiesPerPart[levelPart];
    }

    public void playerDied() {
        /*
        // remove weapons and put them back
        for (int i = 0; i < weaponObjects.Count(); i++) {
            weaponObjects[i].transform.position = weaponLocations[i];
            weaponObjects[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponObjects[i].transform.parent = itemContainer.transform;
        }      
        */ 

        StopCoroutine(spawnEnemies());

        enemiesLeft = -1;
        globals.numKills = 0;

        globals.gameOver = true;

        /*
        for (int j = 0; j < enemies.Count(); j ++) {
            //Destroy(enemies[j]);
            try {
                Destroy(enemies[j]);
            }
            catch {
                //nothing here cuz no need
                wordDisplay.SetText(enemies[j].name);
            }
        }    
        */

        Invoke("disableObject", 3f);
    }

    void spawnWeapon() {
        if (weapons[levelPart] != null) {
            GameObject weapon = Instantiate(weapons[levelPart], levelParts[levelPart].transform.position + new Vector3 (0,2,0), mainCamera.transform.rotation);
            weapon.transform.position += weapon.transform.forward * 1f;

            weapon.transform.parent = itemStorage.transform;
        }
    }

    void disableObject() {
        /*
        // remove weapons and put them back
        for (int i = 0; i < weaponObjects.Count(); i++) {
            weaponObjects[i].transform.position = weaponLocations[i];
            weaponObjects[i].transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponObjects[i].transform.parent = itemContainer.transform;
        }   
        */
        
        // See if I should remove weapons
        /*
        if (nextLevelPart == null) {
            // Delete them entirely
            foreach(Transform child in itemStorage.transform) {
                Destroy(child.gameObject);
            }

            // Gets rid of guns in hands
            foreach (GameObject hand in GameObject.FindGameObjectsWithTag("Hand")) {
                if (hand.transform.childCount > 0) {
                    Destroy(hand.transform.GetChild(0).gameObject);
                }
            }
        }
        */
        
        // New system
        foreach (GameObject pistol in GameObject.FindGameObjectsWithTag("Pistol")) {
            Destroy(pistol);
        }
        foreach (GameObject SMG in GameObject.FindGameObjectsWithTag("SMG")) {
            Destroy(SMG);
        }
        foreach (GameObject shotgun in GameObject.FindGameObjectsWithTag("Shotgun")) {
            Destroy(shotgun);
        }
        foreach (GameObject grenadeLauncher in GameObject.FindGameObjectsWithTag("GrenadeLauncher")) {
            Destroy(grenadeLauncher);
        }

        gameObject.SetActive(false);
    }

    void movementCube() {
        moveCube.GetComponent<doTween>().moveCube(); //not consistent for some reason
        //moveEffect.Play();
    }

    void movementParticles() {
        moveEffect.Play();
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

        //Debug.Log("Started");
        splineAnimation = playerOrigin.GetComponent<SplineAnimate>();

        //Debug.Log("0");
        origin = FindObjectOfType<XROrigin>().gameObject;
        mainCamera = FindObjectOfType<Camera>().gameObject;

        //Debug.Log("1");

        enemiesLeft = -1;
        globals.numKills = 0;

        //Debug.Log("2");

        // get weapon locations and names - no need since gonna instantiate the weapons anyways
        /*
        foreach(Transform child in itemContainer.transform) { // may move to its own start loop since we only need it once but idk if it resets random variables
            Debug.Log("3");
            weaponObjects.Add(child.gameObject);
            Debug.Log("4");
            weaponLocations.Add(child.transform.position);
            Debug.Log("5");
        }
        */

        //Debug.Log("6");

        Invoke("nextPart", 0.1f); // all scripts are initally disabled for levels and we have a check (should probs do that myself tho)
    }

    void Update() {
        //Debug.Log("in game");
        //Debug.Log(globals.numKills);
        // Show enemies left - NEEDS SOME WORK
        //int left = enemiesLeft - globals.numKills;
        //Debug.Log(left);

        if (globals.numKills == enemiesLeft) {
            Invoke("nextPart", Mathf.Max(postBattleTimePerPart[levelPart], 2f));

            //Invoke("movementCube", Mathf.Max(postBattleTimePerPart[levelPart] - 2f, 0.01f)); //some issue with invoking?
            Invoke("movementParticles", Mathf.Max(postBattleTimePerPart[levelPart] - 2f, 0.01f));

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
            //wordDisplay.SetText(wordTimer.ToString());
            if (wordTimer > wordDisplayTime) {
                wordDisplay.SetText("");
                wordTimer = 0;
                showWords = false;
            }
        }
    }
}