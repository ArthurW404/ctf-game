using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSACamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Initial pos
        this.transform.position = new Vector3(-4, 8.58f, -20.55f);
    }

    // Update is called once per frame
    void Update()
    {
        var delta = Time.deltaTime;
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var z = this.transform.position.z;
        // target pos (-4, 5.6, -8.6)
        if (z < -8.6f && y > 5.65f){
            this.transform.position = new Vector3(x, y - delta * 2, z + delta * 8);
        }
    }
}
