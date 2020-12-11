using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followOrient : MonoBehaviour
{
    [SerializeField] private Transform orient;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = player.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(this.transform.position, desiredPosition, ref velocity, 0f);
        this.transform.position = smoothedPosition;
    }
}
