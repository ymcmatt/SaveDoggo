using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemInteract : Interactable
{
    public Item item;
    private Transform refPlayer;         // Scientist's refrence point for holding
    private Transform refDog;            // Dog's refrence point for holding
    private bool inRange = false;       // Check to see whether the object is in the player's grabbing range
    public static ItemInteract pHolding = null;   // Static variable to tell whether player is holding an object
    public static ItemInteract dogHolding = null; // Static variable to tell whether dog is holding an object
    
    public Text pickupText;

    bool hasPlayed = false;
    bool isPickedup = false;
    
    private Transform tr;
    private Rigidbody rgb;
    private Collider bc;
    //Camera cam;

    //AudioSource audio;

    void Start()
    {
        //audio = GetComponent<AudioSource>();
        tr = this.transform;
        //cam = Camera.main;
        rgb = this.GetComponent<Rigidbody>();
        bc = this.GetComponent<BoxCollider>();

        refPlayer = GameObject.FindWithTag("PlayerRef").transform;
        refDog = GameObject.FindWithTag("DogRef").transform;
    }


    public override void Interact()
    {
        base.Interact();
        actionLock = false;
        
        
    }

    void LateUpdate()
    {
        if (!actionLock && Input.GetButtonDown("Interact"))
        {
            if (isPickedup)
            {
                if (dogHolding != this)
                {
                    actionLock = true;
                    PutDownPlayer();
                }
                else
                {
                    if (inFocus && pHolding == null)
                    {
                        PutDownDog();
                        actionLock = true;
                        pHolding = this;
                        PickUp(player, refPlayer.localPosition);
                    }
                }
            }
            else
            {
                if (inFocus && pHolding == null)
                {
                    actionLock = true;
                    pHolding = this;
                    PickUp(player, refPlayer.localPosition);
                }
            }
        }
        
    }

    public override void LoseFocus()
    {

    }

    //void PickUp()
    //{
    //    //transform.eulerAngles = new Vector3(0.0f,0.0f,0.0f);
    //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    //Debug.Log("Picking up " + item.name);
    //    //Debug.Log(item.name);
    //    if (!isPickedup)
    //    {
    //        if (Input.GetButtonDown("Interact"))
    //        {
    //            if (Physics.Raycast(ray, out hit))
    //            {
    //                if (hit.collider == bc)
    //                {
    //                    //audio.Play();
    //                    //pickupText.text = "You have picked up the " + item.name;
    //                    isPickedup = true;
    //                    hasPlayed = true;
    //                    rgb.useGravity = false;
    //                    //rgb.detectionCollisions = false;
    //                    rgb.isKinematic = true;
    //                    tr.SetParent(p, false);
    //                    tr.position = refPlayer.position;
    //                    bc.isTrigger = true;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (Input.GetButtonDown("Interact"))
    //        {
    //            tr.parent = null;
    //            isPickedup = false;
    //            bc.isTrigger = false;
    //            rgb.useGravity = true;
    //            //rgb.detectionCollisions = true;
    //            rgb.isKinematic = false;
    //        }
    //    }
    //}

    public void PickUp(Transform target, Vector3 pos)
    {
        isPickedup = true;
        hasPlayed = true;
        rgb.useGravity = false;
        rgb.isKinematic = true;
         
        tr.SetParent(target, false);
        if (target.tag == "Player")
        {
            Vector3 tmp = tr.position;
            tmp.y = player.position.y;
            tmp.z = Mathf.Min( Vector3.Magnitude(tmp - player.position), pos.z);
            tmp.x = 0;
            tmp.y = pos.y;
            tr.localPosition = tmp; // Due to player scale?
        }
        else
        {
            tr.position = pos;
        }
       //bc.enabled = false;
    }

    public void PutDownPlayer()
    {
        pHolding = null;
        isPickedup = false;
        rgb.useGravity = true;
        rgb.isKinematic = false;
        tr.parent = null;
        //bc.enabled = true;
        
    }

    public void PutDownDog()
    {
        dogHolding = null;
        isPickedup = false;
        rgb.useGravity = true;
        rgb.isKinematic = false;
        tr.parent = null;
        Vector3 drop = tr.position + (tr.forward * 0.25f);
        tr.position = drop;
        //bc.enabled = true;

    }

    IEnumerator CountDown()
    {
        int num = 2;
        while (num >= 0)
        {
            yield return new WaitForSeconds(1);
            num--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            Debug.Log(other + "  " + other.gameObject.layer);
            // If not already holding something and not following the player, then attempt to pick up the object by default
            DogController doggo = other.gameObject.GetComponent<DogController>();
            if (dogHolding == null && !doggo.following  && (Vector3.Magnitude(tr.position - doggo.fetchLocation) < 1f) )
            {
                dogHolding = this;
                isPickedup = true;
                PickUp(other.gameObject.transform, refDog.position);
            }
        }
        else
        {
            base.OnTriggerEnter(other);
        }
    }

    
}
