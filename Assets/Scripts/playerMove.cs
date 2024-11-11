using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class playerMove : MonoBehaviour
{
    //SOME WEIRD ISSUE CAUSING LAG IDK WHY. NEED TO FIX
    private GameObject splines;
    public InputActionReference AButton;
    public GameObject playerOrigin;
    private SplineContainer splineMove;
    private bool activate = true;
    private SplineAnimate splineAnimation;
    private Spline splineTest;
    public List<SplineContainer> yay;
    private int count = 0;
    public bool aSimulator;
    // Start is called before the first frame update
    void Start()
    {
        //splines = GameObject.Find("Splines");
    }

    // Update is called once per frame
    void Update()
    {
        if (AButton.action.IsPressed() && activate) {
        //if (aSimulator && activate) {
            activate = false;

            Debug.Log(count);

            //splineMove = splines.transform.GetChild(1).GetComponent<SplineContainer>();
            splineMove = yay[count];

            //splineAnimation = playerOrigin.AddComponent<SplineAnimate>();
            splineAnimation = playerOrigin.GetComponent<SplineAnimate>();

            splineAnimation.Container = splineMove;

            //splineAnimation.Play();
            splineAnimation.Restart(true);

            count ++;

            if (count == yay.Count) {
                count = 0;
            }
        }
        else if (!AButton.action.IsPressed() && activate == false) {
        //else if (!aSimulator && activate == false) {
            activate = true;
        }
    }
}
