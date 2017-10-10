using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class NameUI : MonoBehaviour
{
    public void SetName(string name)
    {
        GetComponent<Text>().text = name;
    }
}
