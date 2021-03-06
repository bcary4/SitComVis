﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Page_Changer : MonoBehaviour {

    GameObject page1;
    GameObject page2;

    //gets pages and stores them
    private void Start()
    {
        page1 = this.transform.GetChild(0).gameObject;
        page2 = this.transform.GetChild(1).gameObject;
    }

    int page = 1;

    //goes up a page
    public void PageUp()
    {
        if (page1.activeSelf)
        {
            page1.SetActive(false);
            page2.SetActive(true);
            page++;
        }
        if (page2.activeSelf)
        {
            page2.SetActive(false);
            page1.SetActive(true);
            page = 0;
        }

    }

    //goes down a page
    public void PageDown()
    {
        if (page1.activeSelf)
        {
            page1.SetActive(false);
            page2.SetActive(true);
            page = 2;
        }
        if (page2.activeSelf)
        {
            page2.SetActive(false);
            page1.SetActive(true);
            page = 0;
        }
    }

}
