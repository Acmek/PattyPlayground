using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCode : MonoBehaviour
{
    [SerializeField] private float breakForce;
    [SerializeField] private bool isEgg;
    [SerializeField] private GameObject topEgg;
    [SerializeField] private GameObject bottomEgg;
    [SerializeField] private GameObject eggInside;
    [SerializeField] private GameObject eggBreakSound;

    void OnCollisionEnter2D(Collision2D other) {
        ContactPoint2D[] contacts = new ContactPoint2D[other.contactCount];
        other.GetContacts(contacts);

        float totalImpulse = 0;
        foreach(ContactPoint2D contact in contacts) {
            totalImpulse += contact.normalImpulse;
        }

        if(totalImpulse > breakForce) {
            if(isEgg) {
                Instantiate(topEgg, transform.position, new Quaternion(0, 0, 0, 1), GameObject.Find("IngredientHolder").transform);
                Instantiate(bottomEgg, transform.position, new Quaternion(0, 0, 0, 1), GameObject.Find("IngredientHolder").transform);
                Instantiate(eggInside, transform.position, new Quaternion(0, 0, 0, 1), GameObject.Find("IngredientHolder").transform);
            }
            Instantiate(eggBreakSound, transform.position, new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            Destroy(gameObject);
        }
    }
}
