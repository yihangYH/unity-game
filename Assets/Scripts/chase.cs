using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class chase : MonoBehaviour
{
    private Transform target;
    private Vector3 direction;
    public float speed = 4.0f;
    Rigidbody rig;
    void Update() {

       transform.position = Vector3.MoveTowards(this.transform.position,GameObject.FindGameObjectWithTag("Player").transform.position,speed*Time.deltaTime);
       transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }

}
