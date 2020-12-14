using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanoid : Enemy
{
    //[SerializeField] private Rigidbody rb;
    //[SerializeField] private GameObject followThis;

    //[SerializeField] private float enemySpeed = 0.09f;



    // Start is called before the first frame update
    void Awake()
    {
        this.rb = transform.GetChild(0).GetComponent<Rigidbody>();
        followThis = GameObject.Find("Core");
        enemySpeed = 0.09f;
        attackRange = 5f;
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
