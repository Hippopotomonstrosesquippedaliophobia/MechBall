using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalBullet : Bullet
{
    // Start is called before the first frame update
    void Awake()
    {
        bulletSpeed = 50f;
        bulletDmg = 13f;
        lifetime = 3f; //3 seconds to exist
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
