using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    private GameObject parentCollider;
    private int pointAfterKill;
    public Transform characterSelf;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckBodyCharCollider(other))
        {
            if (characterSelf.parent != other.transform.parent)
            {
                parentCollider = other.gameObject.transform.parent.gameObject;
                if (other.gameObject.CompareTag(Constant.TAG_ENEMY_COLIDER))
                {
                    KillChar();
                    ChangeSizeWhenCharDeath(Constant.TAG_ENEMY_COLIDER);
                }
                else if (other.gameObject.CompareTag(Constant.TAG_PLAYER_COLIDER))
                {
                    KillChar();
                    ChangeSizeWhenCharDeath(Constant.TAG_PLAYER_COLIDER);
                }

                //if(characterSelf.CompareTag(Constant.TAG_PLAYER_COLIDER)
                //    && other.CompareTag(Constant.TAG_ENEMY_COLIDER)
                //)
                //{
                //    PlusPointInPlayer();
                //}
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == characterSelf.parent)
        {
            Character componentChar = other.GetComponent<Character>();
            componentChar.DeactiveWeaponAttack(gameObject);
        }
    }

    private void KillChar()
    {
        if(parentCollider != null)
        {
            Character charaCollider = parentCollider.GetComponent<Character>();
            Character charaSelf = characterSelf.parent.GetComponent<Character>();
            if(charaCollider.point <= 0)
            {
                charaSelf.point += 1;
            } else
            {
                charaSelf.point += charaCollider.point;
            }

            if(characterSelf.CompareTag(Constant.TAG_PLAYER_COLIDER))
            {
                PlusPointInPlayer(charaSelf.point);
            }
            charaCollider.ChangeState(new DeathState());
        }
    }

    private void ChangeSizeWhenCharDeath(string tag)
    {
        if (String.Equals(tag, Constant.TAG_PLAYER_COLIDER))
        {
            EnemyController enemyController = EnemyController.InsEnemy;
            enemyController.ChangeSizeCharater(characterSelf.transform);
        } else
        {
            PlayerController playerController = PlayerController.InsPlayer;
            playerController.ChangeSizeCharater(characterSelf.transform);
        }
    }

    private bool CheckBodyCharCollider(Collider collider)
    {
        return collider.transform.parent != null;
    }

    private void PlusPointInPlayer(int pointAfterKill)
    {
        PlayerController playerController = PlayerController.InsPlayer;
        playerController.SetCoin(pointAfterKill);
    }
}
