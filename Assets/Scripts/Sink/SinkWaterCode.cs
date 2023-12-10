using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkWaterCode : MonoBehaviour
{
    [SerializeField] private GameObject splash;

    void OnTriggerEnter2D(Collider2D other) {
        if(!(other.gameObject.tag == "Knife"  || other.gameObject.tag == "Sponge" || other.gameObject.tag == "Cup" || other.gameObject.tag == "SinkCorkTip" || other.gameObject.tag == "NoInteract"))
            Instantiate(splash, new Vector3(other.transform.position.x, other.transform.position.y, 0), new Quaternion(0, 0, 0, 1), GameObject.Find("SoundHolder").transform);
    }
}
