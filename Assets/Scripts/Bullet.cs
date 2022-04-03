using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{  private bool collided;
   public GameObject impact;
   public void OnCollisionEnter(Collision other) {
    //    if(other.gameObject.tag== "Enemy"){
          if(other.gameObject.tag != "Player" ){
            collided = true;
            var imp = Instantiate(impact,other.contacts[0].point, Quaternion.identity) as GameObject;
            Destroy(imp,2);
            Destroy(gameObject);
       }
   }
}
