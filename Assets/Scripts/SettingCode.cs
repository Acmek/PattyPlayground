using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCode : MonoBehaviour
{
    [SerializeField] private GameObject settings;
    [SerializeField] private float resetTimer;
    private bool on;
    private bool cancel;

    public float clickAmount;
    private float leftTime;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            OpenSettings();
        }

        if(Input.GetMouseButtonDown(0)) {
            leftTime = resetTimer;

            if(leftTime > 0) {
                clickAmount ++;
            }

            if(clickAmount == 2) {
                OpenSettings();
                clickAmount = 0;
            }
        }

        if(leftTime > 0)
            leftTime -= Time.deltaTime;
        else
            clickAmount = 0;
    }

    void OpenSettings() {
        if(!on) {
                settings.SetActive(true);
                cancel = true;
                on = true;
            }
            else if(!cancel) {
                settings.SetActive(false);
                on = false;
            }

            cancel = false;
    }
}
