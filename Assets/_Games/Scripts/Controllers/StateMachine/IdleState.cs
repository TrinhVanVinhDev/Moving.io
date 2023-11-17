using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IStateMachine
{
    float randomTime;
    float timer;
    public const int STATE_NAME = 0;

    public void OnEnter(Character character)
    {
        character.StopMoving();
        timer = 0;
        randomTime = Random.Range(3f, 6f);
    }

    public void OnExcute(Character character)
    {
        timer += Time.deltaTime;
        if (character.listTarget.Count > 0)
        {
            character.isMoving = false;
            character.isAttack = true;
        }
        else
        {
            if (timer > randomTime)
            {
                timer = 0f;
                character.isMoving = true;
                character.isAttack = false;
            }
        }
    }

    public void OnExit(Character character)
    {
        //throw new System.NotImplementedException();
    }
}
