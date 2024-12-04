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
    [SerializeField] GameObject selectHand;
    private GameObject currLevel;

    public void startLevel(RaycastHit hit) {
        Debug.Log("here");
        foreach(Transform child in levelStorage.transform) {
            Debug.Log("Searching");
            if(child.CompareTag(hit.transform.tag)) {
                Debug.Log("found");
                currLevel = child.gameObject; //all next levels wont have that tag

                //currLevel.GetComponent<levelManager>().enabled = true;
                // Enable or disable whole object instead
                currLevel.SetActive(true);

                //selectHand.GetComponent<handRayCast>().enabled = false;

                wordDisplay.SetText("");

                levelSelectors.SetActive(false);

                currLevel.GetComponent<levelManager>().levelStart();

                Debug.Log("Starting level");
            }
        }
    }

    public void restartLevelSelect() {
        gameObject.transform.position = startingPad.transform.position;// + new Vector3(0, 1, 0);

        wordDisplay.SetText("Click with the right trigger to select a level");

        //selectHand.GetComponent<handRayCast>().enabled = true;

        levelSelectors.SetActive(true);

        // Need to put weapons back so nvm
        /*
        if (leftHand.transform.childCount > 0) {
            Destroy(leftHand.transform.GetChild(0));
        }
        if (rightHand.transform.childCount > 0) {
            Destroy(rightHand.transform.GetChild(0));
        }
        */

        //selectHand.GetComponent<raycast>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        restartLevelSelect();

        /*
        foreach(Transform child in levelStorage.transform) {
            //child.gameObject.GetComponent<levelManager>().enabled = false;
            child.gameObject.SetActive(false);
        }
        */
    }

/*
    void Update() {
        if (globals.levelTag != "") {
            foreach(Transform child in levelStorage.transform) {
                if(child.CompareTag(globals.levelTag)) {
                    currLevel = child.gameObject;

                    currLevel.GetComponent<levelManager>().enabled = true;
                    //raycast.GetComponent<raycast>().enabled = false;

                    wordDisplay.SetText("");

                    levelSelectors.SetActive(false);

                    globals.levelTag = "";
                }
            }
        }
    }
    */
}
