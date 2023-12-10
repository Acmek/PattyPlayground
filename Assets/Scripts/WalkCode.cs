using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkCode : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float delay;
    [SerializeField] private float heightSpeed;
    private float realWalk;
    private float realDelay;
    private float realHeight;
    private float height;

    void FixedUpdate()
    {
        height += Time.deltaTime * realHeight;
        if(height >= 2 * Mathf.PI)
            height = 0;

        if(realDelay <= 0) {
            if(transform.position.x < 32) {
                realWalk = Random.Range(0.01f, moveSpeed);
                realDelay = Random.Range(0, delay);
                realHeight = Random.Range(1.5f, heightSpeed);
                GetComponent<SpriteRenderer>().flipX = false;
                transform.position = new Vector3(32, transform.position.y, transform.position.z);
            }
            if(transform.position.x > 43.5f) {
                realWalk = -Random.Range(0.01f, moveSpeed);
                realDelay = Random.Range(0, delay);
                realHeight = Random.Range(1.5f, heightSpeed);
                GetComponent<SpriteRenderer>().flipX = true;
                transform.position = new Vector3(43.5f, transform.position.y, transform.position.z);
            }

            transform.position = new Vector3(transform.position.x + realWalk, Mathf.Sin(height) / 5f, transform.position.z);
        }
        else {
            realDelay -= Time.deltaTime;
        }
    }
}
