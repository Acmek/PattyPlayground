using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideCode : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    [SerializeField] private float audioStart;

    void Start()
    {
        GetComponent<AudioSource>().time = audioStart;
        Destroy(gameObject, lifeTime);
    }
}
