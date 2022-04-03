using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FPS : MonoBehaviour
{
    // Start is called before the first frame update
    public int fps = 120;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    float XRotation;
    float YRotation;
    float lookSensitivity =2;
    float currentXRotation;
    float currentYRotation;
    float xRotationV;
    float yRotationV;
    float lookSmoothnes = 0.1f;
    private WaveSpaner waveSpaner;
    public bool check1 = false;
    public bool check2 = false;
    public bool check3 = false;
    public bool check4 = false;
    public bool Skybox = false;
    public HP hp;

    private void Awake(){
        waveSpaner = GameObject.FindGameObjectWithTag("Wave").GetComponent<WaveSpaner>();
        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();

    }
    void Start()
    {   
        Application.targetFrameRate = fps;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp.currentHP<=0){
             SceneManager.LoadScene("Dead");
             Cursor.visible = true;
             Cursor.lockState = CursorLockMode.None;
             
         }
        CharacterController controller = GetComponent<CharacterController>();
        if(controller.isGrounded){
            moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if(Input.GetButton("Jump")){
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection*Time.deltaTime);
        YRotation += Input.GetAxis("Mouse X") * lookSensitivity;
        XRotation -= Input.GetAxis("Mouse Y") * lookSensitivity;
        XRotation = Mathf.Clamp(XRotation,-80,100);
        currentXRotation = Mathf.SmoothDamp(currentXRotation,XRotation, ref xRotationV,lookSmoothnes);
        currentYRotation = Mathf.SmoothDamp(currentYRotation,YRotation, ref yRotationV,lookSmoothnes);
        transform.rotation = Quaternion.Euler(XRotation,YRotation,0);
         
         
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Check1" ){
            waveSpaner.setAnimate();
            waveSpaner.setCurrentWaveNum(15);
            waveSpaner.x = 12;
            waveSpaner.y = 15;
            waveSpaner.setTrigger();
            
            check1 = true;
        
        }
        if(other.gameObject.tag == "Check2" ){
            waveSpaner.setCurrentWaveNum(10);
            waveSpaner.x = 8;
            waveSpaner.y = 11;
            waveSpaner.setTrigger();
            
            check2 = true;
            Skybox = true;
            
        
        }
        if(other.gameObject.tag == "Check3" ){
            waveSpaner.setCurrentWaveNum(5);
            waveSpaner.x = 4;
            waveSpaner.y = 7;
            waveSpaner.setTrigger();
            
            check3 = true;
        
        }
        if(other.gameObject.tag == "Check4" ){
            waveSpaner.setCurrentWaveNum(0);
            waveSpaner.x = 0;
            waveSpaner.y = 3;
            
            waveSpaner.setTrigger();
            check4 = true;
        
        }
    }




    
}
