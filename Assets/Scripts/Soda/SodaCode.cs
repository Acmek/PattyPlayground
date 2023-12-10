using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SodaCode : MonoBehaviour
{
    [SerializeField] private Image sodaImage;
    [SerializeField] private float fillRate;
    [SerializeField] private Transform cup;
    [SerializeField] private ParticleSystem sodaSpill;
    [SerializeField] private GameObject cupCap;
    [SerializeField] private GameObject splashSound;
    [SerializeField] private GameObject capSound;
    [SerializeField] private float stainLevel;
    [SerializeField] private bool isTitle;
    private bool red;
    private bool green;
    private bool blue;
    private bool water;
    private float redFilled;
    private float greenFilled;
    private float blueFilled;
    private bool inOil;
    private bool soundPlay;

    void Start() {
        if(isTitle) {
            redFilled = 70;
            greenFilled = 30;
            blueFilled = 0;

            sodaImage.color = new Color(redFilled / (redFilled + greenFilled + blueFilled), greenFilled / (redFilled + greenFilled + blueFilled), blueFilled / (redFilled + greenFilled + blueFilled), 128f / 255f);
            sodaImage.fillAmount = (redFilled + greenFilled + blueFilled) / 100f;
        }
    }

    void FixedUpdate() {
        if((red || green || blue) && redFilled + greenFilled + blueFilled + fillRate <= 100 && Vector3.Dot(cup.up, Vector3.up) >= 0.7f) {
            if(red)
                redFilled += fillRate;
            if(green)
                greenFilled += fillRate;
            if(blue)
                blueFilled += fillRate;

            sodaImage.color = new Color(redFilled / (redFilled + greenFilled + blueFilled), greenFilled / (redFilled + greenFilled + blueFilled), blueFilled / (redFilled + greenFilled + blueFilled), 128f / 255f);
            sodaImage.fillAmount = (redFilled + greenFilled + blueFilled) / 100f;
        }

        if(!inOil) {
            if((water && Vector3.Dot(cup.up, Vector3.up) >= 0.7f) || Vector3.Dot(cup.up, Vector3.down) >= -0.15f) {
                if(redFilled + greenFilled + blueFilled - (fillRate * 2) >= 0) {
                    redFilled -= (redFilled / (redFilled + greenFilled + blueFilled)) * (fillRate * 2);
                    greenFilled -= (greenFilled / (redFilled + greenFilled + blueFilled)) * (fillRate * 2);
                    blueFilled -= (blueFilled / (redFilled + greenFilled + blueFilled)) * (fillRate * 2);

                    if(redFilled < 0)
                        redFilled = 0;
                    if(greenFilled < 0)
                        greenFilled = 0;
                    if(blueFilled < 0)
                        blueFilled = 0;

                    sodaImage.color = new Color(redFilled / (redFilled + greenFilled + blueFilled), greenFilled / (redFilled + greenFilled + blueFilled), blueFilled / (redFilled + greenFilled + blueFilled), 128f / 255f);
                    sodaImage.fillAmount = (redFilled + greenFilled + blueFilled) / 100f;

                    if(Vector3.Dot(cup.up, Vector3.down) >= -0.15f && redFilled + greenFilled + blueFilled > 0) {
                        sodaSpill.startColor = new Color(redFilled / (redFilled + greenFilled + blueFilled), greenFilled / (redFilled + greenFilled + blueFilled), blueFilled / (redFilled + greenFilled + blueFilled), 128f / 255f);
                        sodaSpill.enableEmission = true;
                        if(!soundPlay) {
                            GetComponent<AudioSource>().Play();
                            soundPlay = true;
                        }
                    }
                    else {
                        GetComponent<AudioSource>().Stop();
                        soundPlay = false;
                    }
                }
                else {
                    sodaImage.fillAmount = 0;
                    redFilled = 0;
                    greenFilled = 0;
                    blueFilled = 0;
                }
            }
        }
        else {
            if(redFilled != 50 || greenFilled != 50 || blueFilled != 0)
                Instantiate(splashSound, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);

            redFilled = 50;
            greenFilled = 50;
            blueFilled = 0;

            sodaImage.color = new Color(redFilled / (redFilled + greenFilled + blueFilled), greenFilled / (redFilled + greenFilled + blueFilled), blueFilled / (redFilled + greenFilled + blueFilled), 128f / 255f);
            sodaImage.fillAmount = (redFilled + greenFilled + blueFilled) / 100f;
        }

        if(inOil || Vector3.Dot(cup.up, Vector3.down) < -0.15f || redFilled + greenFilled + blueFilled <= 0) {
            sodaSpill.enableEmission = false;
            GetComponent<AudioSource>().Stop();
            soundPlay = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "RedSoda")
            red = true;
        if(other.gameObject.tag == "GreenSoda")
            green = true;
        if(other.gameObject.tag == "BlueSoda")
            blue = true;
        if(other.gameObject.tag == "WaterDrop")
            water = true;
        if(other.gameObject.tag == "SinkWater") {
            if(sodaImage.fillAmount > 0) {
                Instantiate(splashSound, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
                sodaImage.fillAmount = 0;
                redFilled = 0;
                greenFilled = 0;
                blueFilled = 0;
            }
        }
        if(other.gameObject.tag == "CupCap") {
            cupCap.SetActive(true);
            Destroy(other.transform.parent.gameObject);
            Destroy(transform.parent.GetChild(2).gameObject);
            Instantiate(capSound, new Vector3(transform.position.x, transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "FryerOil") {
            inOil = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "RedSoda")
            red = false;
        if(other.gameObject.tag == "GreenSoda")
            green = false;
        if(other.gameObject.tag == "BlueSoda")
            blue = false;
        if(other.gameObject.tag == "WaterDrop")
            water = false;
        if(other.gameObject.tag == "FryerOil")
            inOil = false;
    }

    public bool IsFillable() {
        return redFilled + greenFilled + blueFilled + fillRate <= 100 && Vector3.Dot(cup.up, Vector3.up) >= 0.7f;
    }

    public float SpongeHit() {
        float oldFillAmount = sodaImage.fillAmount;
        sodaImage.fillAmount = 0;
        redFilled = 0;
        greenFilled = 0;
        blueFilled = 0;
        return oldFillAmount * stainLevel;
    }
}
