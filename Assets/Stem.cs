using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	private Transform cylinder;
	const float START_RADIUS = 0.1f;
	const float DELTA_RADIUS = 0.01f;
	const float DELTA_LENGTH = 0.04f;
	int age = 1; // The age of the segment in frames.

	// Use this for initialization
	void Start () {
	}

	void Init(Transform parentTransform, Stem stemPrefab, float offset, int age, float pitch, float yaw, float roll)
	{
		transform.SetParent(parentTransform);

		this.stemPrefab = stemPrefab;
		this.age = age;
		CalcCylinder();

		this.transform.Translate(0, offset, 0);
		this.transform.Rotate(pitch, yaw, roll);

		float rad = START_RADIUS + (age * DELTA_RADIUS);
		float len = age* DELTA_LENGTH;
		Vector3 newScale = new Vector3(rad, len, rad);
		Vector3 newPos = new Vector3(0, len, 0);
		transform.GetChild(0).localScale = newScale;
		transform.GetChild(0).Translate(0, len, 0);
	}

	private void CalcCylinder()
	{
		foreach (Transform child in transform)
		{
			if (child.name == "Cylinder")
			{
				cylinder = child;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	}

	public Transform GetCylinder()
	{
		if (cylinder == null) {
			CalcCylinder();
		}
		return cylinder;
	}

	float GetTransformOffset()
	{
		float offset = GetCylinder().localScale.y * 2;
		return offset;
	}

	public Stem AddStemSegment(int age, float pitch, float yaw, float roll)
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.Init(this.transform, stemPrefab, GetTransformOffset(), age, pitch, yaw, roll);
		return stemClone;
	}
}
