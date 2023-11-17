using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipOption : MonoBehaviour
{

    public static EquipOption equipOption;

    private void Awake()
    {
        equipOption = this;
    }

    private EquiqType isEquip;
    public EquiqType IsEquip => isEquip;
    private WeaponId idWeapon;
    public WeaponId IdWeapon => idWeapon;
    private PantId idPant;
    public PantId IdPant => idPant;
    private HeadId idHead;
    public HeadId IdHead => idHead;
    private WeaponId lastIdWeapon;
    public WeaponId LastIdWeapon => lastIdWeapon;
    private PantId lastIdPant;
    public PantId LastIdPant => lastIdPant;
    private HeadId lastHead;
    public HeadId LastHead => lastHead;
    private string nameItem;
    public string NameItem => nameItem;
    private Sprite icon;
    public Sprite Icon => icon;
    private Material skinMaterial;
    public Material SkinMaterial => skinMaterial;

    [SerializeField] private GameObject nameObjetc;
    [SerializeField] private GameObject image;

    private PlayerController player;

    // Start is called before the first frame update
    public void Oninit()
    {
        nameObjetc.GetComponent<TextMeshProUGUI>().text = nameItem;
        image.GetComponent<Image>().sprite = icon;
        player = PlayerController.InsPlayer;
        lastIdWeapon = WeaponId.HAMMER;
        lastIdPant = PantId.PANT_1;
    }

    public void SetWeaponToCharater()
    {
        player.ChangeWeapon(IdWeapon);
        player.ChangePropertyWithWeapon(player.gameObject, IdWeapon);
        player.ChangeScaleAttackZone();
        lastIdWeapon = IdWeapon;
    }

    public void SetHeadToCharater()
    {
        player.ChangeHat(IdHead);
        lastHead = IdHead;
    }

    public void SetPantToCharater()
    {
        player.ChangePant(idPant);
        lastIdPant = idPant;
    }

    public void EquipButton()
    {
        if (isEquip == EquiqType.WEAPON) SetWeaponToCharater();
        if (isEquip == EquiqType.PANT) SetPantToCharater();
        if (isEquip == EquiqType.HEAD) SetHeadToCharater();
    }

    public void SetPropertyWeapon(Weapon weaponData)
    {
        nameItem = weaponData.name;
        icon = weaponData.icon;
        idWeapon = (WeaponId)weaponData.id;
        isEquip = EquiqType.WEAPON;
    }

    public void SetPropertyPant(Skin pantData)
    {
        nameItem = pantData.skinName;
        skinMaterial = pantData.skinMaterial;
        idPant = (PantId)pantData.idSkin;
        isEquip = EquiqType.PANT;
    }

    public void SetPropertyHead(Skin headData)
    {
        nameItem = headData.skinName;
        skinMaterial = headData.skinMaterial;
        idHead = (HeadId)headData.idSkin;
        isEquip = EquiqType.HEAD;
    }
}
