using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	private Transform cylinder;
	const float START_RADIUS = 0.1f;
	const float DELTA_RADIUS = 0.01f;
	const float DELTA_LENGTH = 0.02f;
	const int MAX_AGE = 20;
	int age = 1; // The age of the segment in frames.

	// Use this for initialization
	void Start () {
	}

	void Init(Stem stemPrefab, float offset, int age)
	{
		this.stemPrefab = stemPrefab;
		this.age = age;
		CalcCylinder();


		float rad = START_RADIUS + (age * DELTA_RADIUS);
		float len = age * DELTA_LENGTH;
		Vector3 newScale = new Vector3(rad, len, rad);
		Vector3 newPos = new Vector3(0, len, 0);
		GetCylinder().localScale = newScale;
		GetCylinder().position = newPos;


		this.transform.Translate(0, offset, 0);
	}

	private void CalcCylinder()
	{
		Transform[] cylinderTransforms = gameObject.GetComponentsInChildren<Transform>();
		cylinder = cylinderTransforms[1];

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

	public Stem AddStemSegment(int age)
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.transform.SetParent(this.transform);
		stemClone.Init(stemPrefab, GetTransformOffset(), age);
		return stemClone;
	}
}
