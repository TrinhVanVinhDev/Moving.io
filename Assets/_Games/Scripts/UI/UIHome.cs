using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHome : UICanvas
{
    private PlayerController playerController;
    private void Awake()
    {
        playerController = PlayerController.InsPlayer;
    }

    private void Update()
    {
        if (gameObject.activeSelf && !String.Equals(playerController.curentTag, Constant.TAG_PLAYER_COLIDER))
        {
            playerController.curentTag = Constant.TAG_PLAYER_COLIDER;
        }
    }

    public void PlayButton()
    {
        UIManager uIManager = UIManager.Ins;
        uIManager.OpenUI<UIGamePlay>();
        uIManager.AddBackUI(this);
        Close(0);
        Character.PlayGame();
    }

    public void ChangeEquipButton()
    {
        UIManager uIManager = UIManager.Ins;
        UIManager.Ins.OpenUI<UIEquipment>();
        Close(0);
    }

    public void SettingButton()
    {
        UIManager uIManager = UIManager.Ins;
        UIManager.Ins.OpenUI<UISetting>();
        uIManager.AddBackUI(this);
        Close(0);
    }
}
