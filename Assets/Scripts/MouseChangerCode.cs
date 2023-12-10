using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChangerCode : MonoBehaviour
{
    [SerializeField] private Texture2D mouseCursor;
    [SerializeField] private Texture2D mousePoint;
    [SerializeField] private Texture2D mouseGrab;
    private bool isGrabbing;

    void Update() {
        if(isGrabbing)
            Cursor.SetCursor(mouseGrab, Vector3.zero, CursorMode.ForceSoftware);
    }

    void OnMouseOver() {
        if(!isGrabbing)
            Cursor.SetCursor(mousePoint, Vector3.zero, CursorMode.ForceSoftware);
    }

    void OnMouseDown() {
        isGrabbing = true;
    }

    void OnMouseUp() {
        Cursor.SetCursor(mouseCursor, Vector3.zero, CursorMode.ForceSoftware);
        isGrabbing = false;
    }

    void OnMouseExit() {
        Cursor.SetCursor(mouseCursor, Vector3.zero, CursorMode.ForceSoftware);
    }
}
