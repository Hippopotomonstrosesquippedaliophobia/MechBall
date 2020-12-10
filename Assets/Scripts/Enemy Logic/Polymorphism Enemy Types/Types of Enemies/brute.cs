using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brute : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.GetComponent<Rigidbody>();
        this.followThis = GameObject.Find("Core");
        this.enemySpeed = 0.09f;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    // Overrides normal attack - Ranged
    new public void AttackPlayer()
    {
        //todo
    }
}
