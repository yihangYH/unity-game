using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[System.Serializable]
public class Wave{
    public string waveName;
    public int numOfEnemy;

    public GameObject[] typeEnemgy;
    public float intervale;
    
}
public class WaveSpaner : MonoBehaviour
{
    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spwanPoint;
    public Animator animator;
    public Text waveName;
    public int x, y;

    private Wave currentwace;
    private int currentWaveNum;
    private bool canSpanw = true;
    private bool canAnimate = false;
    private bool trigger = false;
    private float nextspawnTime;
    private HP hp;
    private PP pp;
    private bool trigger1 = true;
    private bool trigger2 = true;
    private bool trigger3 = true;
    private bool trigger4 = true;
    private int count = 0;
    

    private FPS fps;

    private void Awake(){

        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS>();
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
        pp = GameObject.FindGameObjectWithTag("Player").GetComponent<PP>();
    }

    private void Update() {
        currentwace = waves[currentWaveNum];
        SpwanWave();
        if(count == 19){
            SceneManager.LoadScene("end");

        }
        
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
       
        if(totalEnemies.Length == 0 && currentWaveNum+1 != waves.Length && canAnimate){

            waveName.text = waves[currentWaveNum + 1].waveName;
            animator.SetTrigger("WaveComplete");
            count++;
            canAnimate = false;

            currentWaveNum++;
            canSpanw = true;
        }
    }
    void SpwanWave(){
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    
        if(canSpanw && nextspawnTime < Time.time && trigger){

            GameObject randomEnemy = currentwace.typeEnemgy[Random.Range(0,currentwace.typeEnemgy.Length)];
            
            int p_x, p_z;
            p_x = Random.Range(-50,50);
            p_z = Random.Range(-50,50);
            
            Transform randomPoint = randomEnemy.transform;
            randomPoint.position = new Vector3(player.transform.position.x + p_x, player.transform.position.y+50, player.transform.position.z + p_z);

            Instantiate(randomEnemy,randomPoint.position,Quaternion.identity);
            currentwace.numOfEnemy--;
            nextspawnTime = Time.time + currentwace.intervale;
            if(currentwace.numOfEnemy == 0){
    
                canSpanw = false;
                canAnimate = true;
                
            }
            
        }
        if(currentWaveNum == 4 || currentWaveNum == 9 || currentWaveNum == 14 ){

                trigger =false;
                
                
                
            }
        if(totalEnemies.Length == 0&& currentWaveNum == 15){
                fps.Skybox = false;}
        if(currentWaveNum == 4 && totalEnemies.Length == 0 && trigger4){
            pp.maxPP = 180;
            
            hp.maxHP = 180;
            
            trigger4 = false;
            pp.currentPP = pp.maxPP;
            hp.currentHP = hp.maxHP;
            

        }

        if(currentWaveNum == 9 && totalEnemies.Length == 0 && trigger3){
            pp.maxPP = 260;
            
            hp.maxHP = 260;
            
            trigger3 = false;
            pp.currentPP = pp.maxPP;
            hp.currentHP = hp.maxHP;
            

        }

        if(currentWaveNum == 14 && totalEnemies.Length == 0 && trigger2){
            pp.maxPP = 400;
            
            hp.maxHP = 400;
            
            trigger2 = false;
            pp.currentPP = pp.maxPP;
            hp.currentHP = hp.maxHP;
            

        }



    }

    public void setTrigger(){
        this.trigger = true;
    }
 public void setAnimate(){
        this.canAnimate = true;
    }

    public void setCurrentWaveNum(int num){
        this.currentWaveNum = num;
    }
    
}