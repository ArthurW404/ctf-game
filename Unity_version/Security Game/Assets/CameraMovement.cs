using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mouseXMovement = Input.GetAxis("Mouse X");
        var mouseYMovement = Input.GetAxis("Mouse Y");
    }
}
