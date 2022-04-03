using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Material SkyboxDay;
    public Material SkyboxNight;
    private FPS fps; 
    private void Awake(){
        fps = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fps.Skybox){
            RenderSettings.skybox = SkyboxNight;
            

        }
        else{
            RenderSettings.skybox = SkyboxDay;

        }
        
    }
}
