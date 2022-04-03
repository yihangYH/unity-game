using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart : MonoBehaviour
{
    
    private HP hp;
    private HeartGenerator heartGenerator;
    private void Awake() {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
        heartGenerator = GameObject.FindGameObjectWithTag("UI").GetComponent<HeartGenerator>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && hp.currentHP < hp.maxHP){
            Destroy(gameObject);
            heartGenerator.setTrigger();
        }
    }
}
