using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PursuerController : MonoBehaviour
{
    private Transform pursuer;
    private float normalSpeed = 0.75f;
    private float sprintSpeed = 3f;
    public Transform scientist;
    public GameObject sci;
    private VideoPlayer failure;
    public Camera cam;

    public UnityEngine.AI.NavMeshAgent chase;

    // Start is called before the first frame update
    void Start()
    {
        pursuer = this.transform;
        failure = GetComponent<VideoPlayer>();
        chase = GetComponent<UnityEngine.AI.NavMeshAgent>();
        chase.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.z - pursuer.position.z;
        float distance2 = Vector3.Magnitude(scientist.position - pursuer.position);
            //scientist.position.z - pursuer.position.z;
        //Debug.Log(distance);
        //if (cam.transform.position.z)
        
        // Pursuer swaps to actually chasing the player
        if (distance <= 2f)
        {
            chase.enabled = true;
            chase.SetDestination(scientist.position);
            

        }
        else
        {
            pursuer.Translate(0f, 0f, normalSpeed * Time.deltaTime);
        }

        if (distance2 <= 1f)
        {
            Destroy(sci);
            Destroy(gameObject, 1);
            SceneController.S.StartPast();
            Debug.Log("You get caught");

            // Destroy(gameObject, 1);
            // failure.Play();
        }
    }
}
