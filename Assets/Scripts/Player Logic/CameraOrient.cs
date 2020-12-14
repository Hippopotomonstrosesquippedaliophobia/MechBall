using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrient : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Player Camera").transform;
        player = GameObject.Find("Stationary Sphere").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = cam.position;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, 0f);
        transform.position = smoothedPosition;

        transform.LookAt(player.position);
        //transform.LookAt(player.position);
    }
}
