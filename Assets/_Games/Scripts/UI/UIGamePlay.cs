using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI aliveCountText;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = PlayerController.InsPlayer;
        playerController.DeAtiveInvisble();
        CameraController.Ins.CameraInGamePlay();
    }

    private void Update()
    {
        UpdateEnemyAlive();
        UpdatePlayerScore();
        if (gameObject.activeSelf && !String.Equals(playerController.curentTag, Constant.TAG_PLAYER_COLIDER))
        {
            playerController.curentTag = Constant.TAG_PLAYER_COLIDER;
        }

        if(playerController.isDeath)
        {
            UIManager.Ins.OpenUI<UILose>();
            Close(0);
        }
    }

    private void UpdateEnemyAlive()
    {
        int aliveCount = GameManager.Ins.listEnemyCount;
        aliveCountText.text = aliveCount.ToString();
    }

    private void UpdatePlayerScore()
    {
        int playerScore = PlayerController.InsPlayer.point;
        playerScoreText.text = playerScore.ToString();
    }
}
