using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomSpawn : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject spherePrefab;
    public InputField amountInput;
    public InputField maxSizeInput;



    private int amount;
    private float maxSize;
    private const float minSize = 10;
    private float min_Z = 900;
    private float max_Z = 1500;
    private float width;
    private float frustumWidth;
    private float height;
    private float frustumHeight;
    private Vector3 originPosition;
    private List<GameObject> sphereList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        frustumHeight = (float)(2.0 * min_Z * Math.Tan(mainCamera.fieldOfView * 0.5 * Mathf.Deg2Rad));
        frustumWidth = (float)(frustumHeight * mainCamera.aspect);
        originPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateRandomSphere(){
        // float range_X = UnityEngine.Random.Range((width / 2) * -1, width / 2);
        // float range_Y = UnityEngine.Random.Range((height / 2) * -1, height / 2);
        float range_X = UnityEngine.Random.Range((frustumWidth / 2) * -1, frustumWidth / 2);
        float exceptiveArea = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, Screen.height - 188)).z;
        float range_Y = UnityEngine.Random.Range((frustumHeight / 2) * -1, (frustumHeight / 2) - exceptiveArea);
        float range_Z = UnityEngine.Random.Range(min_Z, max_Z);
        Vector3 randomPosition = new Vector3(range_X, range_Y, range_Z);
        Vector3 createPosition = originPosition + randomPosition;

        float randomSize = UnityEngine.Random.Range(minSize, maxSize);
        Vector3 randomScale = new Vector3(randomSize, randomSize, randomSize);
         
        GameObject instantSphere = Instantiate(spherePrefab, createPosition, Quaternion.identity);
        instantSphere.transform.localScale = randomScale;

        sphereList.Add(instantSphere);
    }

    public void CreateButton_OnClick(){
        Debug.Log("Create Button Click !!!!!!!!!!!!!!");

        amount = int.Parse(amountInput.text);
        maxSize = float.Parse(maxSizeInput.text);

        for (int i=0; i<amount; i++){
            CreateRandomSphere();
        }
    }

    public void DeleteButton_OnClick(){
        Debug.Log("Delete Button Click !!!!!!!!!!!!!!");
        foreach(var shpere in sphereList){
            Destroy(shpere);
        }
        
        sphereList.Clear();
    }
}
