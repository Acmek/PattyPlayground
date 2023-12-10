using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutCode : MonoBehaviour
{
    [SerializeField] private GameObject spawnedObject;
    [SerializeField] private GameObject soundEffect;
    [SerializeField] private int min;
    [SerializeField] private int max;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Knife") {
            int amount = Random.Range(min, max + 1);
            
            for(int i = 0; i < amount; i++)
                Instantiate(spawnedObject, transform.position, new Quaternion(0, 0, 0, 1), GameObject.Find("IngredientHolder").transform);
            
            Instantiate(soundEffect, transform.position, new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
            Destroy(gameObject, 0);
        }
    }
}
