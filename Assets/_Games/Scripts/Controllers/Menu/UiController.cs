using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{

    public static UiController InstanceUiController;

    private void Awake()
    {
        InstanceUiController = this;
    }

    [SerializeField] private TextMeshProUGUI aliveCount;
    [SerializeField] private RectTransform popup;
    [SerializeField] private TextMeshProUGUI titlePopup;
    [SerializeField] private TextMeshProUGUI scorePopup;

    private int countAlive;
    private int aliveNumber = 0;
    private GameManager gameManager;
    private PlayerController playerController;

    public void OnInit()
    {
        gameManager = GameManager.Ins;
        playerController = PlayerController.InsPlayer;
        countAlive = gameManager.listEnemy.Count;
        if(aliveCount != null) aliveCount.text = countAlive.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        countAlive = gameManager.listEnemy.Count;
        if (aliveNumber != countAlive)
        {
            aliveNumber = countAlive;
            if (aliveCount != null) aliveCount.text = countAlive.ToString();
        }

        if (playerController.isDeath)
        {
            playerController.isVictory = false;
            ShowPopup();
            return;
        }

        if (countAlive == 0)
        {
            playerController.isVictory = true;
            ShowPopup();
            return;
        }
    }

    private void ShowPopup()
    {
        popup.gameObject.SetActive(true);
        scorePopup.text = playerController.coin.ToString();

        if (playerController.isDeath)
        {
            titlePopup.text = Constant.YOU_DIE;
            titlePopup.color = Color.red;

        } else if (playerController.isVictory)
        {
            titlePopup.text = Constant.YOU_WIN;
        }
    }

    public void BackToHome()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
