using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSwipe : Interactable
{
    public DoorController door;
    private ItemInteract card;
    private Transform cardT;
    bool inProgress = false;
    public float swipeTime = 1f;
    public Quaternion align;
    public float swipe = 0.2f;
    public Vector3 startP;
    public Vector3 endP;

    public string key = "Key";


    // Start is called before the first frame update
    void Start()
    {
        align = Quaternion.Euler(90, -90, 0);      
    }


    // TODO: Make item pickup happen last in the update loop
    public override void Interact()
    {
        base.Interact();
        // Assume that only ID card interactables are accessable at this time
        if (inFocus && !inProgress && ItemInteract.pHolding != null)
        {
            actionLock = true;
            StartCoroutine(SwipeCard());
        }

    }

    public override void LoseFocus()
    {
        
    }

    IEnumerator SwipeCard()
    {
        inProgress = true;
        Vector3 adjust = new Vector3(0, swipe / 2, -0.05f);
        card = ItemInteract.pHolding;
        cardT = card.gameObject.transform;
        cardT.rotation = align;
        startP = this.gameObject.transform.position + adjust;
        endP = startP;
        endP.y += swipe;


        float t = 0;
        while (t < swipeTime)
        {
            t += Time.deltaTime;
            cardT.position = Vector3.Lerp(startP, endP, t / swipeTime);
            yield return null;
        }

        card.PutDownPlayer();

        Debug.Log(key + " - " + card.gameObject.name);
        if (card.gameObject.name == key)
        {
            SoundController.SC.PlaySfx("Beep");
            StartCoroutine(door.Open());
        }
        else
        {
            SoundController.SC.PlaySfx("BadBeep", 1);
        }

        inProgress = false;
    }
}
