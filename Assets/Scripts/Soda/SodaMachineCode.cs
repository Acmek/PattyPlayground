using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachineCode : MonoBehaviour
{
    private bool soundPlay;
    private bool hasCup;
    private List<SodaCode> cups = new List<SodaCode>();

    void Start() {
        transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
    }

    void FixedUpdate() {
        if(cups.Count > 0) {
            bool test = false;
            foreach(SodaCode cup in cups)
                if(cup.IsFillable())
                    test = true;

            if(test) {
                transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = true;
                if(!soundPlay) {
                    GetComponent<AudioSource>().Play();
                    soundPlay = true;
                }
            }
            else {
                transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
                GetComponent<AudioSource>().Stop();
            }
        }
        else {
            GetComponent<AudioSource>().Stop();
            soundPlay = false;
        }

        if(GetComponent<AudioSource>().time > 0.5f)
            GetComponent<AudioSource>().time = 0.15f;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Cup") {
            transform.GetChild(0).GetComponent<ParticleSystem>().trigger.AddCollider(other.gameObject.GetComponent<EdgeCollider2D>());
            cups.Add(other.gameObject.GetComponent<SodaCode>());
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Cup") {
            transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;

            for(int i = 0; i < transform.GetChild(0).GetComponent<ParticleSystem>().trigger.colliderCount; i++)
                if(transform.GetChild(0).GetComponent<ParticleSystem>().trigger.GetCollider(i) == other.gameObject.GetComponent<EdgeCollider2D>())
                    transform.GetChild(0).GetComponent<ParticleSystem>().trigger.RemoveCollider(i);
                
            for(int i = 0; i < cups.Count; i++)
                if(cups[i] == other.gameObject.GetComponent<SodaCode>())
                    cups.RemoveAt(i);
        }
    }
}
