using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minion : Enemy
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

        hp = rb.transform.parent.GetComponent<Health>();
    }


    // Update is called once per frame
    void Update()
    {

        // Execute attack if player is near
        CloseToPlayer();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    // Overrides Normal attack - Melee
    new public void AttackPlayer()
    {
        Debug.Log("Minion attacked");
    }
}
