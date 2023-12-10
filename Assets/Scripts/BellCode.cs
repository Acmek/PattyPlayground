using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellCode : MonoBehaviour
{
    [SerializeField] private Sprite[] people;
    [SerializeField] private bool isTitle;
    private bool resettingTray;
    private bool appear;
    private int index;

    void Start() {
        if(!isTitle) {
            index = Random.Range(0, people.Length);
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = people[index];
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.r, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.g, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.b, 2);
        }
    }

    void FixedUpdate() {
        if(resettingTray) {
            if(!appear) {
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.r, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.g, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.b, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.a - 0.025f);
                if(transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.a < -0.5f) {
                    int newIndex = Random.Range(0, people.Length);
                    while(index == newIndex) {
                        newIndex = Random.Range(0, people.Length);
                    }
                    index = newIndex;
                    transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = people[index];
                    appear = true;
                }
            }
            else {
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = new Color(transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.r, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.g, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.b, transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.a + 0.025f);
                if(transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color.a > 2) {
                    resettingTray = false;
                    appear = false;
                }
            }
        }
    }

    void OnMouseDown() {
        GetComponent<AudioSource>().time = 0.3f;
        GetComponent<AudioSource>().Play();

        if(!resettingTray && !isTitle) {
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(ResetTray());
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        GetComponent<AudioSource>().time = 0.3f;
        GetComponent<AudioSource>().Play();
    }

    IEnumerator ResetTray() {
        yield return new WaitForSeconds(0.05f);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void StartReset() {
        resettingTray = true;
    }
}
