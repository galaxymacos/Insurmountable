using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerCombat : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerMovement _playerMovement;
    [SerializeField] private Skill[] playerSkills;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        if (_playerController.canControl)
        {
            if (_playerMovement.playerCurrentState == PlayerMovement.PlayerState.Stand ||
                _playerMovement.playerCurrentState == PlayerMovement.PlayerState.Walk)    // If player is on ground
            {
                if (Input.GetKeyDown(KeyCode.K))
                    playerSkills[0].Play();

                else if (Input.GetKeyDown(KeyCode.L))
                    playerSkills[1].Play();
                else if (Input.GetKeyDown(KeyCode.LeftShift)) playerSkills[3].Play();
            }

            if (_playerMovement.playerCurrentState == PlayerMovement.PlayerState.Jump ||
                _playerMovement.playerCurrentState == PlayerMovement.PlayerState.DoubleJump)    // If player is on air
                if (Input.GetKeyDown(KeyCode.J))
                    playerSkills[2].Play();
        }
    }
}