using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Keep track of persistent objects by their names
    private static Dictionary<string, GameObject> persistentObjects = new Dictionary<string, GameObject>();

    // Ensure that the GameObject calling this script persists between scenes
    void Awake()
    {
        // Check if an object with the same name already exists
        if (persistentObjects.ContainsKey(gameObject.name))
        {
            // Destroy the new object to prevent duplication
            Destroy(gameObject);
        }
        else
        {
            // Add the object to the dictionary and make it persistent
            persistentObjects.Add(gameObject.name, gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}