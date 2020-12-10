using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion : Enemy
{
    //[SerializeField] private Rigidbody rb;
    //[SerializeField] private GameObject followThis;

    //[SerializeField] private float enemySpeed = 0.09f;



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

    // Overrides Normal attack - Melee
    new public void AttackPlayer()
    {
        //Todo
    }
}
