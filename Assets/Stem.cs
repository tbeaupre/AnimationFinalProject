using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	private Transform cylinder;
	const float START_RADIUS = 0.1f;
	const float DELTA_RADIUS = 0.01f;
	const float DELTA_LENGTH = 0.04f;
	float pitch = 0;
	float yaw = 0;
	float roll = 0;
	int age = 1; // The age of the segment in frames.

	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}

	void Init(Transform parentTransform, Stem stemPrefab, int age, float pitch, float yaw, float roll)
	{
		this.stemPrefab = stemPrefab;
		transform.SetParent(parentTransform);
		this.pitch = pitch;
		this.yaw = yaw;
		this.roll = roll;
		this.age = age;
		UpdateStemTransforms();
	}

	public void UpdateStem(int age)
	{
		this.age = age;
		UpdateStemTransforms();
	}

	void UpdateStemTransforms()
	{
		// Stem Transform Updates
		float offset = transform.parent.localScale.y * 2;
		this.transform.Translate(0, offset, 0);
		this.transform.Rotate(pitch, yaw, roll);

		// Cylinder Transform Updates
		float rad = START_RADIUS + (age * age * DELTA_RADIUS);
		float len = age * DELTA_LENGTH;
		Vector3 newScale = new Vector3(rad, len, rad);
		Vector3 newPos = new Vector3(0, len, 0);
		transform.GetChild(0).localScale = newScale;
		transform.GetChild(0).Translate(0, len, 0);
	}

	public Stem AddStemSegment(int age, float pitch, float yaw, float roll)
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.Init(this.transform, stemPrefab, age, pitch, yaw, roll);
		return stemClone;
	}
}
