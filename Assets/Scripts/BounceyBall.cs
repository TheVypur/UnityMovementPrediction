using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceyBall : MonoBehaviour
{
    public float fGravity = -20f;
    public Vector3 v3MoveDirection;
    private CharacterController m_Controller;

    private void Start()
    {
        m_Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        v3MoveDirection.y += fGravity * Time.deltaTime;
        m_Controller.Move(v3MoveDirection * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        v3MoveDirection = Vector3.Reflect(v3MoveDirection, hit.normal);
    }

}
