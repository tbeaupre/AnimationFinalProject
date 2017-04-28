using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	private Transform cylinder;
	const float RADIUS = 0.1f;
	const int MAX_AGE = 20;
	int age = 1; // The age of the segment in frames.

	// Use this for initialization
	void Start () {
	}

	void Init(Stem stemPrefab, float offset)
	{
		this.stemPrefab = stemPrefab;
		this.transform.Translate(0, offset, 0);
		CalcCylinder();
	}

	private void CalcCylinder()
	{
		Transform[] cylinderTransforms = gameObject.GetComponentsInChildren<Transform>();
		cylinder = cylinderTransforms[1];

	}

	// Update is called once per frame
	void Update () {
		float rad = age * RADIUS;
		Vector3 newScale = new Vector3(rad, GetCylinder().localScale.y, rad);
		GetCylinder().localScale = newScale;
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

	public Stem AddStemSegment()
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.transform.SetParent(this.transform);
		stemClone.Init(stemPrefab, GetTransformOffset());

		return stemClone;
	}
}
