using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class RandomSpawn : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject spherePrefab;
    public TMP_Text numPrefab;
    public InputField amountInput;
    public InputField maxSizeInput;
    public LineRenderer lineRenderer;



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
    private List<TMP_Text> numberList = new List<TMP_Text>();
    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width;
        height = Screen.height;
        frustumHeight = (float)(2.0 * min_Z * Math.Tan(mainCamera.fieldOfView * 0.5 * Mathf.Deg2Rad));
        frustumWidth = (float)(frustumHeight * mainCamera.aspect);
        originPosition = mainCamera.transform.position;

        // Line Renderer
        lineRenderer = lineRenderer.GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.material.color = Color.yellow;
        lineRenderer.startWidth = 10f;
        lineRenderer.endWidth = 10f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateRandomSphere(){
        float range_X = UnityEngine.Random.Range((width / 2) * -1, width / 2);
        float range_Y = UnityEngine.Random.Range((height / 2) * -1, height / 2);
        //float range_X = UnityEngine.Random.Range((frustumWidth / 2) * -1, frustumWidth / 2);
        //float exceptiveArea = mainCamera.ScreenToWorldPoint(new Vector3(0, 1000, 0)).y;
        //float range_Y = UnityEngine.Random.Range((frustumHeight / 2) * -1, (frustumHeight / 2) - exceptiveArea);
        float range_Z = UnityEngine.Random.Range(min_Z, max_Z);
        Vector3 randomPosition = new Vector3(range_X, range_Y, range_Z);
        Vector3 createPosition = originPosition + randomPosition;
        Vector3 numberTextPosition = createPosition + new Vector3(0, 5, 0);

        float randomSize = UnityEngine.Random.Range(minSize, maxSize);
        Vector3 randomScale = new Vector3(randomSize, randomSize, randomSize);
         
        GameObject instantSphere = Instantiate(spherePrefab, createPosition, Quaternion.identity);
        instantSphere.transform.localScale = randomScale;
        sphereList.Add(instantSphere);

        TMP_Text instantTextMesh = Instantiate(numPrefab, numberTextPosition, Quaternion.identity);
        instantTextMesh.text = $"# {sphereList.Count}";
        numberList.Add(instantTextMesh);

        CreateConstellationLine();
    }

    private void CreateConstellationLine(){
        if (sphereList.Count > 0){
            lineRenderer.enabled = true;
            lineRenderer.positionCount = sphereList.Count;

            int currentIndex = sphereList.Count - 1;
            Vector3 currentPosition = sphereList[currentIndex].GetComponent<Transform>().position;

            lineRenderer.SetPosition(currentIndex, currentPosition);
        }
    }

    public void CreateButton_OnClick(){
        amount = int.Parse(amountInput.text);
        maxSize = float.Parse(maxSizeInput.text);

        for (int i=0; i<amount; i++){
            CreateRandomSphere();
        }
    }

    public void DeleteButton_OnClick(){
        foreach(var shpere in sphereList){
            Destroy(shpere);
        }
        
        foreach(var text in numberList){
            Destroy(text.gameObject);
        }
        
        sphereList.Clear();
        numberList.Clear();

        lineRenderer.enabled = false;
    }
}
