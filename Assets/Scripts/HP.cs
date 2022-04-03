using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float maxHP = 100.0f;
    public float currentHP = 10.0f;

    private void Awake() {
        currentHP = maxHP;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            currentHP -= 5.0f;
        }
        if(other.gameObject.tag == "Heart" && currentHP < maxHP){
            currentHP += 20.0f;
            
        }
        

    }
}
