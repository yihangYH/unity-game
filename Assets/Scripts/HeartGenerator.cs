using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartGenerator : MonoBehaviour
{
    public Transform[] spwanPoint;
    public GameObject heart;
    public GameObject pheart;
    private HP hp;
    private PP pp;
    private bool trigger = true;
    private bool trigger1 = true;
    private FPS fps;
    private void Awake() {
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
        pp = GameObject.FindGameObjectWithTag("Player").GetComponent<PP>();
        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS>();

    }
    // Update is called once per frame
    void Update()
    {
    if(fps.check1){
        Heart(6,7);}
    else if(fps.check2){
        Heart(4,5);}
    else if(fps.check3){
        Heart(2,3);}
    else if(fps.check4){
        Heart(0,1);}
    

    }
    public void setTrigger1(){
        this.trigger1 = true;
    }
    public void setTrigger(){
        this.trigger = true;
    }

    private void Heart(int x , int y){
        if(hp.currentHP < hp.maxHP - 50.0f && trigger){
            Transform randomPoint = spwanPoint[x];
            Instantiate(heart,randomPoint.position,Quaternion.identity);
            trigger = false;
        }
        if(pp.currentPP < pp.maxPP - 50.0f && trigger1){
            Transform randomPoint = spwanPoint[y];
            Instantiate(pheart,randomPoint.position,Quaternion.identity);
            trigger1 = false;
        }
    }


}
