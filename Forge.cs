﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forge : MonoBehaviour {

    Camera_Manipulator cm;

    public GameObject selected;
    GameObject canvas;
    Camera cam;

    float xSpd = .05f;
    float ySpd = .05f;
    float rotationSpeed = 1f;

    public bool rotationSnap = true;

    int numColors;
    Color[] c;
    void StoreColors(GameObject p)
    {
        foreach(Transform child in p.transform)
        {
            numColors++;
        }
        c = new Color[numColors + 1];
        for(int i = 0; i < numColors; i++)
        {
            c[i] = p.transform.GetChild(i).GetComponent<Renderer>().material.color;
        }
    }
    void CallColor(GameObject p)
    {
        for(int i = 0; i < numColors; i++)
        {
            p.transform.GetChild(i).GetComponent<Renderer>().material.color = c[i];
        }
        numColors = 0;
        c = null;
    }

    void SpawnMenu()
    {
        cm.RightMenu();
        canvas.SetActive(true);
    }

    public void SpawnObject(GameObject g)
    {
        canvas.SetActive(false);
        cm.Revert();
        selected = Instantiate(g, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0,0,0)));
        StoreColors(selected);
        foreach (Transform child in selected.transform)
        {
            child.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void Drop()
    {
        CallColor(selected);
        selected = null;
        SpawnMenu();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Start () {
        canvas = GameObject.Find("Canvas");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cm = this.gameObject.GetComponent<Camera_Manipulator>();
        //SpawnMenu();
	}
	
	
	void Update () {
        if (selected)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
            if(Input.GetKey(KeyCode.LeftShift))
                selected.transform.Rotate(0, (Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X")) * rotationSpeed, 0);
            else
                selected.transform.Translate((Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X") - Input.GetAxis("Mouse Y"))  * xSpd, 0, (Input.GetAxis("Vertical") + Input.GetAxis("Mouse Y") + Input.GetAxis("Mouse X")) * ySpd, Space.World);
            if (rotationSnap && Input.GetKeyUp(KeyCode.LeftShift))
                selected.transform.Rotate(selected.transform.rotation.x % 5, selected.transform.rotation.y % 5, selected.transform.rotation.z % 5);
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Drop();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider.tag != "NoClick")
                {
                    selected = hit.collider.gameObject;
                    StoreColors(selected);
                    foreach (Transform child in selected.transform)
                    {
                        child.GetComponent<Renderer>().material.color = Color.red;
                    }
                }
                
            }
        }
	}
}
