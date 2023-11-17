using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : IStateMachine
{
    public void OnEnter(Character character)
    {
        character.Death();
    }

    public void OnExcute(Character character)
    {
    }

    public void OnExit(Character character)
    {
        
    }
}
