using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public bool lockon;
    public float followSpeed = 5;
    public float mouseSpeed = 2;
    public float controllerSpeed = 7;

    public Transform target;
    public Transform pivot;
    public Transform camTrans;

    float turnSmoothing = .1f;
    public float minAngle = -75;
    public float maxAngle = 75;

    float smoothX;
    float smoothY;
    float smoothXVelocity;
    float smoothYVelocity;

    public float lookAngle;
    public float tiltAngle;

    // Start is called before the first frame update
    void Start()
    {
        camTrans = Camera.main.transform;
        pivot = camTrans.parent;

        Application.targetFrameRate = 500;
        QualitySettings.vSyncCount = 0;
    }

    void FollowTarget(float delta)
    {
        float speed = delta * followSpeed;
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
        transform.position = targetPosition;
    }

    void HandleRotation(float delta, float vertical, float horizontal, float targetSpeed)
    {
        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, horizontal, ref smoothXVelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, vertical, ref smoothYVelocity, turnSmoothing);
        }
        else
        {
            smoothX = horizontal;
            smoothY = vertical;
        }

        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

        tiltAngle -= smoothY * targetSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(tiltAngle, lookAngle, 0);
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse Y");
        float inputY = Input.GetAxis("Mouse X");

        FollowTarget(Time.deltaTime);
        HandleRotation(Time.deltaTime, inputX, inputY, controllerSpeed);
    }
}
