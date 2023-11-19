using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private RectTransform canvasRT;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject arrowIndicator;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private RectTransform panelIndicator;
    private SpawnObject spawnObject;
    private float timeToDeath = 0;

    [HideInInspector] public int listEnemyCount;
    public List<EnemyController> listEnemy = new List<EnemyController>();
    public List<GameObject> listIndicator = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {

        if(!canvasRT.gameObject.activeSelf && !Character.isNotPlaying)
        {
            canvasRT.gameObject.SetActive(true);
        }

        listEnemyCount = listEnemy.Count;

        for (int i = listEnemyCount - 1; i >= 0; i--)
        {
            if (!listEnemy[i].gameObject.activeSelf)
            {
                listEnemy.RemoveAt(i);
                listIndicator.RemoveAt(i);
                continue;
            }

            if (!listEnemy[i].isDeath)
            {
                listEnemy[i].UpdateEnemyControl();
                if (panelIndicator.gameObject.activeSelf)
                {
                    IndicatorController indicator = listIndicator[i].GetComponent<IndicatorController>();
                    indicator.TargetPosition = listEnemy[i].transform.position;
                    indicator.PlayerTransform = playerTransform;
                    indicator.ChangePositionTarget(canvasRT);
                }
            }
            else
            {
                DeactiveCharater(listEnemy[i].gameObject);
                listIndicator[i].SetActive(false);
            }
        }
    }

    public void OnInit()
    {
        spawnObject = SpawnObject.Ins;
        playerController.OnInit();
        spawnObject.OnInit();
        OnSpawnEnemy();
        OnInitEnemy();
        CameraController.Ins.CameraInMenuHome();
    }

    private void RunningUpdateEnemy() { }

    private void OnSpawnEnemy()
    {
        for (int i = 0; i < spawnObject.sizePool; i++)
        {
            GameObject enemyObjetc = spawnObject.OnSpawnObject("enemy");
            listEnemy.Add(enemyObjetc.GetComponent<EnemyController>());
        }
    }

    private void OnInitEnemy()
    {
        OnSpawnIndicator();
        for (int i = 0; i < spawnObject.sizePool; i++)
        {
            listEnemy[i].OnInit();
            if (listIndicator[i] != null)
            {
                listIndicator[i].GetComponent<IndicatorController>().OnInit();
            }
        }
    }
    private void OnSpawnIndicator()
    {
        for (int i = 0; i < listEnemy.Count; i++)
        {
            GameObject indicatorSpawn = Instantiate(arrowIndicator, canvasRT);
            listIndicator.Add(indicatorSpawn);
        }
    }

    public void DeactiveCharater(GameObject character)
    {
        timeToDeath += Time.deltaTime;
        if (timeToDeath > 1.5f)
        {
            character.SetActive(false);
            timeToDeath = 0;
        }
    }
}
