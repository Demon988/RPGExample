using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float WalkStopPoint = 0.2f;
    CameraRaycaster CameraRaycaster;
    private Vector3 PlayerPosition;
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
