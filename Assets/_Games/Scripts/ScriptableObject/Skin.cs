using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skin
{
    public string skinName;
    public GameObject skinPrefabs;
    public Material skinMaterial;
    public int idSkin;
    public int typeEquip;
    public bool isOwner;
    public bool isPlayerEquip;
    public int price;
}