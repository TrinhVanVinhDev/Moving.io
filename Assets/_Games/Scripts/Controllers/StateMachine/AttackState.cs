using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IStateMachine
{

    private float timeToAttack = 0f;

    public void OnEnter(Character character)
    {
        character.isAttack = true;
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

            if(timeToAttack > Constant.TIME_FREEZE)
            {
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
