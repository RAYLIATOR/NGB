using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjects : MonoBehaviour
{
    public GameObject pineTree;
	void Start ()
    {
        ReplaceObjects("Tree");
	}
	
	void Update ()
    {
		
	}

    void ReplaceObjects(string tag)
    {
        List<GameObject> objects = new List<GameObject>();
        objects.AddRange(GameObject.FindGameObjectsWithTag(tag));
        for (int i = 0; i < objects.Count; i++)
        {
            Instantiate(pineTree, objects[i].transform.position, objects[i].transform.rotation);
            Destroy(objects[i]);
        }
    }

    void RandomRotate(string tag)
    {
        List<GameObject> objects = new List<GameObject>();
        objects.AddRange(GameObject.FindGameObjectsWithTag(tag));
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.Rotate(0, Random.Range(0f,360f), 0);
        }
    }
}
