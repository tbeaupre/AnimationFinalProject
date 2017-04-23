using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	public List<Stem> children = new List<Stem>();

	// Use this for initialization
	void Start () {

	}

	public void Init (Stem stemPrefab, List<Stem> children)
	{
		this.stemPrefab = stemPrefab;
		foreach (Stem child in children)
		{
			this.children.Add(child);
			child.transform.SetParent(this.transform);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void DestroyChildren()
	{
		foreach (Stem child in children)
		{
			child.DestroyChildren();
		}
		Destroy(this.gameObject);
	}

	public Stem AddStemSegment()
	{
		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.transform.SetParent(this.transform);
		stemClone.stemPrefab = this.stemPrefab;
		stemClone.transform.Translate(0, 0.4f, 0);

		this.children.Add(stemClone);

		return stemClone;
	}
}
