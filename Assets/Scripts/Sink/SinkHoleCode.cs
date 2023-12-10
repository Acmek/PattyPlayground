using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkHoleCode : MonoBehaviour
{
    [SerializeField] private SinkCode sinkCode;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "SinkCorkTip")
            sinkCode.SetIsPlugged(true);
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "SinkCorkTip")
            sinkCode.SetIsPlugged(false);
    }
}
