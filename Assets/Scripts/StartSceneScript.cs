﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneScript : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(Scene.FirstLevel, LoadSceneMode.Single);
    }
}
