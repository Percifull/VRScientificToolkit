﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;


[System.Serializable]
public class EventParameter
{
    public string name;
    public System.Type systemType;
    public int typeIndex;
    public bool hideFromInspector;

    public void SetTypeFromIndex()
    {
        //p.systemType = System.Type.GetType(s);
        Debug.Log("Set Property");
        systemType = STKEventTypeChecker.allowedTypes[typeIndex];
    }
}

[CreateAssetMenu(menuName = "VR Scientific Toolkit/STKEvent")]
public class STKEvent : ScriptableObject
{
    public EventParameter[] parameters;
    public string eventName;
    private Hashtable objects = new Hashtable();
    private int uniqueID;
    private float time;

    public void AddParameter(string name, System.Type type)
    {
        if (parameters == null)
        {
            parameters = new EventParameter[1];
            parameters[0] = new EventParameter();
            parameters[0].name = name;
            parameters[0].systemType = type;
            parameters[0].hideFromInspector = true;
            return;
        }

        EventParameter[] oldParameters = parameters;
        parameters = new EventParameter[oldParameters.Length + 1];

        for (int i = 0; i < oldParameters.Length; i++)
        {
            parameters[i] = oldParameters[i];
        }

        parameters[parameters.Length - 1] = new EventParameter();
        parameters[parameters.Length - 1].name = name;
        parameters[parameters.Length - 1].systemType = type;
        parameters[parameters.Length - 1].hideFromInspector = true;
    }

    public void SetValue(string key, object value)
    {
        //Test if Key exists and Value is the correct Datatype
        foreach (EventParameter p in parameters)
        {
            if (p.systemType == null)
            {
                p.SetTypeFromIndex();
            }
            if (key == p.name)
            {
                if (value.GetType() == p.systemType)
                {
                    objects.Add(key, value);
                }
            }
        }
        
    }

}
