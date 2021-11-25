using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]

public class Enemy : Interactable
{
    public GameMang playerManager;
    CharacterStats myStats;
    private void Start()
    {

        playerManager = GameMang.instance;
        myStats = GetComponent<CharacterStats>();

    }

    public override void Interact()
    {
        base.Interact();


        CharacterCombat PlayerCombat = playerManager.Player.GetComponent<CharacterCombat>();
        //attack
        if (PlayerCombat != null)
        {
            PlayerCombat.Attack(myStats);
            Debug.Log(myStats.ToString());
            Debug.Log(" : Attacking");
        }
    }

}
