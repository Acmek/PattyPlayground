using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleanable : MonoBehaviour
{
    [SerializeField] private float stainLevel;

    public float GetStainLevel() {
        return stainLevel;
    }
}
