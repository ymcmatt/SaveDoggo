using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleMeasurer : MonoBehaviour
{
    private float weight = 0.0f;
    public Text scaleText;
    public AudioSource audio;
    private bool isTaken = false;
    private bool hasWeights = false;
    public GameObject iron_door;

    // Start is called before the first frame update
    void Start()
    {
        audio = this.transform.parent.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.weight != 3)
        {
            if(!audio.isPlaying){
                audio.Play();
            }
            //dogBark.Play();
        }
        if (this.weight == 3)
        {
            if (hasWeights)
            {
                if (isTaken)
                {
                    iron_door.SetActive(false);
                }                
            }
            audio.Stop();
            //dogBark.Stop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
        {
            if (other.tag == "weights")
            {
                hasWeights = true;
            }
            this.weight += rb.mass;
            Debug.Log(this.weight);
            if (isTaken)
            {
                scaleText.text = "Scale now has weight " + this.weight.ToString();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if(rb != null)
        {
            if (other.tag == "weights")
            {
                hasWeights = true;
            }
            this.weight -= rb.mass;
            Debug.Log(this.weight);
            scaleText.text = "Scale now has weight " + this.weight.ToString();
            isTaken = true;
        }
    }
}
