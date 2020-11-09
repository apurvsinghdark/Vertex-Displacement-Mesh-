using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{ 
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 20 * Time.time, 0);  
    }
}
