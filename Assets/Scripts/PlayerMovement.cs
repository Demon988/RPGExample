using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float WalkStopPoint = 0.2f;
    CameraRaycaster CameraRaycaster;
    private Vector3 PlayerPosition;
    bool isInDrectionMode = false;
    ThirdPersonCharacter m_thirdPersonCharacter;
    void Start()
    {
        CameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        PlayerPosition = transform.position;
        m_thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.G))
        {
            isInDrectionMode = !isInDrectionMode;
            PlayerPosition = transform.position; //Clear the click target
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
            switch (CameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    PlayerPosition = CameraRaycaster.hit.point;
                    break;
            }
        }
        var postion = PlayerPosition - transform.position;
        if (postion.magnitude >= WalkStopPoint)
        {
            m_thirdPersonCharacter.Move(postion, false, false);
        }
        else
        {
            m_thirdPersonCharacter.Move(Vector3.zero, false, false);

        }
    }
}
