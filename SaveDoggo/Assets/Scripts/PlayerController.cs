using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator ani;
    private Rigidbody rb;
    private Transform camTr;
    private Transform playerTr;

    public float moveSpeed = 10.0f;
    public float rotateSpeed = 50f;
    public float jumpHeight = 5.0f;
    public float heading;
    public float rotate;
    public float rotateDamp = 10f;
    public new Vector3 playerDirection;
    private CharacterController cc;
    //private HashSet<GameObject> kickable;

    private bool action;
    private bool cameraLock = false;

    // Scripting hashes
    private int speedHash = Animator.StringToHash("Speed");
    private int interactHash = Animator.StringToHash("Interact");

    [SerializeField]
    private float check = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        //ani = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        action = false;
        camTr = this.transform.GetChild(0).transform;
        playerTr = this.transform;
        cc = GetComponent<CharacterController>();
        

        //kickable = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!action)
        {
            //heading = Mathf.Deg2Rad * camTr.rotation.eulerAngles.y;
            //float player_Horizontal = Input.GetAxis("Horizontal");
            //float player_Vertical = Input.GetAxis("Vertical");
            //Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            //playerDirection = this.transform.right * player_Horizontal + this.transform.forward * player_Vertical;

            //this.transform.rotation = Quaternion.LookRotation(movement);

            //if (movement.magnitude > Mathf.Epsilon)
            //{

            //    // Rotate Avatar to move the same way as camera
            //    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(new Vector3(0f, camTr.rotation.eulerAngles.y, 0f)), Time.deltaTime * rotateDamp);
            //    this.transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
            //    // Move Avatar in direction camera is facing
            //    Vector3 altered = new Vector3();
            //    altered.x = (movement.x * Mathf.Cos(heading)) + (movement.z * Mathf.Sin(heading));
            //    altered.z = (movement.z * Mathf.Cos(heading)) - (movement.x * Mathf.Sin(heading));
            //    altered.y = rb.velocity.y / moveSpeed;
            //    rb.velocity = altered * moveSpeed;
            //    rb.velocity = playerDirection * moveSpeed;

            //}
            //else
            //{
            //    rb.velocity = new Vector3(0, rb.velocity.y, 0);
            //}

            //ani.SetFloat(speedHash, movement.magnitude);
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


            if(moveHorizontal != 0 || moveVertical != 0){
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
            }
            Vector3 downward = new Vector3(0f, -10f, 0f);
            //this.transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
            //rb.AddForce(movement * moveSpeed * Time.deltaTime);
            cc.Move(movement * moveSpeed * Time.deltaTime);
            cc.Move(downward * 10f * Time.deltaTime);
            ani.SetFloat(speedHash, movement.magnitude);

        }
    }


    public void PlayLiftHandAnimation()
    {
        ani.SetTrigger(interactHash);
    }

}