using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthTexture : MonoBehaviour
{
    Camera cam;

	void Start ()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
	}
	
	void Update ()
    {
		
	}
}
