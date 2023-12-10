using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCode : MonoBehaviour
{
    [SerializeField] private GameObject soundEffect;
    [SerializeField] private GameObject splashSound;
    [SerializeField] private float fallSpeed;
    private bool isIceCream;

    void Start() {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -fallSpeed);
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(!isIceCream) {
            if(other.gameObject.tag == "IceCream" || other.gameObject.tag == "Cone") {
                Destroy(GetComponent<Rigidbody2D>());
                transform.parent = other.gameObject.transform;
                transform.position = new Vector3(transform.position.x, transform.position.y, -0.099f);
                transform.localRotation = new Quaternion(0, 0, 0, 1);
                GetComponent<BoxCollider2D>().isTrigger = false;
                gameObject.tag = "IceCream";
                gameObject.layer = LayerMask.NameToLayer("Default");
                Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);

                isIceCream = true;
            }
            else {
                if(!(other.gameObject.tag == "Lever" || other.gameObject.tag == "Sponge")) {
                    Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
                    Destroy(gameObject);
                }
            }
        }

        if(other.gameObject.tag == "SinkWater" || other.gameObject.tag == "WaterDrop" || other.gameObject.tag == "FryerOil") {
            Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            if(other.gameObject.tag == "FryerOil")
                Instantiate(splashSound, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
        Destroy(gameObject);
    }
}
