using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSetting : MonoBehaviour
{
    private int num = 100;
    public GameObject pursuer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown(num));
    }

    // Update is called once per frame
    void Update()
    {
        if (num == 0)
        {
            pursuer.SetActive(true);
        }
        if (Input.GetKeyDown("escape"))
        {
            //instructionUI.SetActive(!instructionUI.activeSelf);
            //Debug.Log(instructionUI.activeSelf);
            SceneManager.LoadScene("Resume");
        }
    }


    IEnumerator CountDown(int num)
    {
        while (num >= 0)
        {
            yield return new WaitForSeconds(1);
            num--;
        }
    }
}
