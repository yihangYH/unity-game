using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewChase : MonoBehaviour
{   public Transform firePoint;
    private Transform target;
    private Vector3 direction;
    public float speed = 4.0f;
    public float projectileSpeed = 30f;
    public float timetoShoot = 1.3f;
    float originalTime;
    public GameObject projectile;
    void Start(){
        originalTime = timetoShoot;
    }
    void Update() {

       transform.position = Vector3.MoveTowards(this.transform.position,GameObject.FindGameObjectWithTag("Player").transform.position,speed*Time.deltaTime);
       transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
private void FixedUpdate(){
    timetoShoot -= Time.deltaTime;
    if(timetoShoot<0){
        ShootAtPlayer();
        timetoShoot=originalTime;
    }
}

void ShootAtPlayer()
{
     
    GameObject tempBullet = Instantiate(projectile, firePoint.position, firePoint.rotation); 
    Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
    tempRigidBodyBullet.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
  
    Destroy(tempBullet, 5f);
}

}
