using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float WalkStopPoint = 0.2f;
    [SerializeField] float AttackStopPoint = 0.8f;
    CameraRaycaster CameraRaycaster;
    private Vector3 CurrentDestination,clickPoint;
    bool isInDrectionMode = false;
    ThirdPersonCharacter m_thirdPersonCharacter;
    void Start()
    {
        CameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        CurrentDestination = transform.position;
        m_thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G))
        {
            isInDrectionMode = !isInDrectionMode;
            CurrentDestination = transform.position; //Clear the click target
        }
        if (isInDrectionMode)
        {
            ProcessDirectionMovement();
        }
        else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectionMovement()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;
        m_thirdPersonCharacter.Move(m_Move, false, false);

    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            clickPoint = CameraRaycaster.hit.point;
            switch (CameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    CurrentDestination = ShortDestination(clickPoint,WalkStopPoint);
                    break;
                case Layer.Enemy:
                    CurrentDestination = ShortDestination(clickPoint, AttackStopPoint);
                    break;

            }
        }
        WalkToDestination();
    }

    private void WalkToDestination()
    {
        var postion = CurrentDestination - transform.position;
        if (Vector3.Distance(CurrentDestination, transform.position) > 0.1f)
        {
            m_thirdPersonCharacter.Move(postion, false, false);
        }
        else
        {
            m_thirdPersonCharacter.Move(Vector3.zero, false, false);

        }
    }
    //calculation stop distance
    private Vector3 ShortDestination(Vector3 destination,float shortering)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortering;
        return destination - reductionVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, CurrentDestination);
        Gizmos.DrawSphere(CurrentDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.1f);
    }
}
