using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreenCode : MonoBehaviour
{
    private bool loaded;

    void Start() {
        loaded = true;
    }

    void Update() {
        if(loaded) {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, GetComponent<SpriteRenderer>().color.a - 0.001f);

            if(GetComponent<SpriteRenderer>().color.a < 0) {
                GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                loaded = false;
            }
        }
    }
}
