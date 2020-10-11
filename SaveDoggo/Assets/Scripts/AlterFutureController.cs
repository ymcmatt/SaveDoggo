using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlterFutureController : MonoBehaviour
{
    public Light dogLight;
    public Light timeLight;
    public Light timeGlow;

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
        intoDialogue[1] = "'This time I got out with Gougou'";
        intoDialogue[2] = "'Since then, I have finally completed the machine from the papers that I had salvaged.'";
        intoDialogue[3] = "'Though I don't see a reason to use it.'";
        doggoDialogue = new string[4];
        doggoDialogue[0] = "Hey Gougou it's me.";
        doggoDialogue[1] = "want to go for a walk?";
        doggoDialogue[2] = "heard there is a new theme park called 'Build Virtural World'";
        doggoDialogue[3] = "Let's go Gougou";

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
        SoundController.SC.PlaySound("DogHappy");
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
            timeLight.intensity = Mathf.Lerp(0, lightMax, time / transitionTime);
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
                SceneManager.LoadScene("Victory");
            }


        }
    }
}
