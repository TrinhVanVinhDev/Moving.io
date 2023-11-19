using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEquipment : UICanvas
{

    [SerializeField] private EquipmentDataBase equipmentData;
    [SerializeField] private Transform contentItem;
    [SerializeField] private GameObject itemLock;
    [SerializeField] private GameObject itemUnlock;

    private GameObject itemOption;
    private List<GameObject> listItem = new List<GameObject>();

    public void Awake()
    {
        CreateListItem(EquiqType.WEAPON);
        CameraController.Ins.CameraInMenuChangeEquip();
    }

    public void WeaponButton()
    {
        CreateListItem(EquiqType.WEAPON);
    }

    public void PantButton()
    {
        CreateListItem(EquiqType.PANT);
    }

    public void HeadButton()
    {
        CreateListItem(EquiqType.HEAD);
    }

    public void OnApply()
    {
        SaveChange();
        UIManager.Ins.OpenUI<UIHome>();
        Close(0);
    }

    public void ButtonBack()
    {
        UIManager.Ins.OpenUI<UIHome>();
        Close(0);
    }

    public void CreateListItem(EquiqType equiqType)
    {
        DestroyAllChild();
        switch (equiqType)
        {
            case EquiqType.WEAPON:
                CreateWeapon();
                break;
            case EquiqType.PANT:
                CreatePant();
                break;
            case EquiqType.HEAD:
                CreateHead();
                break;
            default:
                CreateWeapon();
                break;
        }
    }

    public void CreateWeapon()
    {
        for (int i = 0; i < equipmentData.weaponCount; i++)
        {
            Weapon weaponData = equipmentData.GetWeaponById(i + 1);
            if (!weaponData.isOwner)
            {
                itemOption = Instantiate(itemLock, contentItem);
                EquipLock weaponLock = itemOption.GetComponent<EquipLock>();
                weaponLock.SetPropertyWeaponLock(weaponData);
                weaponLock.Oninit();
            }
            else
            {
                itemOption = Instantiate(itemUnlock, contentItem);
                EquipOption weaponComponent = itemOption.GetComponent<EquipOption>();
                weaponComponent.SetPropertyWeapon(weaponData);
                weaponComponent.Oninit();
            }

            listItem.Add(itemOption);
        }
    }

    public void CreatePant()
    {
        for (int i = 0; i < equipmentData.pantCount; i++)
        {
            Skin pantData = equipmentData.GetPantById(i + 1);
            if(!pantData.isOwner)
            {
                itemOption = Instantiate(itemLock, contentItem);
                EquipLock pantComponent = itemOption.GetComponent<EquipLock>();
                pantComponent.SetPropertyPantLock(pantData);
            } else
            {
                itemOption = Instantiate(itemUnlock, contentItem);
                EquipOption pantComponent = itemOption.GetComponent<EquipOption>();
                pantComponent.SetPropertyPant(pantData);
                pantComponent.Oninit();
            }
            listItem.Add(itemOption);
        }
    }

    public void CreateHead()
    {
        for (int i = 0; i < equipmentData.headCount; i++)
        {
            itemOption = Instantiate(itemUnlock, contentItem);
            listItem.Add(itemOption);
            Skin headData = equipmentData.GetHeadById(i + 1);
            EquipOption headComponent = itemOption.GetComponent<EquipOption>();
            headComponent.SetPropertyHead(headData);
            headComponent.Oninit();
        }
    }

    private void DestroyAllChild()
    {
        if (listItem.Count > 0) listItem.Clear();
        foreach (Transform child in contentItem)
        {
            Destroy(child.gameObject);
        }
    }

    public void SaveChange()
    {
        EquipOption equipOption = EquipOption.equipOption;
        int idWeapon = (int)equipOption.LastIdWeapon;
        int idPant = (int)equipOption.LastIdPant;
        equipmentData.RemoveEquipWeapon();
        equipmentData.AddEquipWeapon(idWeapon);
        PlayerPrefs.SetInt(Constant.KEY_WEAPON, idWeapon);
        PlayerPrefs.SetInt(Constant.KEY_PANT, idPant);
    }

}
