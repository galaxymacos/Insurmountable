using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Skill
{
    [SerializeField] private float blockingDuration = 0.5f;
    private void FixedUpdate()
    {
        if (!_skillNotOnCooldown)
        {
            if (TimePlayed + cooldown <= Time.time)
            {
                _skillNotOnCooldown = true;
            }
        }
        
        if (_isPlaying)
        {
            if (TimePlayed + blockingDuration <= Time.time)
            {
                print("Unblocking");
                playerMovement.ChangePlayerState(PlayerMovement.PlayerState.Stand);
                playerController.canControl = true;
                _isPlaying = false;
            }
        }
    }

    public override void Play()
    {
        if (_skillNotOnCooldown)
        {
            playerController.canControl = false;
            print("Blocking");
            base.Play();
            _skillNotOnCooldown = false;    // Skill is on cooldown
            playerMovement.ChangePlayerState(PlayerMovement.PlayerState.Block);
        }
        else
        {
            print("Blocking is on cooldown");
        }
    }
    
}
