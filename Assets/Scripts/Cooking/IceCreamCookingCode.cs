using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamCookingCode : MonoBehaviour
{
    [SerializeField] private float cookRate;
    [SerializeField] private float grillMultiplier;
    [SerializeField] private float fryerMultiplier;
    [SerializeField] private AudioClip grilling;
    [SerializeField] private AudioClip frying;
    [SerializeField] private float fadeRate;
    [SerializeField] private SpriteRenderer sprite;
    private float originalCookRate;
    private bool cooking;

    void Start() {
        originalCookRate = cookRate;
    }

    void FixedUpdate() {
        if(cooking) {
            if(sprite.color.r != 0)
                sprite.color = new Color(sprite.color.r - cookRate, sprite.color.g, sprite.color.b, 1);
            if(sprite.color.g != 0)
                sprite.color = new Color(sprite.color.r, sprite.color.g - cookRate, sprite.color.b, 1);
            if(sprite.color.b != 0)
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b - cookRate, 1);

            if(sprite.color.r < 0)
                sprite.color = new Color(0, sprite.color.g, sprite.color.b, 1);
            if(sprite.color.g < 0)
                sprite.color = new Color(sprite.color.r, 0, sprite.color.b, 1);
            if(sprite.color.b < 0)
                sprite.color = new Color(sprite.color.r, sprite.color.g, 0, 1);
        }
        else {
            if(GetComponent<AudioSource>().volume > 0)
                GetComponent<AudioSource>().volume -= fadeRate;
            if(GetComponent<AudioSource>().volume < 0) {
                GetComponent<AudioSource>().volume = 0;
                GetComponent<AudioSource>().Stop();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "GrillTop") {
            cooking = true;
            cookRate = originalCookRate * grillMultiplier;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().clip = grilling;
            GetComponent<AudioSource>().Play();
        }
        if(other.gameObject.tag == "FryerOil") {
            cooking = true;
            cookRate = originalCookRate * fryerMultiplier;
            GetComponent<AudioSource>().volume = 0.25f;
            GetComponent<AudioSource>().clip = frying;
            GetComponent<AudioSource>().Play();
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "GrillTop" || other.gameObject.tag == "FryerOil")
            cooking = false;
    }
}
