using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWall : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Cube"))
        {
            Transform transformToDelete = collider.transform;
            Transform parent = collider.transform.parent;
            while (parent != null)
            {
                transformToDelete = parent;
                parent = parent.parent;
            }
            Destroy(transformToDelete.gameObject);
        }
    }
}
