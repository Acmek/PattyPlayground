using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCode : MonoBehaviour
{
    [SerializeField] private float fadeRate;
    [SerializeField] private SpriteRenderer fadingObject;
    private int count;
    private bool setFade;

    void FixedUpdate() {
        if(setFade) {
            if(fadingObject.color.a != 0)
                fadingObject.color = new Color(1, 1, 1, fadingObject.color.a - fadeRate);
            
            if(fadingObject.color.a < 0)
                fadingObject.color = new Color(1, 1, 1, 0);
        }
        else {
            if(count > 0 && fadingObject.color.a != 0) {
                fadingObject.color = new Color(1, 1, 1, fadingObject.color.a - fadeRate);
            
                if(fadingObject.color.a < 0)
                    fadingObject.color = new Color(1, 1, 1, 0);
            }
            if(count <= 0 && fadingObject.color.a != 1)
                fadingObject.color = new Color(1, 1, 1, fadingObject.color.a + fadeRate);
            
                if(fadingObject.color.a > 1)
                    fadingObject.color = new Color(1, 1, 1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!(other.gameObject.tag == "Knife"  || other.gameObject.tag == "Sponge" || other.gameObject.tag == "Cup" || other.gameObject.tag == "SinkCorkTip" || other.gameObject.tag == "NoInteract"))
            count++;
    }

    void OnTriggerExit2D(Collider2D other) {
        if(!(other.gameObject.tag == "Knife"  || other.gameObject.tag == "Sponge" || other.gameObject.tag == "Cup" || other.gameObject.tag == "SinkCorkTip" || other.gameObject.tag == "NoInteract"))
            count--;
    }

    public void SetFade(bool b) {
        setFade = b;
    }
}
