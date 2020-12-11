using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Transform followThis;
    [SerializeField] private Camera cam;

    // Following Character
    [SerializeField] public Vector3 offset;
    [SerializeField] public Vector3 savedOffset;
    [SerializeField] private Vector3 velocity = Vector3.zero;

    // Mouse Look Settings
    [SerializeField] private Vector3 previousPosition;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float smoothing;
    [SerializeField] private bool invertMouse; 
    [SerializeField] public bool groundCheck = false;

    // Zoom Settings
    [SerializeField] private float minScrollIn;
    [SerializeField] private float maxScrollOut;
    
    [SerializeField] private Vector3 targetPosition;    
    [SerializeField] private Vector2 currentLookingPos;

    // Start is called before the first frame update
    void Awake()
    {
        followThis = GameObject.FindWithTag("Player").transform;//Gets Core of Player

        //Original Camera preset
        offset = new Vector3(0, 2, -5);

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);

        // Locks in cursor to game scene
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void LateUpdate()
    {
        FollowPlayer();
        //RotateCamera();
        ZoomCamera();
    }

    private void FollowPlayer()
    { 
        Vector3 desiredPosition = followThis.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothing);
        transform.position = smoothedPosition;

        transform.LookAt(followThis); //Keep player in view 
    }
    
    private void FollowPlayerPan()
    {
        Vector3 desiredPosition = followThis.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothing + 1.0f);
        transform.position = smoothedPosition;

        transform.LookAt(followThis); //Keep player in view
    }
    
    private void ZoomCamera()
    {
        float mouseScroll = Input.mouseScrollDelta.y;

        // Check for Zooming
        if (mouseScroll != 0)
        {
            // Reversed because smaller number is actually largest in negative numbers
            mouseScroll = Mathf.Clamp((offset.z + mouseScroll), -maxScrollOut, -minScrollIn); 
            offset = new Vector3(offset.x, offset.y, mouseScroll);
        }
    } 

    private void RotateCamera()
    {
        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        cam.transform.position = new Vector3();

        cam.transform.Rotate(Vector3.left, angle: direction.y * 180);
        cam.transform.Rotate(Vector3.up, angle: -direction.x * 180, relativeTo: Space.World);
        cam.transform.Translate(new Vector3(0,0,-10));


        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
    //private void RotateCamera()
    //{
    //    Vector2 axisInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));


    //    // Scale the inputs with mouse sensitivity and smoothing
    //    axisInput = Vector2.Scale(axisInput, new Vector2(mouseSensitivity * smoothing, mouseSensitivity * smoothing));

    //    // Smooth transition to mouse look coords
    //    smoothVelocity.x = Mathf.Lerp(smoothVelocity.x, axisInput.x, 1f / smoothing);
    //    smoothVelocity.y = Mathf.Lerp(smoothVelocity.y, axisInput.y, 1f / smoothing);

    //    currentLookingPos += smoothVelocity;

    //    // Clamps view to 180 degrees look up and down
    //    currentLookingPos.y = Mathf.Clamp(currentLookingPos.y, -80f, 80f);

    //    // Implement look
    //    transform.localRotation = Quaternion.AngleAxis(-currentLookingPos.y, Vector3.right);
    //    transform.localRotation = Quaternion.AngleAxis(currentLookingPos.x, followThis.transform.up);

    //}
}
