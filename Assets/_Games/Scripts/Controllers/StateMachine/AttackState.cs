using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IStateMachine
{

    private float timeToAttack = 0f;
    private float timeAttackRandom = 0;

    public void OnEnter(Character character)
    {
        character.isAttack = true; 
        timeAttackRandom = Random.Range(1f, 2f);
    }

    public void OnExcute(Character character)
    {
        if(character.isAttack)
        {
            timeToAttack += Time.deltaTime;

            if(timeToAttack > 1f)
            {
                character.ChangeAnimByBool(Constant.IS_IDLE);
            }

            if(timeToAttack > timeAttackRandom)
            {
                timeAttackRandom = Random.Range(1f, 2f);
                character.Attack();
                timeToAttack = 0f;
            }

        }
    }

    public void OnExit(Character character)
    {
        character.isAttack = false;
        character.ChangeAnimByBool(Constant.IS_IDLE);
    }
}
