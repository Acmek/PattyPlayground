using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ButtonCode : MonoBehaviour
{
    [SerializeField] private Texture2D mouseCursor;
    [SerializeField] private Texture2D mousePoint;
    [SerializeField] private Texture2D mouseGrab;
    [SerializeField] private Sprite button;
    [SerializeField] private Sprite buttonOver;
    [SerializeField] private bool isQuit;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private bool isKitchen;
    private bool hasClicked;
    private bool isGrabbing;

    void Update() {
        if(isGrabbing)
            Cursor.SetCursor(mouseGrab, Vector3.zero, CursorMode.ForceSoftware);
        
        if(hasClicked && !isKitchen) {
            background.color = new Color(0, 0, 0, background.color.a + 0.01f);
            if(background.color.a > 1)
                SceneManager.LoadScene("Kitchen");
        }
    }

    void OnMouseOver() {
        if(!isGrabbing)
            Cursor.SetCursor(mousePoint, Vector3.zero, CursorMode.ForceSoftware);
        
        if(!isKitchen)
            GetComponent<SpriteRenderer>().sprite = buttonOver;
    }

    void OnMouseDown() {
        isGrabbing = true;

        if(!hasClicked && !isKitchen) {
            if(isQuit) {
                Application.Quit();
            }
            else {
                hasClicked = true;
            }
        }
    }

    void OnMouseUp() {
        Cursor.SetCursor(mouseCursor, Vector3.zero, CursorMode.ForceSoftware);
        isGrabbing = false;
    }

    void OnMouseExit() {
        Cursor.SetCursor(mouseCursor, Vector3.zero, CursorMode.ForceSoftware);
        
        if(!isKitchen)
            GetComponent<SpriteRenderer>().sprite = button;
    }

    public void ExitToTitle() {
        SceneManager.LoadScene("TitleScreen");
    }
}
