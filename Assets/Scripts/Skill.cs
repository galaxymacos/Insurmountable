using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
	[SerializeField] private string skillName;
	[SerializeField] internal float cooldown;
	[SerializeField] private Image skillImage;
	


	internal float TimePlayed = 0f;
	internal PlayerController playerController;
	internal PlayerMovement playerMovement;
	internal Rigidbody rb;
	internal Animator animator;
	internal bool _skillNotOnCooldown = true;

	internal bool _isPlaying;

	
	// Start is called before the first frame update
	public virtual void Start()
	{
		_skillNotOnCooldown = true;	// No cooldown when started
		animator = GameManager.Instance.player.GetComponent<Animator>();
		rb = GameManager.Instance.player.GetComponent<Rigidbody>();
		playerController = GameManager.Instance.player.GetComponent<PlayerController>();
		playerMovement = GameManager.Instance.player.GetComponent<PlayerMovement>();
	}

	private void LateUpdate()	
	{
		if (_skillNotOnCooldown)	// skill cooldown
		{
			skillImage.fillAmount = 1;

		}
		else
		{
			skillImage.fillAmount = Mathf.Clamp01((Time.time - TimePlayed) / cooldown);

		}
	}


	public IEnumerator PlayerCanControl(float afterSec)
    {
	    yield return new WaitForSeconds(afterSec);
	    playerController.canControl = true;
	    print("Finishing skill");
    }

    public virtual void Play()
    {
	    _isPlaying = true;
	    TimePlayed = Time.time;

    }

}
