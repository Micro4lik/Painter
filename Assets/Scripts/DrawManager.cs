using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawManager : MonoBehaviour
{
    public GameObject drawPrefab;
    private GameObject trail;
    private Plane planeObj;
    private Vector3 startPos;

    private List<GameObject> trailsList;

    private int orderInLayer;

    private TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        planeObj = new Plane(Camera.main.transform.forward * -1, transform.position);
        trailRenderer = drawPrefab.GetComponent<TrailRenderer>();
        orderInLayer = 0;

        trailsList = new List<GameObject>();
    }

    public void SetColor(string color)
    {
        switch (color)
        {
            case "green":
                trailRenderer.startColor = Color.green;
                trailRenderer.endColor = Color.green;
                break;

            case "red":
                trailRenderer.startColor = Color.red;
                trailRenderer.endColor = Color.red;
                break;

            case "black":
                trailRenderer.startColor = Color.black;
                trailRenderer.endColor = Color.black;
                break;
        }
    }

    public void Clear()
    {
        foreach (GameObject trail in trailsList)
        {
            trail.GetComponent<TrailRenderer>().Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            Ray mousRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (planeObj.Raycast(mousRay, out _dis))
            {
                startPos = mousRay.GetPoint(_dis);
            }

            trail = Instantiate(drawPrefab, startPos, Quaternion.identity);
            trail.GetComponent<TrailRenderer>().sortingOrder = orderInLayer;
            trailsList.Add(trail);

            orderInLayer++;
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetMouseButton(0))
        {
            Ray mousRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _dis;
            if (planeObj.Raycast(mousRay, out _dis))
            {
                trail.transform.position = mousRay.GetPoint(_dis);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
