using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    CharacterController controller;
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    float horizontalLookSensitivity = 2500f;
    [SerializeField]
    float verticalLookSensitivity = 5000f;
    [SerializeField]
    bool invertUpDown = true;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 5.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f; 

    // Start is called before the first frame update
    void Start()
    {
        if(!controller) {
            controller = GetComponent<CharacterController>();
        }

        if(!playerCamera) {
            playerCamera = GetComponentInChildren<Camera>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        float axial = Input.GetAxis("Vertical"); //forward/backward
        float strafe = Input.GetAxis("Horizontal"); //side to side
        if(axial != 0 || strafe != 0) {
            HandleMovement(new Vector3(strafe, 0, axial));
        }

        float lookUpDown = Input.GetAxis("Mouse Y") * Time.deltaTime * verticalLookSensitivity * (invertUpDown ? -1 : 1);
        float lookLeftRight = Input.GetAxis("Mouse X") * Time.deltaTime * horizontalLookSensitivity;
        if(!Mathf.Approximately(0, lookUpDown) || !Mathf.Approximately(0, lookLeftRight)) {
            HandleLook(lookLeftRight, lookUpDown);
        }



        HandleAttack(Input.GetAxis("Fire1")); //LMB
    }

    ///<summary>Uses movement relative to characters local axes</summary>
    void HandleMovement(Vector3 relativeMovement) {
        float lookAng = gameObject.transform.rotation.eulerAngles.y;
        Vector3 move = Quaternion.Euler(0, lookAng, 0) * relativeMovement;
        controller.Move(move * Time.deltaTime * playerSpeed);
    }

    ///<summary>Yaw - around Y/green axis, Pitch - around X/red axis</summary>
    void HandleLook(float rotateCharacterYawDegrees, float rotateCameraPitchDegrees) {
        float targetAngle = playerCamera.transform.rotation.eulerAngles.x + rotateCameraPitchDegrees;
        if ((targetAngle >= 270 && targetAngle <= 450) // angle between 270 and 450
            || (targetAngle <= 90 && targetAngle >= -90)) // angle between -90 and 90
        { 
            playerCamera.transform.rotation = Quaternion.Euler(targetAngle, playerCamera.transform.rotation.eulerAngles.y, playerCamera.transform.rotation.eulerAngles.z);
        }
        
        gameObject.transform.Rotate(new Vector3(0, rotateCharacterYawDegrees, 0));

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    bool holdingAttack = false;
    float chargeTime = 0;
    const float minChargeTime = 0.5f;
    const float maxChargeTime = 2; //seconds
    void HandleAttack(float attack) {
        print(attack);
        if(attack > 0) {
            if(holdingAttack) {
                chargeTime += Time.deltaTime;
            } else {
                holdingAttack = true;
            }
        } else if(holdingAttack) {
            holdingAttack = false;
            if(chargeTime > minChargeTime) {
                // Shoot()
                chargeTime = 0;
            }
        }
    }
}
