using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCode : MonoBehaviour
{
    [SerializeField] private float heightSpeed;
    [SerializeField] private float maxHeight;
    private float height;

    void FixedUpdate()
    {
        height += Time.deltaTime * heightSpeed;
        if(height >= 2 * Mathf.PI)
            height = 0;

        transform.position = new Vector3(transform.position.x, 2.54f + Mathf.Sin(height) * maxHeight, transform.position.z);
    }
}
