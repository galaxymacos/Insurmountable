using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
	
	
	public int hp = 200;
	public Transform floatingHpPlace;
	public InGameUi InGameUi;

	private void Awake()
	{
	}

	private void Start()
	{
		GameManager.Instance.gameObjects.Add(gameObject);

	}

	public void TakeDamage(int damage)
    {
	    hp -= damage;
	    if (hp <= 0)
	    {

			GameManager.Instance.onPlayerDieCallback?.Invoke();
	    }
	    InGameUi.DisplayFloatingDamage(floatingHpPlace,damage);

    }

    

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Deadly"))
		{

			GameManager.Instance.onPlayerDieCallback?.Invoke();


			print(gameObject.name + " dies for falling");
		}
	}

}
