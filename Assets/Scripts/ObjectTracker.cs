using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public GameObject goTrackingObject;
    public GameObject goIndicator;
    public Vector3 v3AverageVelocity;
    public Vector3 v3AverageAcceleration;

    private Vector3 v3PrevVel;
    private Vector3 v3PrevAccel;
    private Vector3 v3PrevPos;

    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();

        Vector3 v3Velocity = (goTrackingObject.transform.position - v3PrevPos) / Time.deltaTime;
        Vector3 v3Accel = v3Velocity - v3PrevVel;

        v3AverageVelocity = v3Velocity;
        v3AverageAcceleration = v3Accel;

        GetProjectedPosition(1);

        v3PrevPos = goTrackingObject.transform.position;
        v3PrevVel = v3Velocity;
        v3PrevAccel = v3Accel;


    }

    public Vector3 GetProjectedPosition(float fTime)
    {
        Vector3 v3Ret = new Vector3();

        //X0 + v0 * t + 1/2 a t^2
        v3Ret = goTrackingObject.transform.position + (v3AverageVelocity * Time.deltaTime * (fTime / Time.deltaTime)) + (0.5f * v3AverageAcceleration * Time.deltaTime * Mathf.Pow(fTime / Time.deltaTime, 2));
        goIndicator.transform.position = v3Ret;

        return v3Ret;
    }

}
