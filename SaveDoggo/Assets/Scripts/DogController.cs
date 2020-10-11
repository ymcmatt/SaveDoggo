using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{

    [SerializeField]
    private Animator ani;

    private Camera cam;
    private Transform follow;
    private NavMeshAgent dog;

    public bool following;
    public Vector3 fetchLocation;

    private int speedHash = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        follow = GameObject.FindWithTag("FollowLocation").transform;
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        dog = GetComponent<NavMeshAgent>();
        following = true;
        dog.SetDestination(follow.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fetch"))
        {
            SoundController.SC.PlaySound("DogGo", 1);
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit ))
            {
                following = false;
                fetchLocation = hit.point;
                dog.SetDestination(fetchLocation);
                //Debug.Log("Fetching");
            }
        }

        if (Input.GetButtonDown("Return"))
        {
            SoundController.SC.PlaySound("DogCome", 1);
            following = true;
        }

        //Alright time to interact with the dog
        if(Input.GetButtonDown("Interact"))
        {
            if(ItemInteract.dogHolding == null)
            {
                float difference = (this.transform.position - follow.position).magnitude;
                if (difference < 2f )
                {
                    //Dog barks, if the facing is right.
                    //For facing:
                    Vector3 dogPlayerVector = follow.position - transform.position;
                    Vector3 playerVector = follow.forward;
                    if(Vector3.Dot(dogPlayerVector, playerVector) < -0.2)
                    {
                        Debug.Log("Bark!");
                        SoundController.SC.PlaySfx("DogSadShort1");
                    }                    
                }
                //Check the distance:

            }
            //Look for the player, lazy implementation, if the player is within range of the dog
            // and the dog is not holding anything, the dog barks.
        }

        if (following)
        {
            float difference = (this.transform.position - follow.position).magnitude;
            if (difference > 2f )
            {
                //Debug.Log("Resuming");
                dog.Resume();
                dog.SetDestination(follow.position);
            }
            else if (difference < 0.5f)
            {
                //Debug.Log("Stopping");
                dog.velocity = Vector3.zero;
                if (ItemInteract.dogHolding != null)
                {
                    ItemInteract.dogHolding.PutDownDog();
                }
            }
           
        }

        ani.SetFloat(speedHash, dog.velocity.magnitude);

    }
}
