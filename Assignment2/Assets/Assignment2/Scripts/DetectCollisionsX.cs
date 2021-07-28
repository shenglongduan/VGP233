using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisionsX : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //The balls should only be destroyed when coming into direct contact with a dog.
        if (other.CompareTag("Dog"))
        {
            Destroy(gameObject);
        }
    }
}
