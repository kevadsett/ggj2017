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

	private Vector3 lastPos;
	private Vector3 newPos;
	private Quaternion lastRot;
	private Quaternion newRot;
	private float timer;

	public void InitAtPoint (Vector3 point)
	{
		lastPos = newPos = point;
		lastRot = newRot = Quaternion.identity;
		timer = 0.0f;
	}

	public void MoveToPoint (Vector3 point)
	{
		lastPos = posXform.localPosition;
		newPos = point;

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
		DEBUG_INPUT ();

		timer += Time.deltaTime * moveSpeed;
		timer = Mathf.Clamp01 (timer);

		float v = speedCurve.Evaluate (timer);
		float bounce = bounceHeight * heightCurve.Evaluate (v);

		posXform.localPosition = Vector3.Lerp (lastPos, newPos, v);
		rotXform.localRotation = Quaternion.Slerp (lastRot, newRot, v);
		hopXform.localPosition = new Vector3 (0.0f, bounce, 0.0f);
	}

	[Conditional ("USE_DEBUG_INPUT")]
	private void DEBUG_INPUT ()
	{
		if (timer < 1.0f) return;

		if (Input.GetKey (KeyCode.W)) MoveToPoint (posXform.localPosition + Vector3.forward);
		if (Input.GetKey (KeyCode.A)) MoveToPoint (posXform.localPosition + Vector3.left);
		if (Input.GetKey (KeyCode.D)) MoveToPoint (posXform.localPosition + Vector3.right);
		if (Input.GetKey (KeyCode.S)) MoveToPoint (posXform.localPosition + Vector3.back);
	}
}