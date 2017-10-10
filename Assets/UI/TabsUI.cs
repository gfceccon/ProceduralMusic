using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class TabsUI : MonoBehaviour
{
    private GameObject current;
    private List<GameObject> tabs;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int index = tabs.FindIndex(go => go == current);
            index = (index + 1) % tabs.Count;
            current.SetActive(false);

            current = tabs[index];
            current.SetActive(true);
        }
    }

    public void AddTab(GameObject tab)
    {
        if(tabs == null)
            tabs = new List<GameObject>();
        tabs.Add(tab);
        if (current != null)
            tab.SetActive(false);
        else
            current = tab;
        tab.transform.SetParent(transform, false);
        tab.transform.SetAsFirstSibling();
    }

    public void RemoveTab(GameObject tab)
    {
        if (tabs == null)
            return;
        tabs.Remove(tab);

        if (tab == current)
        {
            current.SetActive(false);
            current = tabs[0];
            current.SetActive(true);
        }

        Destroy(tab);
    }
}
