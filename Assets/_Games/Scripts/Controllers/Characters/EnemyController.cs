using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    public static EnemyController InsEnemy;

    private void Awake()
    {
        InsEnemy = this;
    }
    private float timerWaitAttack;

    private Vector3 offSetremoveY;

    public override void OnInit()
    {
        base.OnInit();
        movingDirection = Vector3.zero;
        originPosition = transform.position;
        ApplyEquipment();
    }

    public void UpdateEnemyControl()
    {
        if (isNotPlaying) return;

        if (isMoving)
        {
            ChangeState(runState);
        } else
        {
            offSetremoveY = new Vector3(transform.position.x, 0, transform.position.z);
            originPosition = new Vector3(originPosition.x, 0f, originPosition.z);
            if (Vector3.Distance(originPosition, offSetremoveY) < 0.2f)
            {
                if (isAttack)
                {
                    if (listTarget.Count > 0)
                    {
                        targetAttack = listTarget[0].transform;
                        if (targetAttack != null)
                        {
                            enemyTarget = targetAttack.GetComponent<Character>();
                        }
                        if (curentState != attackState)
                        {
                            ChangeState(attackState);
                        }
                    }
                    else
                    {
                        if (curentState == attackState)
                        {
                            ChangeState(idleState);
                        }
                    }
                }
                else
                {
                    if (curentState == runState)
                    {
                        ChangeState(idleState);
                    }
                }
            }
        }

        if(targetAttack != null)
        {
            GetComponentEnemy();
            if (enemyTarget.isDeath)
            {
                ChangeState(idleState);
                listTarget.Remove(targetAttack.gameObject);
                targetAttack = null;
            }
        }

        if (curentState != null)
        {
            curentState.OnExcute(this);
        }
    }

    private void ApplyEquipment()
    {
        Color skinColor = Random.ColorHSV();
        WeaponId idWe = RandomCreateWeapon();
        PantId idPant = RandomCreatePant();
        HeadId idHead = RandomCreateHead();
        ChangeWeapon(idWe);
        ChangePant(idPant);
        ChangeHat(idHead);
        ChangeSkinColor(skinColor);
        ChangePropertyWithWeapon(gameObject, idWe);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
        {
            GameObject gameObjectParent = other.gameObject.transform.parent.gameObject;
            if (!listTarget.Contains(gameObjectParent))
            {
                if ((other.gameObject.CompareTag(Constant.TAG_ENEMY_COLIDER) ||
                    other.gameObject.CompareTag(Constant.TAG_PLAYER_COLIDER))
                    )
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
                if(enemyTarget != null)
                {
                    enemyTarget = null;
                }
                listTarget.Remove(gameObjectParent);
            }
        }
    }

    public override void Moving(Vector3 movingDirection, bool isPlayer)
    {
        base.Moving(movingDirection, isPlayer);
    }
}
