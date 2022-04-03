using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHeart : MonoBehaviour
{
    private PP pp;
    private HeartGenerator heartGenerator;
    private void Awake() {
        pp = GameObject.FindGameObjectWithTag("Player").GetComponent<PP>();
        heartGenerator = GameObject.FindGameObjectWithTag("UI").GetComponent<HeartGenerator>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && pp.currentPP < pp.maxPP){
            Destroy(gameObject);
            heartGenerator.setTrigger1();
        }
    }
}
