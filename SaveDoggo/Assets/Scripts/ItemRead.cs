using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemRead : Interactable
{
    public Item item;
    public Image img;
    private bool isRead = false;
    public Sprite instructionUI;

    // AudioSource audio;

    void Start()
    {

    }

    public override void Interact()
    {
        base.Interact();
        if (inFocus)
        {
            actionLock = true;
            Read();
        }
        
    }

    void Read()
    {
        if (!isRead)
        {
            //if (Input.GetButtonDown("Interact"))
            //{
                Debug.Log("!!!");
                img.sprite = instructionUI;
                img.enabled = !img.enabled;
                isRead = true;
            //}
        }
        else
        {
            //if (Input.GetButtonDown("Interact"))
            //{
                img.enabled = !img.enabled;
                isRead = false;
                img.sprite = null;
            //}
        }

    }

    public override void LoseFocus()
    {
        img.enabled = false;
        isRead = false;
        img.sprite = null;
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
}
