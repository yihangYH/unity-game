using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp =100;
    
    void Start()
    {
        hp = Random.Range(200,300);
    }
    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "projectile"){
            hp -= 25;
            if(hp <= 0){
                Destroy(gameObject);
            }
        }
               if(other.gameObject.tag == "projectileFire"){
            if(hp <= 0){
                Destroy(gameObject);
            }
            hp-=30;
            if(hp <= 0){
                Destroy(gameObject);
            }



        }

        if(other.gameObject.tag == "projectileFlash"){
            if(hp <= 0){
                Destroy(gameObject);
            }
            hp-=40;
            if(hp <= 0){
                Destroy(gameObject);
            }



        }
    }
    IEnumerator wait(float waitTime){ //creating a function
        yield return new WaitForSeconds(waitTime); //tell unity to wait!!
    }


    
}
