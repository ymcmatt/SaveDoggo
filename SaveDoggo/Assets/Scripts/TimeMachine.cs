    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMachine : MonoBehaviour
{

    public bool change = false;
    public bool cameraRotating = false;
    public Image fade;
    public Color clear = new Color(0.8f, 0.8f, 0.8f, 0);
    public Color full = new Color(0.8f, 0.8f, 0.8f, 1);
    public Animator animator;
    public CameraController cameraControl;
    public Transform frontface;
    private Transform cameraOrigin;

    float t = 0;
    float d = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        fade.color = clear;
        this.animator = GetComponent<Animator>();
        //this.cameraOrigin = cameraControl.transform.position;
    }

    // Update is called once per frame
    //Ok, we gonna wait for the door to close, then do everything else.
    void Update()
    {
        if (change)
        {

            if (t > d)
            {
                //SceneController.StartFlashback();
                change = false;
            }
            else
            {
                fade.color = Color.Lerp(clear, full, t / d);
                t += Time.deltaTime;

            }
            
        }
        else
        {
            AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
            if(asi.IsName("time_travel") && asi.normalizedTime > 1.0f)
            {
                change = true;
                
            }
            else if(cameraRotating)
            {
                cameraControl.transform.position = Vector3.Lerp(cameraControl.transform.position, frontface.position, 0.1f);
                cameraControl.transform.rotation = Quaternion.Lerp(cameraControl.transform.rotation, frontface.rotation, 0.1f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //change = true;
            SoundController.SC.PlaySfx("TimeWhir");
            animator.SetBool("IsPlayerInside", true);
            cameraRotating = true;
            cameraControl.enabled = false;
            StartCoroutine(FadeOut());
        }
    }

    public IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(4.5f);
        t = 0;
        while (t < 2f)
        {
            t += Time.deltaTime;
            fade.color = Color.Lerp(clear, full, t / 2f);
            yield return null;
        }
        SceneController.StartFlashback();
    }
}
