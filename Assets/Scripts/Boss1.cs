using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    // This script is used to generate the first boss spider boss
    public int hp =100;
    
    void Start()
    {
        hp = Random.Range(200,300);
    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "projectile"){
            hp -= 10;
            if(hp <= 0){
                Destroy(gameObject);
            }
        }
               if(other.gameObject.tag == "projectileFire"){
            if(hp <= 0){
                Destroy(gameObject);
            }
            hp-=15;
            if(hp <= 0){
                Destroy(gameObject);
            }



        }

        if(other.gameObject.tag == "projectileFlash"){
            if(hp <= 0){
                Destroy(gameObject);
            }
            hp-=20;
            if(hp <= 0){
                Destroy(gameObject);
            }



        }
    }
    IEnumerator wait(float waitTime){ 
        yield return new WaitForSeconds(waitTime); 
    }


    
}
