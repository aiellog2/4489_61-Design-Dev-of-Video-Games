using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Force : MonoBehaviour
{

    [SerializeField] private CharacterController controller;
    [SerializeField] private float slowDown = 0.3f;
    [SerializeField] private NavMeshAgent agent;

    private Vector3 slowVelocity;
    private Vector3 hit;

    private float verticalVelocity;

    public Vector3 Movement => hit + Vector3.up * verticalVelocity; 

    private void Update()
    {
        if (verticalVelocity < 0f && controller.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        hit = Vector3.SmoothDamp(hit, Vector3.zero, ref slowVelocity, slowDown);

        if (agent != null)
        {
            if (hit.sqrMagnitude < 0.2f * 0.2f)
            {
                hit = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void Jump(float jumpForce)
    {
        verticalVelocity += jumpForce;
    }

    public void AddForce(Vector3 force)
    {
        hit += force;
        if(agent != null)
        {
            agent.enabled = false;
        }
    }
}
