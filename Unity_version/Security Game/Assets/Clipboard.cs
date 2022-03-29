using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
   
    private bool visible;

    // Start is called before the first frame update
    void Start()
    {
        visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        var delta = Time.deltaTime;
        var x = this.transform.position.x;
        var y = this.transform.position.y;
        var z = this.transform.position.z;
        
        // If visible and not in visible position, go to visible position
        if (!visible && z <= -50f && y > -50f && x <= 60f ){
            this.transform.position = new Vector3(x + delta * 64, y - delta * 64 , z + delta * 64);
        }


        // Go back to normal state
        if (visible && z >= -115f  &&  y < 8f && x >= 0) {
            this.transform.position = new Vector3(x - delta * 64, y + delta * 64 , z - delta * 64);
        }

        if(Input.GetMouseButtonDown(0))
        {
            // Whatever you want it to do.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
            RaycastHit hit;  
            if (Physics.Raycast(ray, out hit)) {  
                //Select stage    
                if (hit.transform.name == "Clipboard") {  
                    visible = !visible;
                    Debug.Log(visible);
                }  
            }  
        }
    }
}
