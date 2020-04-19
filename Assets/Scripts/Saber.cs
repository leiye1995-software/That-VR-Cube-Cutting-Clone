using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saber : MonoBehaviour
{

    public ScoreController scoreController;
    /*
    0 = red
    1 = blue
    */
    public int type = 0;

    public Text angleText;

    private Vector3 previousPosition = new Vector3();

    void Update()
    {
        /*Debug.Log("Update previous position: " + previousPosition);
        Debug.Log("Update position: " + transform.position);*/
        previousPosition = transform.position;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("CutFace"))
        {
            Debug.Log("Previous: " + previousPosition);
            Debug.Log("Current: " + transform.position);
            Debug.Log("Saber direction: " + (transform.position - previousPosition));
            Debug.Log("Cube up: " + collider.transform.up);
            CubeBehaviour cube = collider.GetComponentInParent<CubeBehaviour>();
            //from Valem Youtube channel "MAKING BEAT SABER IN 10 MIN - Unity Challenge" (gh4k0Q1Pl7E)
            //check if cut direction is correct: get angle between the saber and the cube up direction
            //saber cutting direction should match as much as posible to cube up direction
            float angle = Vector3.Angle(transform.position - previousPosition, collider.transform.up);
            Debug.Log(angle);
            angleText.text = ((int)angle).ToString();
            if (angle < 45f)
            {
                //direction is correct
                Debug.Log("Good");
                scoreController.GoodCut();
                cube.GoodCut();
            }
            else
            {
                //direciton is not correct
                Debug.Log("Bad");
                scoreController.BadCut();
                cube.BadCut();
            }
        }
        else if (collider.CompareTag("NotCutFace"))
        {
            CubeBehaviour cube = collider.GetComponentInParent<CubeBehaviour>();
            scoreController.BadCut();
            cube.BadCut();
        }
        /*if (collider.CompareTag("CutFace"))
        {
            CubeBehaviour cube = collider.GetComponentInParent<CubeBehaviour>();
            if (type == cube.type)
            {
                scoreController.GoodCut();
                cube.GoodCut();
            }
            else
            {
                scoreController.BadCut();
                cube.BadCut();
            }
        }
        else if (collider.CompareTag("NotCutFace"))
        {
            CubeBehaviour cube = collider.GetComponentInParent<CubeBehaviour>();
            scoreController.BadCut();
            cube.BadCut();
        }*/
    }
}
