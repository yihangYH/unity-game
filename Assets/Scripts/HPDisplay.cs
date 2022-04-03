using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPDisplay : MonoBehaviour
{
    public Image healthPoint;
    public Image healthEffect;

    private HP hp;
    private void Awake() {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
    }

    private void Update() {
        healthPoint.fillAmount = hp.currentHP / hp.maxHP;
        if(healthEffect.fillAmount > healthPoint.fillAmount){
            healthEffect.fillAmount -= 0.003f;
        }else{
            healthEffect.fillAmount = healthPoint.fillAmount;
        }
    }
}
