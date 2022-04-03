using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PP : MonoBehaviour
{
    public float maxPP = 100.0f;
    public float currentPP = 10.0f;

    private void Awake() {
        currentPP = maxPP;
    }



    private void Update() {
        if(Input.GetButtonDown("Fire3") ){
            currentPP -= 5.0f;
        }
        if(Input.GetButtonDown("Fire2") ){
            currentPP -= 20.0f;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PHeart" && currentPP < maxPP){
            currentPP += 20.0f;
        
        }
        

    }
}
