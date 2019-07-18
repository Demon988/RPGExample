using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField] Texture2D WalkCursor = null;
    [SerializeField] Texture2D AttackCursor = null;
    [SerializeField] Texture2D UnknowCursor = null;
    [SerializeField] Vector2 CursorHotport = new Vector2(0,0);
    void Start()
    {
        GetComponent<CameraRaycaster>().onLayerChange += UpdateCursorWhenLayerChange;
    }

    // Update is called once per frame
    void UpdateCursorWhenLayerChange(Layer newLayer)
    {
        switch (newLayer)
        {
            case Layer.Walkable:
                Cursor.SetCursor(WalkCursor, CursorHotport, CursorMode.Auto);
                break;
            case Layer.Enemy:
                Cursor.SetCursor(AttackCursor, CursorHotport, CursorMode.Auto);
                break;
            case Layer.RaycastEndStop:
                Cursor.SetCursor(UnknowCursor, CursorHotport, CursorMode.Auto);
                break;
            default:
                return;
        }
    }
}
