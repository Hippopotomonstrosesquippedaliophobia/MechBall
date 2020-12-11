using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brute : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        this.rb = transform.GetChild(0).GetComponent<Rigidbody>();
        followThis = GameObject.Find("Core");
        enemySpeed = 0.09f;
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
