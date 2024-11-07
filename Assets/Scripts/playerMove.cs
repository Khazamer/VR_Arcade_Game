using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class playerMove : MonoBehaviour
{
    private GameObject splines;
    public InputActionReference AButton;
    public GameObject playerOrigin;
    private SplineContainer splineMove;
    private bool activate = true;
    private SplineAnimate splineAnimation;
    private Spline splineTest;
    // Start is called before the first frame update
    void Start()
    {
        splines = GameObject.Find("Splines");
    }

    // Update is called once per frame
    void Update()
    {
        if (AButton.action.IsPressed() && activate) {
            activate = false;

            splineMove = splines.transform.GetChild(1).GetComponent<SplineContainer>();

            //splineAnimation = playerOrigin.AddComponent<SplineAnimate>();
            splineAnimation = playerOrigin.GetComponent<SplineAnimate>();

            splineAnimation.Container = splineMove;

            splineAnimation.Play();
        }
    }
}
