using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D WalkCursor = null;
    [SerializeField] Texture2D AttackCursor = null;
    [SerializeField] Texture2D UnknowCursor = null;
    [SerializeField] Vector2 CursorHotport = new Vector2(24,24);
    CameraRaycaster CameraRaycaster;
    void Start()
    {
        CameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CameraRaycaster.layerHit)
        {
            case Layer.Walkable:
                Cursor.SetCursor(WalkCursor, CursorHotport, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(AttackCursor, CursorHotport, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(UnknowCursor, CursorHotport, CursorMode.Auto);
                return;
        }
    }
}
