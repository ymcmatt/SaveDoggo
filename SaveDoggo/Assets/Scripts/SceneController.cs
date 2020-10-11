using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
    public static int retry = 0;
    private VideoPlayer vid;
    public Image fade;

    public static SceneController S;
    public GameObject[] canvas;

    
    void Start()
    {
        vid = GetComponent<VideoPlayer>();
        S = this;
    }

    public static void StartFuture()
    {
        SceneManager.LoadScene("FutureLab 2");
    }

    public static void StartFlashback()
    {
        SceneManager.LoadScene("Flashback");
    }

    public void StartPast()
    {
        ///eventually have code to play animations
        StartCoroutine(PastTransition());
        
    }

    public static void StartVictory()
    {
        ///eventually have code to play animations
        SceneManager.LoadScene("Victory");
    }

    IEnumerator PastTransition()
    {
        SoundController.SC.StopBGM();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Flashback")
        {
            fade.color = Color.black;
            SoundController.SC.PlaySound("DogConflict");
            yield return new WaitForSeconds(2f);
            vid.frame = 300;
            vid.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            vid.Play();
            yield return new WaitForSeconds(0.1f);
            fade.color = Color.clear;
            SoundController.SC.PlaySound("TimeWhir");
            yield return new WaitForSeconds(2.37f);
            SceneManager.LoadScene("MainScene_LightingReworked");
        }
        else
        {
            // Disable all the UI
            foreach (GameObject c in canvas)
            {
                c.SetActive(false);
            }
            Debug.Log("!!");
            // Time.timeScale = 0;
            //fade.color = Color.black;
            //SoundController.SC.PlaySound("DogConflict");
            //yield return new WaitForSeconds(2f);

            Debug.Log("Starting Video");
            //SoundController.SC.PlaySound("TimeWhir");
            // For now, start about 4 seconds into the video
            vid.frame = 300;
            vid.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            vid.Play();
            yield return new WaitForSeconds(0.05f);
            //SoundController.SC.PlaySound("TimeWhir");
            //fade.color = Color.clear;
            yield return new WaitForSeconds(13f);
            Debug.Log("Resetting");
            SceneManager.LoadScene("MainScene_LightingReworked");
        }

    }
}
