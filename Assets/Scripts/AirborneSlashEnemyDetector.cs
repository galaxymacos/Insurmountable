using System.Collections.Generic;
using UnityEngine;

public class AirborneSlashEnemyDetector : MonoBehaviour
{
	internal List<Collider> _enemiesInRange;

	private void Start()
	{
		_enemiesInRange = new List<Collider>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
			_enemiesInRange.Add(other);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))

			_enemiesInRange.Remove(other);
	}
}