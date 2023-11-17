using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : IStateMachine
{
    private PlayerController insPlayer;
    private bool isPlayer = false;

    public void OnEnter(Character character)
    {
        insPlayer = PlayerController.InsPlayer;
    }

    public void OnExcute(Character character)
    {
        if(character.isMoving)
        {
            if(character == insPlayer)
            {
                character.movingDirection = insPlayer.movingDirection;
                character.isMoving = false;
                isPlayer = true;
            } else
            {
                isPlayer = false;
            }
            character.Moving(character.movingDirection, isPlayer);
        }
    }

    public void OnExit(Character character)
    {
        character.StopMoving();
    }
}
