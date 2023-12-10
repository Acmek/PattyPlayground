using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCode : MonoBehaviour
{
    [SerializeField] private GameObject soundEffect;
    [SerializeField] private bool isTitle;
    private float deltaX, deltaY;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        deltaX = mousePos.x - transform.position.x;
        deltaY = mousePos.y - transform.position.y;
    }

    void OnMouseUp() {
        if(!isTitle)
            rb.gravityScale = 2;
    }

    void OnMouseDrag() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.MovePosition(new Vector2(mousePos.x - deltaX, mousePos.y - deltaY));
    }
}