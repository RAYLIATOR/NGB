using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Crafting : MonoBehaviour
{
    Animator anim;
    Camera cam;
    GameObject axe;
    public GameObject hand;
    public GameObject logPrefab;
    Vector3 logPosition;
    Vector3 treePosition;
    public GameObject woodEffect1;
    public bool inZone;
    public GameObject xMark;
    public GameObject xInstruction;
    bool marked;
    int logsPlaced;
    int logs;
    bool hasAxe;
    public GameObject currentLog;
    public GameObject[] placedLogs;

	void Start ()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        logPosition = new Vector3(3.5f,0,-5);
        logsPlaced = 0;
	}

    void Update()
    {
        if (inZone && Input.GetKeyDown(KeyCode.M) && !marked)
        {
            xInstruction.SetActive(false);
            xMark.SetActive(true);
            marked = true;
        }
        if (inZone && Input.GetKeyDown(KeyCode.B) && marked && logs>0)
        {
            Destroy(currentLog);
            placedLogs[logsPlaced].SetActive(true);
            logsPlaced += 1;
            logs -= 1;
            if(logsPlaced >= 7)
            {
                SceneManager.LoadScene(2);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tree" && Input.GetKeyDown(KeyCode.P) && hasAxe && logs == 0)
        {
            PlayerFocus.barsIn = true;
            PlayerLook.freezeLook = true;
            PlayerMove.freezeMove = true;
            transform.LookAt(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z));
            if(Vector3.Distance(new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), transform.position) < 2f)
            {
                transform.position -= transform.forward * 2;
            }
            treePosition = other.transform.position;
            cam.transform.localEulerAngles = new Vector3(50f, 0, 0);
            anim.SetTrigger("Chop");
            Destroy(other.gameObject, 4.8f);
            Invoke("PickUpLog", 4.8f);
            Invoke("UnPause", 4.8f);
        }
        if (other.tag == "Axe" && Input.GetKeyDown(KeyCode.P))
        {
            PlayerFocus.barsIn = true;
            PlayerLook.freezeLook = true;
            PlayerMove.freezeMove = true;
            transform.position = new Vector3(90.5f, 11.672f, 78.4f);
            transform.localEulerAngles = new Vector3(0, 117, 0);
            cam.transform.localEulerAngles = new Vector3(7f, 0, 0);
            anim.SetTrigger("Pick");
            axe = other.gameObject;
            Invoke("PickUpAxe", 2.17f);
            Invoke("UnPause", 7f);
            //Invoke("PauseEditor", 7.43f);
            Destroy(axe.GetComponent<Rigidbody>());
            Destroy(axe.GetComponent<BoxCollider>());
        }
    }

    void Axe()
    {

    }

    void PauseEditor()
    {
        Debug.Break();
    }

    void PickUpLog()
    {
        GameObject log = Instantiate(logPrefab, transform.position, Quaternion.identity);
        log.transform.parent = transform;
        log.transform.localPosition = logPosition;
        log.transform.localEulerAngles = Vector3.zero;
        currentLog = log;
        logs += 1;
        Invoke("WoodEffect1", 0);
    }

    void WoodEffect1()
    {
        Instantiate(woodEffect1, treePosition, Quaternion.Euler(-90,0,0));
    }

    void PickUpAxe()
    {
        hasAxe = true;
        axe.transform.parent = hand.transform;
        axe.transform.localPosition = new Vector3(0.563f, -0.316f, 0.025f);
    }

    void UnPause()
    {
        PlayerLook.freezeLook = false;
        PlayerMove.freezeMove = false;
        PlayerFocus.barsOut = true;
    }
}
