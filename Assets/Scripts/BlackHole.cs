using UnityEngine;

public class BlackHole : MonoBehaviour
{
	private bool _tookDamage;
	[SerializeField] private float absorbSpeed = 5f;
	[SerializeField] private int damage = 20;
	[SerializeField] private float duration = 3f;

	private float existTime;
	[SerializeField] private float explosionForce = 100f;
	[SerializeField] private float radius = 5f;
	[SerializeField] private float damagePerSecWhenAbsorb = 4f;

	[SerializeField] private float stiffTimeAfterExplosion = 1.5f;

	private Camera mainCamera;

	private void Start()
	{
		existTime = Time.time;
		mainCamera = Camera.main;
	}

	private void FixedUpdate()
	{

		LayerMask enemyLayer = 1 << 9;

		var enemies =
			Physics.OverlapSphere(transform.position, radius, enemyLayer);
		if (existTime + duration >= Time.time)
		{
			

			foreach (var enemy in enemies)
			{
				var direction = (transform.position - enemy.transform.position).normalized;
				enemy.gameObject.GetComponent<Rigidbody>().AddForce(direction * Time.fixedDeltaTime * absorbSpeed);
				enemy.GetComponent<Enemy>().TakeDamage(1/damagePerSecWhenAbsorb, Time.fixedDeltaTime);

			}
		}


		else
		{
			foreach (var enemy in enemies)
			{
				var direction = (transform.position - enemy.transform.position).normalized;
//				enemy.gameObject.GetComponent<Rigidbody>().AddForce(direction * Time.fixedDeltaTime * absorbSpeed);
				enemy.GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
					transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f),
						Random.Range(-1f, 1f)), radius);
				enemy.GetComponent<Enemy>().TakeDamage(damage,stiffTimeAfterExplosion);
				mainCamera.GetComponent<CameraEffect>().StartShaking();

			}
			Destroy(gameObject);
		}
	}
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.black;
		Gizmos.DrawSphere(transform.position, radius);
	}
}