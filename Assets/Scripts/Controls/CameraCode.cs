using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Texture2D mouseCursor;
    [SerializeField] private Texture2D mouseGrab;
    private float oldCamPos, oldMousePos;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1)) {
            oldCamPos = transform.position.x;
            oldMousePos = Input.mousePosition.x;
        }

        if(Input.GetMouseButton(1)) {
            transform.position = new Vector3(oldCamPos - (Input.mousePosition.x - oldMousePos) * moveSpeed, transform.position.y, transform.position.z);
            Cursor.SetCursor(mouseGrab, Vector3.zero, CursorMode.ForceSoftware);
        }

        if(Input.GetMouseButton(0)) {
            if(transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= 7.5f) {
                if(transform.position.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x >= 8.5f)
                    transform.position = new Vector3(transform.position.x - moveSpeed * 30, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x - moveSpeed * 5, transform.position.y, transform.position.z);
            }
            if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x >= 7.5f) {
                if(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x >= 8.5f)
                    transform.position = new Vector3(transform.position.x + moveSpeed * 30, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x + moveSpeed * 5, transform.position.y, transform.position.z);
            }
        }

        if(Input.GetMouseButtonUp(1))
            Cursor.SetCursor(mouseCursor, Vector3.zero, CursorMode.ForceSoftware);

        if(transform.position.x < 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        if(transform.position.x > 65.6f)
            transform.position = new Vector3(65.6f, transform.position.y, transform.position.z);
    }

    public void SetMoveSpeed(float sliderValue) {
        moveSpeed = sliderValue;
    }
}
