using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	const float START_RADIUS = 0.1f;
	const float DELTA_RADIUS = 0.0005f;
	const float DELTA_LENGTH = 0.1f;

	float pitch = 0;
	float yaw = 0;
	float roll = 0;
	int previousAge = 0;
	public int age = 0; // The age of the segment in frames.
	public int id = -1;


	// Use this for initialization
	void Start () {}

	// Update is called once per frame
	void Update () {}

	// Initializes the Stem segment
	void Init(Transform parentTransform, float offset, Stem stemPrefab, int age, float pitch, float yaw, float roll)
	{
		this.stemPrefab = stemPrefab;
		this.pitch = pitch;
		this.yaw = yaw;
		this.roll = roll;
		this.age = age;
		transform.SetParent(parentTransform);
		UpdateStemTransforms(offset);
	}

	// Updates the age of the stem
	public void UpdateStem(int age)
	{
		this.previousAge = this.age;
		this.age = age;
	}

	// Updates the stem's transforms based on its parent and its age
	void UpdateStemTransforms(float offset)
	{
		// Stem Transform Updates
		this.transform.Translate(0, offset, 0, this.transform.parent);
		if (age == 0)
		{
			this.transform.Rotate(pitch, yaw, roll);
		}

		// Cylinder Transform Updates
		float rad = START_RADIUS + (age * age * DELTA_RADIUS);
		float previousLen = previousAge * DELTA_LENGTH;
		float len = age * DELTA_LENGTH;
		Vector3 newScale = new Vector3(rad, len, rad);
		transform.GetChild(0).localScale = newScale;
		transform.GetChild(0).Translate(0, len - previousLen, 0);

		UpdateChildTransforms((len - previousLen) * 2);
	}

	// Updates all of its children based upon its new transforms
	public void UpdateChildTransforms(float offset)
	{
		if (age != previousAge)
		{
			foreach (Stem child in this.gameObject.GetComponentsInChildren<Stem>())
			{
				if (child != this && child.transform.parent == this.transform)
				{
					child.transform.SetParent(this.transform);
					child.UpdateStemTransforms(offset);
				}
			}
		}
	}

	// Creates a new stem segment with this stem as its parent
	public Stem AddStemSegment(int age, float pitch, float yaw, float roll)
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.Init(this.transform, transform.GetChild(0).localScale.y * 2, stemPrefab, age, pitch, yaw, roll);
		return stemClone;
	}
}
