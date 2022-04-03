using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PPDisplay : MonoBehaviour
{
    public Image healtppoint;
    public Image healthEffect;

    private PP pp;
    private void Awake() {
        pp = GameObject.FindGameObjectWithTag("Player").GetComponent<PP>();
    }

    private void Update() {
        healtppoint.fillAmount = pp.currentPP / pp.maxPP;
        if(healthEffect.fillAmount > healtppoint.fillAmount){
            healthEffect.fillAmount -= 0.003f;
        }else{
            healthEffect.fillAmount = healtppoint.fillAmount;
        }
    }
}
