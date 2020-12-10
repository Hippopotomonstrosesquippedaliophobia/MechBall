using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followOrient : MonoBehaviour
{
    [SerializeField] private Transform orient;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get orientation point to follow and look at ball so that we can always determine where to go
        orient.transform.position = Vector3.MoveTowards(orient.transform.position, cam.transform.position, 100f);
        Vector3 targetPostition = new Vector3(player.position.x,
                                        orient.transform.position.y,
                                        player.position.z);
        orient.transform.LookAt(targetPostition);
    }
}
