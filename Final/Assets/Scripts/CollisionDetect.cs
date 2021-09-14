using System;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<RunmanController>().GameOver(true);
    }
}