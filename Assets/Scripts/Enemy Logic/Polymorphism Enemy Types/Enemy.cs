﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, EnemyInterface
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] public GameObject followThis;
    [SerializeField] public float enemySpeed;

    // Default Movement - Can be overridden for more complex movement
    public void FollowPlayer()
    {
        // Get orientation point to follow and look at ball so that we can always determine where to go
        this.rb.transform.position = Vector3.MoveTowards(this.rb.transform.position, followThis.transform.position, enemySpeed);
        Vector3 targetPostition = new Vector3(  followThis.transform.position.x,
                                                this.rb.transform.position.y,
                                                followThis.transform.position.z
                                              );
        this.rb.transform.LookAt(targetPostition);
    }

    //Default attack
    public void AttackPlayer()
    {

    }
}
