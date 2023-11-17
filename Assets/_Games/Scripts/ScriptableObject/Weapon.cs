using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Weapon
{
    public int id;
    public string name;
    public GameObject prefabs;
    public GameObject meshPrefab;
    public Sprite icon;
    public float range;
    public float velocity;
    public bool isReturn;
    public bool isOwner;
    public bool isPlayerEquip;
    public int price;

    [HideInInspector]
    public GameObject image
    {
        get
        {
            return prefabs.transform.GetChild(0).gameObject ? prefabs.transform.GetChild(0).gameObject : null;
        }
    }
}