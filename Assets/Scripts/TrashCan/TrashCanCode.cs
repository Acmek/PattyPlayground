using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TrashCanCode : MonoBehaviour
{
    [SerializeField] private GameObject soundEffect;
    [SerializeField] private bool isTool;
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private bool servable;
    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Image image;
    [SerializeField] private bool isIceCream;
    private bool destroy;

    void FixedUpdate() {
        if(destroy) {
            if(image != null) {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.05f);
                if(image.color.a < 0)
                    Destroy(gameObject);
            }

            if(sprites.Length > 0) {
                for(int i = 0; i < sprites.Length; i++) {
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, sprites[i].color.a - 0.1f);
                    if(sprites[i].color.a < 0)
                        Destroy(gameObject);
                }
            }
            else {
                GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, GetComponent<SpriteRenderer>().color.a - 0.1f);
                if(GetComponent<SpriteRenderer>().color.a < 0)
                    Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!isIceCream) {
            if(other.gameObject.tag == "TrashCan") {
                if(isTool) {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
                    gameObject.transform.position = targetPos;
                    Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
                }
                else {
                    Destroy(gameObject);
                    Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
                }
            }
            if(other.gameObject.tag == "Remove") {
                if(isTool) {
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                    gameObject.transform.rotation = new Quaternion(0, 0, 0, 1);
                    gameObject.transform.position = targetPos;
                }
                else {
                    Destroy(gameObject);
                }
            }
        }
        if(other.gameObject.tag == "Tray") {
            if(servable) {
                destroy = true;
                Destroy(GetComponent<Rigidbody2D>());
                Destroy(GetComponent<MoveCode>());
                Destroy(GetComponent<MouseChangerCode>());
                Destroy(GetComponent<CollisionSound>());
                Destroy(GetComponent<Cleanable>());
                GameObject.Find("Bell").GetComponent<BellCode>().StartReset();
            }
        }
    }
}
