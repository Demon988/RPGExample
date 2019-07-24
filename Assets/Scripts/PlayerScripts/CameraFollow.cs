using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject Player;
    private Transform CameraMain;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        CameraMain = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Player.transform.position;
    }
}
