﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class STKTestController : MonoBehaviour {

    public GameObject verticalGroup; //Parent object for spawned selections
    public GameObject inputPropertyPrefab;
    public GameObject togglePropertyPrefab;
    public GameObject buttonPrefab;
    //public GameObject startButton;

    public GameObject[] startActivateObjects;

    [SerializeField]
    public GameObject[] testStages;
    public static int numberOfStages;
    public GameObject stagePrefab;

    [SerializeField]
    private List<STKTestControllerProperty> properties = new List<STKTestControllerProperty>();
    private Hashtable values = new Hashtable();
    private static float time;
    private int currentStage = 0;

    private bool hasTimeLimit;
    
    void Awake () {
        testStages = Array.ConvertAll(STKArrayTools.ClearNullReferences(testStages), item => item as GameObject);
        numberOfStages = testStages.Length;
        foreach (GameObject g in testStages)
        {
            g.SetActive(false);
        }
        testStages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	}

    public void AddStage()
    {
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        GameObject newStage = Instantiate(stagePrefab);
        newStage.transform.parent = verticalGroup.transform;
        newStage.GetComponent<STKTestStage>().myController = gameObject.GetComponent<STKTestController>();
        testStages = Array.ConvertAll(STKArrayTools.AddElement(newStage,testStages), item => item as GameObject);
        testStages = Array.ConvertAll(STKArrayTools.ClearNullReferences(testStages), item => item as GameObject);
        foreach (GameObject g in testStages)
        {
            g.SetActive(false);
        }
        testStages[testStages.Length-1].SetActive(true);
    }

    public void StageEnded()
    {
        testStages[currentStage].SetActive(false);
        if ((currentStage+1) != testStages.Length && testStages[(currentStage+1)] != null)
        {
            currentStage++;
            testStages[currentStage].SetActive(true);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}
