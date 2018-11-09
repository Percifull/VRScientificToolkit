﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class STKTestController : MonoBehaviour {

    public GameObject verticalGroup; //Parent object for spawned selections
    public GameObject propertyPrefab;
    public GameObject startButton;

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

    // Use this for initialization
    void Awake () {
        testStages = Array.ConvertAll(STKArrayTools.ClearNullReferences(testStages), item => item as GameObject);
        numberOfStages = testStages.Length;
        testStages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	}

    public void AddStage()
    {
        GameObject newStage = Instantiate(stagePrefab);
        newStage.transform.parent = verticalGroup.transform;
        stagePrefab.GetComponent<STKTestStage>().myController = gameObject.GetComponent<STKTestController>();
        testStages = Array.ConvertAll(STKArrayTools.AddElement(newStage,testStages), item => item as GameObject);
        testStages = Array.ConvertAll(STKArrayTools.ClearNullReferences(testStages), item => item as GameObject);
    }

    public void StageEnded()
    {
        testStages[currentStage].SetActive(false);
        if ((currentStage+1) != testStages.Length && testStages[(currentStage+1)] != null)
        {
            currentStage++;
            testStages[currentStage].SetActive(true);
        }
    }
    
}
