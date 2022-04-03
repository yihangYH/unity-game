using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss4 : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp =100;
    
    void Start()
    {
        hp = Random.Range(500,600);
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
    IEnumerator wait(float waitTime){ //creating a function
        yield return new WaitForSeconds(waitTime); //tell unity to wait!!
    }


    
}
