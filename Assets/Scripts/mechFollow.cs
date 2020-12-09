using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechFollow : MonoBehaviour
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
        Vector3 loweredPlayerPos = player.transform.position;
        loweredPlayerPos.y = loweredPlayerPos.y - 0.5f;

        orient.transform.position = Vector3.MoveTowards(orient.transform.position, loweredPlayerPos, 100f);
    }
}
