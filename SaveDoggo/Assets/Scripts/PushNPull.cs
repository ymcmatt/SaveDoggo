using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushNPull : MonoBehaviour
{
    Vector3 prevLoc = Vector3.zero;
    Transform thingToPull;

    private Transform scientist;

    void Start()
    {
        scientist = this.transform;
    }

    void OnCollisionEnter(Collision hit)
    {
        //Debug.Log(hit.transform.tag);
        if (hit.transform.tag == "Pullable")
        { 
                thingToPull = hit.transform;
                       
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (thingToPull != null)
        {
            if (Input.GetButtonDown("Interact"))
            {
                Debug.Log("pulling");

                //thingToPull.position = Vector3.MoveTowards(thingToPull.position, scientist.position, 5);   
                Vector3 D = transform.position - thingToPull.position; // line from crate to player
                float dist = D.magnitude;
                Vector3 pullDir = D.normalized; // short blue arrow from crate to player
                 // don't pull if too close
                  // this is the same math to apply fake gravity. 10 = normal gravity
                    float pullF = 100;
                    // Now apply to pull force, using standard meters/sec converted
                    //    into meters/frame:
                    Debug.Log(pullDir);
                    thingToPull.GetComponent<Rigidbody>().AddForce(pullDir * (pullF * Time.deltaTime));
                

            }
        }
    }
    
}