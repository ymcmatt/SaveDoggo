using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FutureLabController : MonoBehaviour
{
    public Light deskLight;
    public Light dogLight;
    public Light timeLight;
    public Light timeGlow;
    public GameObject glowingTrail;
    public Text monologue;

    public float lightMax = 2f;
    public float transitionTime = 2f;

    

    int index = 0;

    public string[] intoDialogue;
    public string[] doggoDialogue;
    private string[] targetDialogue;

    private bool pause = false;
    private bool dogState = true;

    // Start is called before the first frame update
    void Start()
    {
        dogLight.intensity = 0;
        timeLight.intensity = 0;
        timeGlow.intensity = 0;
        intoDialogue = new string[4];
        intoDialogue[0] = "'Five years ago the government raided my lab trying to find the secret for time machine'";
        intoDialogue[1] = "'I got caught but Gougou saved me from them.'";
        intoDialogue[2] = "'Since then, I have finally completed the machine from the papers that I had salvaged.'";
        intoDialogue[3] = "'...all at the sacrifice of Gougou.'";
        doggoDialogue = new string[4];
        doggoDialogue[0] = "It's just me, Gougou, it's just me.";
        doggoDialogue[1] = "Since that day 5 years ago, you haven't been able sleep in peace, have you?";
        doggoDialogue[2] = "5 years without being able to see, fend for yourself, or barely hear...";
        doggoDialogue[3] = "This time I'll save you, Gougou. I promise.";

        NextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!pause)
            {
                NextLine();
            }
        }
    }

    public IEnumerator LightDog()
    {
        Debug.Log("Starting Light Dog");
        pause = true;
        monologue.text = "";
        SoundController.SC.PlaySound("DogSadLong");
        float time = 0f;
        while (time < transitionTime)
        {
            time += Time.deltaTime;
            dogLight.intensity = Mathf.Lerp(0, lightMax, time / transitionTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return new WaitForSeconds(.1f);
        pause = false;
        NextLine();
    }

    public IEnumerator LightTimeMachine()
    {
        Debug.Log("Starting Light Machine");
        float time = 0f;
        while (time < transitionTime)
        {
            time += Time.deltaTime;
            dogLight.intensity = Mathf.Lerp(lightMax, 0, time / transitionTime);
            deskLight.intensity = Mathf.Lerp(lightMax, 0, time / transitionTime);
            timeLight.intensity = Mathf.Lerp(0, lightMax, time / transitionTime);
            glowingTrail.GetComponent<Renderer>().material.SetFloat("_Visibility", Mathf.Lerp(0, 1, time / transitionTime));
            //timeLight.intensity = Mathf.Lerp(0, lightMax, time / transitionTime);
            timeGlow.intensity = Mathf.Lerp(0, 1, time / transitionTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void NextLine()
    {
        if (index < 4)
        {
            if (dogState)
            {
                monologue.text = intoDialogue[index];
            }
            else
            {
                monologue.text = doggoDialogue[index];
            }
            
            index++;
        }
        else
        {
            if (dogState)
            {
                index = 0;
                targetDialogue = doggoDialogue;
                StartCoroutine(LightDog());
                dogState = false;
            }
            else
            {
                StartCoroutine(LightTimeMachine());
            }
            
           
        }
    }
}
