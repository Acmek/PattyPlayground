using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeCode : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spongeStain;
    [SerializeField] private GameObject soundEffect;
    private float stainLevel;
    private bool sinkWater;
    private bool waterDrop;
    private bool isDirty;

    void FixedUpdate() {
        if(sinkWater) {
            stainLevel = 0;
            spongeStain.color = new Color(1, 1, 1, 0);
            isDirty = false;
        }
        if(waterDrop && !sinkWater) {
            stainLevel -= 1;
            if(stainLevel < 0)
                stainLevel = 0;
            spongeStain.color = new Color(1, 1, 1, stainLevel / 255);
            isDirty = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!isDirty) {
            if(other.gameObject.GetComponent<Cleanable>() != null) {
                stainLevel += other.gameObject.GetComponent<Cleanable>().GetStainLevel();

                if(stainLevel > 170) {
                    stainLevel = 170;
                    isDirty = true;
                }
                spongeStain.color = new Color(1, 1, 1, stainLevel / 255);
                Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);

                if(other.gameObject.tag == "Cone" || other.gameObject.tag == "PattySide")
                    Destroy(other.gameObject.transform.parent.gameObject);
                else
                    Destroy(other.gameObject);
            }

            if(other.gameObject.tag == "Cup") {
                stainLevel += other.gameObject.GetComponent<SodaCode>().SpongeHit();

                if(stainLevel > 170) {
                    stainLevel = 170;
                    isDirty = true;
                }
                spongeStain.color = new Color(1, 1, 1, stainLevel / 255);
                Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            }
        }

        if(other.gameObject.tag == "SinkWater")
            sinkWater = true;
        if(other.gameObject.tag == "WaterDrop")
            waterDrop = true;
        if(other.gameObject.tag == "FryerOil") {
            if(stainLevel < 170)
                Instantiate(soundEffect, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            stainLevel = 170;
            spongeStain.color = new Color(1, 1, 1, stainLevel / 255);
            isDirty = true;
        }
            
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "SinkWater")
            sinkWater = false;
        if(other.gameObject.tag == "WaterDrop")
            waterDrop = false;
    }
}
