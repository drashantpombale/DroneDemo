using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{

    public Button start;

    // Start is called before the first frame update
    void Start()
    {
        start = GetComponent<Button>();
        start.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
