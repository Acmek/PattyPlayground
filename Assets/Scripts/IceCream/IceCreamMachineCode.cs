using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMachineCode : MonoBehaviour
{
    [SerializeField] private Sprite leverSpriteUp;
    [SerializeField] private Sprite leverSpriteDown;
    [SerializeField] private GameObject iceCream;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private float spawnDelay;
    [SerializeField] private GameObject clickSound;
    private float spawnTimer;
    private bool isOn;
    private float xOffset;

    void Start() {
        xOffset = GetComponent<BoxCollider2D>().offset.x;
    }

    void Update() {
        if(isOn) {
            if(spawnTimer < spawnDelay)
                spawnTimer += Time.deltaTime;
            if(spawnTimer >= spawnDelay) {
                spawnTimer = 0;
                Instantiate(iceCream, spawnPos, new Quaternion(0, 0, 0, 1), GameObject.Find("IngredientHolder").transform);
            }
            if(GetComponent<AudioSource>().time > 2.5f)
                GetComponent<AudioSource>().time = 0.15f;
        }
    }

    void OnMouseDown() {
        if(isOn) {
            GetComponent<SpriteRenderer>().sprite = leverSpriteUp;
            GetComponent<BoxCollider2D>().offset = new Vector2(xOffset, 3.495f);
            isOn = false;
            Instantiate(clickSound, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            GetComponent<AudioSource>().Stop();
        }
        else {
            GetComponent<SpriteRenderer>().sprite = leverSpriteDown;
            GetComponent<BoxCollider2D>().offset = new Vector2(xOffset, 2.285f);
            spawnTimer = 0;
            isOn = true;
            Instantiate(clickSound, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            GetComponent<AudioSource>().Play();
        }
    }
}
