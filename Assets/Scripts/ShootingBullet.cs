using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    public Camera camera;
    public Transform LHF,RHF,big;
    public GameObject projectile;
    public GameObject projectile1;
    public GameObject projectile2;
    public AudioSource sound;
    public AudioSource sound1;
    public AudioSource sound2;
    private Vector3 destination;
    private bool left,b,right;
    public float projectileSpeed = 30f;
    public float projectile1Speed = 30f;
    public float projectile2Speed = 30f;
    private float timeToFire;
    private float firerate = 4;
    private PP pp;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Awake() {
        pp = GameObject.FindGameObjectWithTag("Player").GetComponent<PP>();
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time >= timeToFire){
            sound.Play();
            timeToFire = Time.time + 1/firerate;
            left = true;
            ShootProjectile();
        }
        if(Input.GetButtonDown("Fire2") && Time.time >= timeToFire && pp.currentPP - 20 >= 0 ){
            sound2.Play();
            right = true;
            timeToFire = Time.time + 1/firerate;
            ShootProjectile();
        }
        if(Input.GetButtonDown("Fire3") && Time.time >= timeToFire && pp.currentPP - 5 >= 0){
            sound1.Play();
            timeToFire = Time.time + 1/firerate;
            ShootProjectile();
        }
    }

    void ShootProjectile(){
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0f));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)){
            destination = hit.point;

        }else{
            destination = ray.GetPoint(1000);

        }
        if(left){
            left = false;
            InstantiateProjectile(LHF);
        }else if(right){
            right = false;
            InstantiateProjectile2(RHF);
        }else{
            b = false;
            InstantiateProjectile1(big);
        }
        
    }
    void InstantiateProjectile(Transform firePoint){
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        Destroy(projectileObj,2f);
    }

    void InstantiateProjectile1(Transform firePoint){
        var projectileObj = Instantiate(projectile1, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectile1Speed;
        Destroy(projectileObj,2f);
    }

    void InstantiateProjectile2(Transform firePoint){
        var projectileObj = Instantiate(projectile2, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectile2Speed;
        Destroy(projectileObj,6f);
    }
}
