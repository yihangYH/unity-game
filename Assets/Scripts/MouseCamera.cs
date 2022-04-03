using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 turn;
    public Vector3 deltaMove;
    public GameObject mover;
    public Camera camera;
    public float maximumLength;
    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    public float speed = 1;
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        mover.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
        camera.transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        RaycastHit hit;
            var mousPos = Input.mousePosition;
            rayMouse = camera.ScreenPointToRay(mousPos);
            if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, maximumLength)){

                RotateToMouseDirection(gameObject, hit.point);
            }else{
                var pos = rayMouse.GetPoint(maximumLength);
                RotateToMouseDirection(gameObject, pos);
            }
        if(Input.GetAxisRaw("Vertical") == 0.0){
            
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = runSpeed;
        }
        else{
            moveSpeed= walkSpeed;
        }


        deltaMove = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical")) * moveSpeed *Time.deltaTime;
        mover.transform.Translate(deltaMove);

        
    }

    void RotateToMouseDirection(GameObject obj, Vector3 destination){
        direction = destination - obj.transform.position;
        rotation = Quaternion.LookRotation(direction);
        //obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation,rotation,1);
    }
    public Quaternion GetQuaternion(){
        return rotation;
        
    }
}
