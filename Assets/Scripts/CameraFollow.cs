using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Vector3 offset;
	public Vector3 offsetIn3D;
	public float smoothSpeed = 0.125f;
	public Transform target;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void FixedUpdate()
	{
		Vector3 desiredPosition;
		if (GameManager.Instance.is3D)
		{
			mainCamera.orthographic = false;

			desiredPosition = target.position + offsetIn3D;
		}
		else
		{
			mainCamera.orthographic = true;
			desiredPosition = target.position + offset;
		}
		var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		if (GameManager.Instance.is3D)
		{
		}
		else
		{
			transform.rotation = Quaternion.identity;
		}
	}
}