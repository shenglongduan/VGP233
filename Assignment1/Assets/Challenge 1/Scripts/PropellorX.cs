using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellorX : MonoBehaviour
{
    //rotation speed
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        //propellor rotates at a speed with variable rotationSpeed
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}