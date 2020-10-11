using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculateWeight : MonoBehaviour
{
    private Transform scale;
    private List<Rigidbody> objectsList;
    float totalWeight = 0;
    public Text scaleText;
    //AudioSource audio;
    float detectRadius = 0.2f;
    float boundX = -1;
    float boundZ = -1;

    // Start is called before the first frame update
    void Start()
    {
        scale = this.transform;
        objectsList = new List<Rigidbody>();
        //audio = GetComponent<AudioSource>();

        if (boundX <= 0)
        {
            boundX = detectRadius;
        }

        if (boundZ <= 0)
        {
            boundZ = detectRadius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount % 50 == 0)
        {
            DetermineWeight();
        }
    }

    void DetermineWeight()
    {
        Collider[] weights = Physics.OverlapSphere(scale.position, detectRadius);

        for (int i = 0; i < weights.Length; i++)
        {
            if (weights[i].gameObject.tag == "weights")
            {
                if (!objectsList.Contains(weights[i].gameObject.GetComponent<Rigidbody>()))
                {
                    objectsList.Add(weights[i].gameObject.GetComponent<Rigidbody>());
                }
            }

        }
        // Debug.Log(objectsList);
        for (int i = 0; i < objectsList.Count; i++)
        {
            if (CheckValidity(objectsList[i]))
            {
                totalWeight += objectsList[i].mass;
            }
            
        }
        //scaleText.text = "Scale now has weight " + totalWeight.ToString();
        /*
        if (totalWeight != 3)
        {
            if(!audio.isPlaying){
                audio.Play();
            }
            
            //dogBark.Play();
        }
        if (totalWeight == 3)
        {
            audio.Stop();
            //dogBark.Stop();
        }*/

        totalWeight = 0;
        objectsList.Clear();
    }

    bool CheckValidity(Rigidbody rb)
    {
        if (!rb.useGravity)
        {
            return false;
        }
        Vector3 otherPos = rb.gameObject.transform.position;
        if (Mathf.Abs(otherPos.x - scale.position.x) < boundX && Mathf.Abs(otherPos.z - scale.position.z) < boundZ)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
}
