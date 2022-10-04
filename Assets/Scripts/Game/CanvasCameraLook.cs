using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraLook : MonoBehaviour
{
    private void FixedUpdate()
    {
        Quaternion rot = Camera.main.transform.rotation;
        rot.x = 0; rot.z = 0;
        transform.rotation = rot;
    }
}
