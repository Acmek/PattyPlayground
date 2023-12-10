using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SinkCode : MonoBehaviour
{
    [SerializeField] private Image sinkWater;
    [SerializeField] private GameObject sinkCollider;
    [SerializeField] private float fillRate;
    [SerializeField] private FadeCode fadeCode;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private GameObject tap;
    [SerializeField] private AudioClip waterRunning;
    [SerializeField] private AudioClip waterDraining;
    [SerializeField] private float fadeRate;
    [SerializeField] private PolygonCollider2D waterDrop;
    private bool isRunning;
    private bool isPlugged;
    private bool canDrain;

    void FixedUpdate() {
        if(sinkWater.fillAmount > 0) {
            fadeCode.SetFade(true);
        }
        else {
            fadeCode.SetFade(false);
        }

        if(isRunning) {
            if(sinkWater.fillAmount != 1) {
                sinkWater.fillAmount += fillRate;
                canDrain = true;
            }
            fadeCode.SetFade(true);

            if(GetComponent<AudioSource>().time > 10) {
                GetComponent<AudioSource>().clip = waterRunning;
                GetComponent<AudioSource>().time = 1;
            }
        }
        if(!isPlugged && sinkWater.fillAmount != 0)
            sinkWater.fillAmount -= fillRate * 1.5f;

        if(sinkWater.fillAmount < 0)
            sinkWater.fillAmount = 0;
        if(sinkWater.fillAmount > 1)
            sinkWater.fillAmount = 1;

        if(!isPlugged && sinkWater.fillAmount > 0 && !isRunning) {
            if(canDrain) {
                GetComponent<AudioSource>().clip = waterDraining;
                GetComponent<AudioSource>().time = 5.9f;
                GetComponent<AudioSource>().volume = 0.4f;
                GetComponent<AudioSource>().Play();
                canDrain = false;
            }
        }
        if(sinkWater.fillAmount == 0 && !isRunning) {
            if(GetComponent<AudioSource>().volume > 0) {
                GetComponent<AudioSource>().volume -= fadeRate;
            }
            if(GetComponent<AudioSource>().volume <= 0) {
                GetComponent<AudioSource>().volume = 0;
                canDrain = true;
                GetComponent<AudioSource>().Stop();
            }
        }
        if(isPlugged && GetComponent<AudioSource>().clip == waterDraining && !isRunning) {
            canDrain = true;
            GetComponent<AudioSource>().Stop();
        }
        
        sinkCollider.transform.localPosition = new Vector3(sinkCollider.transform.localPosition.x, -1.35f + (1.35f * sinkWater.fillAmount), sinkCollider.transform.localPosition.z);

        if(sinkCollider.transform.localPosition.y == -1.35f)
            sinkCollider.GetComponent<BoxCollider2D>().enabled = false;
        else
            sinkCollider.GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnMouseDown() {
        if(isRunning) {
            particles.Stop();
            isRunning = false;
            waterDrop.enabled = false;
            GetComponent<AudioSource>().Stop();
        }
        else {
            particles.Play();
            isRunning = true;
            waterDrop.enabled = true;
            GetComponent<AudioSource>().clip = waterRunning;
            GetComponent<AudioSource>().time = 1;
            GetComponent<AudioSource>().volume = 0.075f;
            GetComponent<AudioSource>().Play();
        }

        Instantiate(tap, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
    }

    public void SetIsPlugged(bool b) {
        isPlugged = b;
    }
}
