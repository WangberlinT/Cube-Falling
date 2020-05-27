using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapElement 
{
    private static GameObject mapPrefab = (GameObject)Resources.Load("Prefabs/UI/MapElement", typeof(GameObject));
    private GameObject mapElement;
    private Button button;
    private string name;
    public MapElement(string Name, GameObject mapContainer)
    {
        mapElement = GameObject.Instantiate(mapPrefab);
        mapElement.transform.SetParent(mapContainer.transform);
        Debug.Log(mapElement.name);
        button = mapElement.GetComponent<Button>();
        button.onClick.AddListener(SetMap) ;
        TextMeshProUGUI text = mapElement.GetComponentInChildren<TextMeshProUGUI>();
        name = Name;
        text.text = Name;
    }

    public void SetMap()
    {
        LoadMap.GetInstance().SetMap(name);
    }

    public void DistroyElement()
    {
        GameObject.Destroy(mapElement);
    }
}
