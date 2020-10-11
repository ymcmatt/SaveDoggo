using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashbackController : MonoBehaviour
{
    public Image fade;
    public Color filter = new Color(0.8f, 0.8f, 0.45f, 0.45f);
    public Color full = new Color(0.8f, 0.8f, 0.8f, 1f);

    public GameObject door;

    public CutsceneMovement dog;
    public CutsceneMovement player;
    public CutsceneMovement pursuer;

    public Camera faceCam;
    public Camera backCam;

    float t = 0;
    float d = 1.0f;

    float timer = 0f;

// Start is called before the first frame update
void Start()
    {
        fade = GameObject.FindWithTag("Respawn").GetComponent<Image>();
        dog = GameObject.FindWithTag("Dog").GetComponent<CutsceneMovement>();
        player = GameObject.FindWithTag("Player").GetComponent<CutsceneMovement>();
        pursuer = GameObject.FindWithTag("Pursuer").GetComponent<CutsceneMovement>();

        fade.color = full;
        StartCoroutine(PlayScene());

        faceCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        backCam = GameObject.FindWithTag("Finish").GetComponent<Camera>();
        faceCam.enabled = true;
        backCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < d)
        {
            timer += Time.deltaTime;
            fade.color = Color.Lerp(full, filter, timer / d);
        }
    }

    public IEnumerator PlayScene()
    {
        dog.Move(Vector3.forward, 3.75f, 4f);
        player.Move(Vector3.forward, 10f, 4f);
        pursuer.Move(Vector3.forward, 2.25f, 5f);
        yield return new WaitForSeconds(2.25f);
        faceCam.enabled = false;
        backCam.enabled = true;
        pursuer.Move(Vector3.forward, 1.5f, 4f);
        yield return new WaitForSeconds(1.5f);
        faceCam.enabled = true;
        backCam.enabled = false;
        faceCam.gameObject.transform.parent = null;
        dog.Rotate(0, -180, 0.2f);
        dog.Move(new Vector3(-1,0,1), 0.5f, 3f);
        pursuer.Move(Vector3.forward, 0.6f, 6f);
        yield return new WaitForSeconds(0.7f);

        SceneController.S.StartPast();
        //Play some suffering SFX

        //StartCoroutine(BlackOut());
    }


    //public IEnumerator FadeOut()
    //{
    //    t = 0;
    //    while (t < d)
    //    {
    //        t += Time.deltaTime;
    //        fade.color = Color.Lerp(filter, full, t / d);
    //        yield return null;
    //    }
    //    SceneController.StartPast();
    //}
}
