using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerment : MonoBehaviour
{
    [SerializeField] private GameObject canvasMenu;
    [SerializeField] private GameObject canvasGame;
    [SerializeField] private GameObject menuHome;
    [SerializeField] private GameObject menuChangeEquip;
    [SerializeField] private GameObject menuSetting;
    [SerializeField] private GameObject apllyButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject settingButton;
    [SerializeField] private GameObject changeEquipButton;


    public void ApplyButton()
    {
        BackToHome();
        SaveChange();
        CameraController.Ins.CameraInMenuHome();
    }

    public void PlayButton()
    {
        canvasGame.SetActive(true);
        canvasMenu.SetActive(false);
        CameraController.Ins.CameraInGamePlay();
        UiController.InstanceUiController.OnInit();
        PlayerController.InsPlayer.DeAtiveInvisble();
    }

    public void HomeButton()
    {
        canvasMenu.SetActive(true);
        canvasGame.SetActive(false);
        CameraController.Ins.CameraInMenuHome();
    }

    public void BackToHome()
    {
        menuChangeEquip.SetActive(false);
        menuSetting.SetActive(false);
        backButton.SetActive(false);
        apllyButton.SetActive(false);
        menuHome.SetActive(true);
        playButton.SetActive(true);
        changeEquipButton.SetActive(true);
        settingButton.SetActive(true);
        CameraController.Ins.CameraInMenuHome();
    }

    public void SettingButton()
    {
        menuChangeEquip.SetActive(false);
        menuSetting.SetActive(true);
        backButton.SetActive(true);
        apllyButton.SetActive(true);
        menuHome.SetActive(false);
        playButton.SetActive(false);
        changeEquipButton.SetActive(false);
        settingButton.SetActive(false);

    }

    public void ChangeEquipButton()
    {
        menuChangeEquip.SetActive(true);
        menuSetting.SetActive(false);
        backButton.SetActive(true);
        apllyButton.SetActive(true);
        menuHome.SetActive(false);
        playButton.SetActive(false);
        changeEquipButton.SetActive(false);
        settingButton.SetActive(false);
        ChangeEquipment.Ins.ListWeapon();
        CameraController.Ins.CameraInMenuChangeEquip();

    }

    public void SaveChange()
    {
        EquipOption equipOption = EquipOption.equipOption;
        PlayerPrefs.SetInt(Constant.KEY_WEAPON, (int)equipOption.LastIdWeapon);
        PlayerPrefs.SetInt(Constant.KEY_PANT, (int)equipOption.LastIdPant);
    }
}
