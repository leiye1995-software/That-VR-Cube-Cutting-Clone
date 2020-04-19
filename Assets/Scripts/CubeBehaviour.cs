using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{

    public GameObject blueCubeMesh;
    public GameObject redCubeMesh;
    public GameObject directionalImage;
    public GameObject dotImage;
    public GameObject directionalCollider;
    public GameObject dotCollider;


    public GameObject mine;

    public float speed = 10f;

    public float initialTime;

    public int type;

    public void SetCubeProperties(int type, int direction)
    {
        blueCubeMesh.SetActive(false);
        redCubeMesh.SetActive(false);
        mine.SetActive(false);
        directionalImage.SetActive(false);
        dotImage.SetActive(false);
        directionalCollider.SetActive(false);
        dotCollider.SetActive(false);

        this.type = type;

        //0 = red, 1 = blue, 3 = mine
        if (type == 0)
        {
            redCubeMesh.SetActive(true);
        }
        else if (type == 1)
        {
            blueCubeMesh.SetActive(true);
        }
        else if (type == 3)
        {
            mine.SetActive(true);
        }

        if (type != 3)
        {
            /*
            0 = bottom to top
            1 = top to bottom
            2 = right to left
            3 = left to right
            4 = bottom right to top left
            5 = bottom left to top right
            6 = top right to bottom left
            7 = top left to bottom right
            8 = dot
            */
            directionalImage.SetActive(true);
            directionalCollider.SetActive(true);
            //just get the z for euler angle
            float z = 0.0f;
            switch (direction)
            {
                case 0:
                    z = 0.0f;
                    break;
                case 1:
                    z = 180.0f;
                    break;
                case 2:
                    z = 90.0f;
                    break;
                case 3:
                    z = 270.0f;
                    break;
                case 4:
                    z = 45.0f;
                    break;
                case 5:
                    z = 315.0f;
                    break;
                case 6:
                    z = 135.0f;
                    break;
                case 7:
                    z = 225.0f;
                    break;
                case 8:
                    z = 0.0f;
                    directionalImage.SetActive(false);
                    directionalCollider.SetActive(false);
                    dotImage.SetActive(true);
                    dotCollider.SetActive(true);
                    break;
            }
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * -Time.deltaTime * speed);
    }

    public void GoodCut()
    {
        Destroy(gameObject);
    }
    public void BadCut()
    {
        Destroy(gameObject);
    }
}
