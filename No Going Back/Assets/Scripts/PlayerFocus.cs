using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFocus : MonoBehaviour
{
    bool lookAtShip;
    Quaternion shipQuaternion;
    Transform target;
    Vector3 shipViewPosition;
    int shipZoom;
    bool zoomIn;
    bool zoomOut;
    int fov;
    public Image topBar;
    public Image bottomBar;
    public static bool barsIn, barsOut;
    float barsRate;

    void Start ()
    {
        barsRate = 0.03f;
        target = GameObject.FindGameObjectWithTag("Ship").transform;
        shipQuaternion = new Quaternion(0.0f, -0.5f, 0.0f, 0.9f);
        shipZoom = 10;
        fov = 60;
        shipViewPosition = new Vector3(197, 23, 80);
    }
	
	void Update ()
    {
        if(barsIn)
        {
            topBar.fillAmount += barsRate;
            bottomBar.fillAmount += barsRate;
            if (topBar.fillAmount >= 1f && bottomBar.fillAmount >= 1f)
            {
                barsIn = false;
            }
        }
        if (barsOut)
        {
            topBar.fillAmount -= barsRate;
            bottomBar.fillAmount -= barsRate;
            if (topBar.fillAmount <= 0 && bottomBar.fillAmount <= 0)
            {
                barsOut = false;
            }
        }
        if (lookAtShip)
        {
            Vector3 targetDir = target.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = 1 * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);

            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localEulerAngles = Vector3.zero;

            transform.localPosition = shipViewPosition;

            if(transform.rotation == shipQuaternion && !zoomOut)
            {
                zoomIn = true;
            }

            if(zoomIn)
            {
                if(Camera.main.fieldOfView<=shipZoom)
                {
                    zoomIn = false;
                    Invoke("ZoomOut", 3);
                }
                else
                {
                    Camera.main.fieldOfView -= 0.3f;
                }
            }

            if(zoomOut)
            {
                if(Camera.main.fieldOfView >= fov)
                {
                    BarsOut();
                    zoomOut = false;
                    lookAtShip = false;
                    PlayerLook.freezeLook = false;
                    PlayerMove.freezeMove = false;
                }
                else
                {
                    Camera.main.fieldOfView += 0.3f;
                }
            }
            //print(transform.rotation);
        }
	}

    void ZoomOut()
    {
        zoomOut = true;
    }

    public void LookAtShip()
    {
        lookAtShip = true;
        BarsIn();
    }

    void BarsIn()
    {
        barsIn = true;
    }

    void BarsOut()
    {
        barsOut = true;
    }
}
