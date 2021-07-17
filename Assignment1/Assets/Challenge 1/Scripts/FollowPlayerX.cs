using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    //followed target
    public GameObject plane;

    //relative position
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        //the camera always locate at offset position relative to the plane local position
        transform.position = plane.transform.position + offset;
        //the camera always look at plane
        transform.LookAt(plane.transform);
    }
}