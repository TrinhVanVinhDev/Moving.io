//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChangeEquipment : Singleton<ChangeEquipment>
//{
//    [SerializeField] private EquipmentDataBase equipmentData;
//    [SerializeField] private GameObject weaponOptionPrefab;
//    [SerializeField] private GameObject pantPrefabs;
//    [SerializeField] private GameObject lockPrefabs;
//    [SerializeField] private Transform listWeapon;
//    [SerializeField] private Transform listPant;
//    [SerializeField] private GameObject scrollWeapon;
//    [SerializeField] private GameObject scrollPant;

//    private GameObject equipOption;
//    private List<GameObject> listequipPant = new List<GameObject>();
//    private List<GameObject> listequipWeapon = new List<GameObject>();
//    private List<GameObject> listequipHat = new List<GameObject>();

//    public void ListWeapon()
//    {
//        if (listequipWeapon.Count > 0) return;
//        for (int i = 0; i < equipmentData.weaponCount; i++)
//        {
//            Weapon weaponData = equipmentData.GetWeaponById(i + 1);
//            //if(!weaponData.isOwner)
//            //{
//            //    equipOption = Instantiate(lockPrefabs, listWeapon);
//            //    EquipLock weaponLock = equipOption.GetComponent<EquipLock>();
//            //    weaponLock.SetPropertyWeaponLock(weaponData);
//            //    weaponLock.Oninit();
//            //} else
//            //{
//            equipOption = Instantiate(weaponOptionPrefab, listWeapon);
//            EquipOption weaponComponent = equipOption.GetComponent<EquipOption>();
//            weaponComponent.SetPropertyWeapon(weaponData);
//            weaponComponent.Oninit();
//            //}

//            listequipWeapon.Add(equipOption);
//        }
//    }


//    public void ListPant()
//    {
//        if (listequipPant.Count > 0) return;
//        for (int i = 0; i < equipmentData.pantCount; i++)
//        {
//            equipOption = Instantiate(pantPrefabs, listPant);
//            listequipPant.Add(equipOption);
//            Skin pantData = equipmentData.GetPantById(i + 1);
//            EquipOption pantComponent = equipOption.GetComponent<EquipOption>();
//            pantComponent.SetPropertyPant(pantData);
//            pantComponent.Oninit();
//        }

//    }

//    public void ListHat()
//    {
//        if (listequipHat.Count > 0) return;

//        for (int i = 0; i < equipmentData.headCount; i++)
//        {
//            equipOption = Instantiate(pantPrefabs, listPant);
//            listequipPant.Add(equipOption);
//            Skin pantData = equipmentData.GetPantById(i + 1);
//            EquipOption pantComponent = equipOption.GetComponent<EquipOption>();
//            pantComponent.SetPropertyPant(pantData);
//            pantComponent.Oninit();
//        }
//    }

//    public void ChangeListPant()
//    {
//        if(scrollWeapon.activeSelf)
//        {
//            scrollWeapon.SetActive(false);
//        }

//        scrollPant.SetActive(true);
//        ListPant();
//    }

//    public void ChangeListWeapon()
//    {
//        if (scrollPant.activeSelf)
//        {
//            scrollPant.SetActive(false);
//        }

//        scrollWeapon.SetActive(true);
//        ListWeapon();
//    }
//}
