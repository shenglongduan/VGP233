using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    private void LateUpdate()
    {
        var targetPosition = Target.position;
        transform.position = targetPosition + Offset;
    }
}