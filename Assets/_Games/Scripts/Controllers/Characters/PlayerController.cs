using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : Character
{
    public static PlayerController InsPlayer;

    [Header("Player property")]
    [SerializeField] private Transform attackZone;
    [SerializeField] private GameObject circleEnemy;
    [SerializeField] private Transform playerBody;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private GameObject prefabsPlusPoint;
    [SerializeField] private TextMeshPro textPoint;

    private WeaponId idWeaponEquip;
    private PantId idPantEquip;
    private GameObject circleTarget;

    [HideInInspector] public int coin = 0;
    [HideInInspector] public string curentTag;

    private int pointPlayer = 0;

    private void Awake()
    {
        InsPlayer = this;
    }

    void Update()
    {
        if (isVictory)
        {
            curentState.OnExit(this);
            ChangeAnimByBool(Constant.IS_DANCE);
            return;
        }

        if (isDeath)
        {
            return;
        }

        movingDirection = new Vector3(JoystickControl.direct.x, 0f, JoystickControl.direct.z);
        if(movingDirection != Vector3.zero)
        {
            isMoving = true;
            if (curentState != runState)
            {
                ChangeState(runState);
            }
        } else
        {
            isMoving = false;
            if(curentState == runState)
            {
                curentState.OnExit(this);
            }
            if (!playerBody.gameObject.CompareTag(Constant.TAG_INVISIBLE))
            {
                if (listTarget.Count > 0)
                {
                    targetAttack = listTarget[0].transform;
                    if (enemyTarget != targetAttack)
                    {
                        enemyTarget = targetAttack.GetComponent<Character>();
                    }

                    PushTargetToEnemy(targetAttack);
                    if (curentState != attackState)
                    {
                        ChangeState(attackState);
                    }
                }
                else
                {
                    if (curentState == attackState)
                    {
                        RemoveTargetToEnemy();
                        curentState.OnExit(this);
                    }

                }

            }
        }

        if (enemyTarget != null)
        {
            if (enemyTarget.isDeath)
            {
                point += (enemyTarget.point == 0 ? 1 : enemyTarget.point);
                ChangeState(idleState);
                listTarget.Remove(targetAttack.gameObject);
                RemoveTargetToEnemy();
                targetAttack = null;
                enemyTarget = null;
            }
        }

        if (curentState != null)
        {
            curentState.OnExcute(this);
        }
    }

    private void ApplyEquipment()
    {
        ChangeWeapon(idWeaponEquip);
        ChangePant(idPantEquip);
        ChangeHat(HeadId.HEAD_1);
        ChangeSkinColor(Color.white);
        ChangePropertyWithWeapon(gameObject, idWeaponEquip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            GameObject gameObjectParent = other.gameObject.transform.parent.gameObject;
            if (!listTarget.Contains(gameObjectParent))
            {
                if (other.gameObject.CompareTag(Constant.TAG_ENEMY_COLIDER))
                {
                    listTarget.Add(gameObjectParent);
                }
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            GameObject gameObjectParent = other.gameObject.transform.parent.gameObject;
            if (listTarget.Contains(gameObjectParent))
            {
                RemoveTargetToEnemy();
                if (enemyTarget != null)
                {
                    enemyTarget = null;
                }
                listTarget.Remove(gameObjectParent);
            }
        }
    }

    public void ChangeScaleAttackZone()
    {
        SphereCollider colliderChar = gameObject.transform.GetComponent<SphereCollider>();
        float scaleCircle = (colliderChar.radius / Constant.CIRCLE_ONE_UNIT) * 2;
        attackZone.localScale = new Vector3(scaleCircle, scaleCircle, 1);
    }

    private int GetDataInPlayerPreFabs(string key)
    {
        int value = Constant.ID_DFAULT;
        if(PlayerPrefs.HasKey(key))
        {
            if(PlayerPrefs.GetInt(key) != 0)
            {
                value = PlayerPrefs.GetInt(key);
            }
        }
        return value;
    }

    public override void OnInit()
    {
        base.OnInit();
        AtiveInvisible();
        idWeaponEquip = (WeaponId)GetDataInPlayerPreFabs(Constant.KEY_WEAPON);
        idPantEquip = (PantId)GetDataInPlayerPreFabs(Constant.KEY_PANT);
        ApplyEquipment();
        ChangeScaleAttackZone();
        //textCoin.text = coin.ToString();
    }

    public override void Moving(Vector3 movingDirection, bool isPlayer)
    {
        base.Moving(movingDirection, isPlayer);
    }

    private void PushTargetToEnemy(Transform enemy)
    {
        if(circleTarget == null)
        {
            circleTarget = Instantiate(circleEnemy, enemy);
        }
    }

    private void RemoveTargetToEnemy()
    {
        if(circleTarget != null)
        {
            Destroy(circleTarget);
        }
    }

    public void SetCoin(int coin)
    {
        textCoin.text = coin.ToString();
        ShowAnimPlusPoint(coin);
    }

    private void ShowAnimPlusPoint(int point)
    {
        GameObject animPlus = Instantiate(prefabsPlusPoint, gameObject.transform);
        if (textPoint == null)
        {
            textPoint = animPlus.GetComponent<TextMeshPro>();
        }
        textPoint.text = "+" + point.ToString();
    }

    private void AtiveInvisible()
    {
        playerBody.gameObject.tag = Constant.TAG_INVISIBLE;
        curentTag = Constant.TAG_INVISIBLE;
    }

    public void DeAtiveInvisble()
    {
        playerBody.gameObject.tag = Constant.TAG_PLAYER_COLIDER;
        curentTag = Constant.TAG_PLAYER_COLIDER;
    }
}
