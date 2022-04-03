using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public void Update(){
    if(Input.GetButton("Jump")){
                SceneManager.LoadScene("Game");
            }
    if(Input.GetButton("Fire3")){
                Application.Quit();
            }}
        
}
