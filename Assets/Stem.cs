using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
	}

	public Stem AddStemSegment()
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.transform.SetParent(this.transform);
		stemClone.stemPrefab = this.stemPrefab;
		stemClone.transform.Translate(0, 0.4f, 0);

		return stemClone;
	}
}
