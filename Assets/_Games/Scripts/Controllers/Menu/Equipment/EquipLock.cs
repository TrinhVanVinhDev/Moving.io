using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EquipLock : MonoBehaviour
{
    private int price;
    public int Price => price;
    private int idItem;
    public int IdItem => idItem;
    private EquiqType equiqType;
    public EquiqType EquiqType => equiqType;

    [SerializeField] private TextMeshProUGUI priceItem;
    [SerializeField] private EquipmentDataBase equipmentData;
    [SerializeField] private Transform itemUnlock;
    [SerializeField] private Transform uiEquipment;


    public void Oninit()
    {
        priceItem.GetComponent<TextMeshProUGUI>().text = price.ToString();
    }

    public void UnlockItem()
    {
        equipmentData.BuyWeapon(idItem);
        UIEquipment equipScript = uiEquipment.GetComponent<UIEquipment>();
        equipScript.WeaponButton();
    }

    public void SetPropertyWeaponLock(Weapon lockData)
    {
        price = lockData.price;
        idItem = lockData.id;
        equiqType = EquiqType.WEAPON;
    }

    public void SetPropertyPantLock(Skin skin)
    {
        price = skin.price;
        idItem = skin.idSkin;
        equiqType = EquiqType.PANT;
    }
}
