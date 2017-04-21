using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stem : MonoBehaviour {
	public Stem stemPrefab;
	int age = 0;
	public List<Stem> children = new List<Stem>();

	// Use this for initialization
	void Start () {

	}

	void Init (Stem stemPrefab, List<Stem> children)
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

	public void GrowChildren()
	{
		Debug.Log("Growing Children");
		foreach (Stem child in children)
		{
			child.Grow();
		}
	}

	public virtual void Grow ()
	{
		Debug.Log("Stem Growing");
		age++;
		GrowChildren();

		Stem stemClone = Instantiate<Stem>(stemPrefab, transform.position, transform.rotation);
		stemClone.transform.SetParent(this.transform);
		stemClone.Init(stemPrefab, children);
		stemClone.transform.Translate(0, 0.4f, 0);
		this.children.Clear();
		this.children.Add(stemClone);
	}
}
