using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour, IAttackable
{
    public void Attack()
    {

    }

    private void AttackStarted()
    {
        Debug.Log(gameObject.name + " attack started");
    }

    private void AttackEnded()
    {
        Debug.Log(gameObject.name + " attack ended");
    }
}