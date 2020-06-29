using System.Collections.Generic;
using UnityEngine;

public class SceneGameObjects : MonoBehaviour
{
    [SerializeField] private GameObject selectedGameObject;
    [SerializeField] private List<GameObject> sceneGameObjectList;

    public GameObject SelectedGameObject => selectedGameObject;

    private void Awake()
    {
        sceneGameObjectList = new List<GameObject>();
        Movement[] movingObjects = FindObjectsOfType<Movement>();

        if (movingObjects != null && movingObjects.Length > 0)
        {
            foreach (Movement item in movingObjects)
            {
                if (!sceneGameObjectList.Contains(item.gameObject))
                    sceneGameObjectList.Add(item.gameObject);
            }
        }
    }

    private void Start()
    {
        if (!sceneGameObjectList.IsNullOrEmpty())
            Select(sceneGameObjectList[0].name);
    }

    public bool SetSelectedGameObject(string objectName)
    {
        GameObject go = sceneGameObjectList.FindObjectByName(objectName);
        selectedGameObject = go ? go : selectedGameObject;
        return go != null;
    }

    public bool Select(string objectName)
    {
        bool success = SetSelectedGameObject(objectName);
        if (success)
            HighlightSelectedGameObject();
        return success;
    }

    private void HighlightSelectedGameObject()
    {
        foreach (GameObject item in sceneGameObjectList)
        {
            item.GetComponent<HighlightObject>().SetHighlight(false);
        }
        selectedGameObject.GetComponent<HighlightObject>().SetHighlight(true);
    }
}