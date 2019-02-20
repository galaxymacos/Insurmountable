using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraPosIn3D;


    // Player ability
    public bool canAwake;
    internal bool canControl;
    [SerializeField] private float enter3DWorldDuration = 2.5f;
    public float facingOffset = 1; // 1 if player is facing right and -1 if player is facing left
    private bool is3D;
    public bool isFacingRight;
    
    public delegate void OnFacingChange(bool isFacingRight);
    public OnFacingChange onFacingChangeCallback;

    // Player State
    [SerializeField] private Camera mainCamera;

    private float startCountdown;
    public bool transferStoragePowerFull;

    private CameraEffect _cameraEffect;

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        if (Camera.main != null)
        {
            mainCamera = Camera.main;
            _cameraEffect = mainCamera.GetComponent<CameraEffect>();

        }

        isFacingRight = true;
        onFacingChangeCallback?.Invoke(true);
        canControl = true;
    }



    private void CheckIfPlayerAwakes()
    {
        if (canControl)
        {
            
                if (Input.GetKeyDown(KeyCode.T))
                {
                    startCountdown = Time.time;
                    Time.timeScale = 0.5f;
                    _cameraEffect.StartShaking(10000f);    // Keep shaking
                }

                if (Input.GetKey(KeyCode.T))
                    if (startCountdown + enter3DWorldDuration * Time.timeScale <= Time.time)
                    {
                        print(startCountdown + enter3DWorldDuration * Time.timeScale + "  " + Time.time);
                        
                        transferStoragePowerFull = true;
                    }

                if (Input.GetKeyUp(KeyCode.T))
                {
                    _cameraEffect.StopShaking();
                    Time.timeScale = 1f;
                    if (transferStoragePowerFull)
                    {
                        Time.timeScale = 1f;
                        GameManager.Instance.is3D = !GameManager.Instance.is3D;
                        if (GameManager.Instance.is3D)
                        {
                            GameManager.Instance.OnSceneChangeCallback?.Invoke(true);                            
                        }
                        else
                        {
                            GameManager.Instance.OnSceneChangeCallback?.Invoke(false);
                        }
                    }

                }
            

        }
    }

    private void Update()
    {
        if (!canControl)
        {
            return;
        }

        if (isFacingRight)
            facingOffset = 1;
        else
            facingOffset = -1;
        if (canAwake) CheckIfPlayerAwakes();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.Jump();
            playerMovement.isGliding = true;

        }
        

        if (!Input.GetKey(KeyCode.Space))
        {
            playerMovement.FallDown();
        }
        
        
        var horizontalMovement = Input.GetAxisRaw("Horizontal");
        var verticalMovement = Input.GetAxisRaw("Vertical");
        playerMovement.Move(horizontalMovement,verticalMovement);
        
        ChangeFaceDirection(horizontalMovement);
       
    }

    private void ChangeFaceDirection(float horizontalMovement)    // Player Controller
    {
        if (horizontalMovement < 0)
        {
            if (isFacingRight)
            {
                var localScale = transform.localScale;
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
                isFacingRight = false;
                onFacingChangeCallback?.Invoke(false);
            }
        }
        else if (horizontalMovement > 0)
        {
            if (!isFacingRight)
            {
                var localScale = transform.localScale;
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
                isFacingRight = true;
                onFacingChangeCallback?.Invoke(true);

            }
        }
    }

}