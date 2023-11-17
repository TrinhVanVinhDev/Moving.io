using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentDataBase", menuName = "gameConfiguration/EquipmentDataBase")]
public class EquipmentDataBase : ScriptableObject
{
    [SerializeField] private List<Weapon> weapon;
    [SerializeField] private List<Skin> head;
    [SerializeField] private List<Skin> pant;

    public int weaponCount
    {
        get
        {
            return weapon.Count;
        }
    }

    public int headCount
    {
        get
        {
            return head.Count;
        }
    }

    public int pantCount
    {
        get
        {
            return pant.Count;
        }
    }

    public Weapon GetWeaponById(int idWeapon)
    {
        Weapon weaponSelect = new Weapon();
        for (int i = 0; i < weaponCount; i++)
        {
            if (weapon[i].id == idWeapon)
            {
                weaponSelect = weapon[i];
                break;
            }
        }

        return weaponSelect;
    }

    public Weapon GetWeaponByIndex(int index)
    {
        Weapon weaponSelect = new Weapon();
        for (int i = 0; i < weaponCount; i++)
        {
            if (i == index)
            {
                weaponSelect = weapon[i];
                break;
            }
        }

        return weaponSelect;
    }

    public Skin GetHeadById(int idHead)
    {
        Skin headSelect = new Skin();
        for (int i = 0; i < headCount; i++)
        {
            if (head[i].idSkin == idHead)
            {
                headSelect = head[i];
                break;
            }
        }

        return headSelect;
    }

    public Skin GetHeadByIndex(int index)
    {
        Skin headSelect = new Skin();
        for (int i = 0; i < headCount; i++)
        {
            if (i == index)
            {
                headSelect = head[i];
                break;
            }
        }

        return headSelect;
    }

    public Skin GetPantById(int idPant)
    {
        Skin pantSelect = new Skin();
        for (int i = 0; i < weaponCount; i++)
        {
            if (pant[i].idSkin == idPant)
            {
                pantSelect = pant[i];
                break;
            }
        }

        return pantSelect;
    }

    public Skin GetPantByIndex(int index)
    {
        Skin pantSelect = new Skin();
        for (int i = 0; i < weaponCount; i++)
        {
            if (i == index)
            {
                pantSelect = pant[i];
                break;
            }
        }

        return pantSelect;
    }

    public Weapon BuyWeapon(int id)
    {
        Weapon weaponBuy = GetWeaponById(id);
        weaponBuy.isOwner = true;
        return weaponBuy;
    }

    public void RemoveEquipWeapon()
    {
        for(int i = 0; i < weaponCount; i++)
        {
            if(weapon[i].isPlayerEquip)
            {
                weapon[i].isPlayerEquip = false;
            } else
            {
                continue;
            }
        }
    }

    public void AddEquipWeapon(int idWeapon)
    {
        Weapon weaponSelect = GetWeaponById(idWeapon);
        weaponSelect.isPlayerEquip = true;
    }
}
