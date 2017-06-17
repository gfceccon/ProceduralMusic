using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{

	void Start ()
	{
        Note n = new Note(ENote.A);
        print(n.ToFreq());
	}
	
	void Update ()
	{
	}
}
