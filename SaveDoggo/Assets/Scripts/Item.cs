using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This allows us to in Unity right click and create new assets -> Inventory -> Item
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        // something might happen
        // well be overwritten by children Use()
        Debug.Log("Using " + name);
    }

}
