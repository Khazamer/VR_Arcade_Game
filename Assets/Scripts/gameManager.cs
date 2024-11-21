using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class gameManager : MonoBehaviour
{
    [SerializeField] TMP_Text wordDisplay;
    [SerializeField] GameObject levelStorage;
    [SerializeField] GameObject startingPad;
    [SerializeField] GameObject levelSelectors;
    [SerializeField] GameObject raycast;
    private GameObject currLevel;

    void restartLevelSelect() {
        gameObject.transform.position = startingPad.transform.position + new Vector3(0, 1, 0);

        wordDisplay.SetText("Point at a cube and press a trigger to select a level");

        levelSelectors.SetActive(true);

        raycast.GetComponent<raycast>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        restartLevelSelect();

        foreach(Transform child in levelStorage.transform) {
            child.gameObject.GetComponent<levelManager>().enabled = false;
        }
    }

    void Update() {
        if (globals.levelTag != "") {
            foreach(Transform child in levelStorage.transform) {
                if(child.CompareTag(globals.levelTag)) {
                    currLevel = child.gameObject;

                    currLevel.GetComponent<levelManager>().enabled = true;
                    raycast.GetComponent<raycast>().enabled = false;

                    wordDisplay.SetText("");

                    levelSelectors.SetActive(false);

                    globals.levelTag = "";
                }
            }
        }
    }
}
