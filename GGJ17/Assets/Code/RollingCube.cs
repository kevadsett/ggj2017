using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class RollingCube : MonoBehaviour
{
	public float moveSpeed;
	public AnimationCurve speedCurve;
	public AnimationCurve heightCurve;
	public float bounceHeight;
	public Transform posXform;
	public Transform hopXform;
	public Transform rotXform;
	public Transform shadowXform;
	public bool drowned = false;

	private Vector3 lastPos;
	private Vector3 newPos;
	private Quaternion lastRot;
	private Quaternion newRot;
	private float timer;
	private float extraBounce;


	public void InitAtPoint (Vector3 point)
	{
		lastPos = newPos = point;
		lastRot = newRot = Quaternion.identity;
		timer = 0.0f;
	}

	public void MoveToPoint (Vector3 point, float extraBounce)
	{
		lastPos = posXform.localPosition;
		newPos = point;
		this.extraBounce = extraBounce;

		lastRot = rotXform.localRotation;

		Vector3 delta = newPos - lastPos;
		Vector3 euler = Vector3.zero;

		if (delta.x >  0.5f) euler.z -= 90.0f;
		if (delta.x < -0.5f) euler.z += 90.0f;
		if (delta.z >  0.5f) euler.x += 90.0f;
		if (delta.z < -0.5f) euler.x -= 90.0f;

		// stability hack
		euler.x = Mathf.Round (euler.x / 90.0f) * 90.0f;
		euler.y = Mathf.Round (euler.y / 90.0f) * 90.0f;
		euler.z = Mathf.Round (euler.z / 90.0f) * 90.0f;

		newRot = Quaternion.Euler (euler) * lastRot;

		timer = 0.0f;
	}

	private void Update ()
	{
		if (drowned)
			return;

		timer += Time.deltaTime * moveSpeed;
		timer = Mathf.Clamp01 (timer);

		float v = speedCurve.Evaluate (timer);
		float bounce = bounceHeight * heightCurve.Evaluate (v);

		posXform.localPosition = Vector3.Lerp (lastPos, newPos, v);
		rotXform.localRotation = Quaternion.Slerp (lastRot, newRot, v);
		hopXform.localPosition = new Vector3 (0.0f, bounce * extraBounce, 0.0f);
		shadowXform.localScale = Vector3.one * (1.0f - bounce * 0.75f);
	}
}