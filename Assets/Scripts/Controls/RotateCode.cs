using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCode : MonoBehaviour
{
    void OnMouseDrag() {
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<Rigidbody2D>().MoveRotation(0);
    }
}
