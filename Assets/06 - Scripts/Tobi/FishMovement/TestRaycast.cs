using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
{
    public float dist;
    public int numOffRays;
    public float turnfration;
    public float highlightAngle;
    public bool drawUnHighlighted = false;
    public int highlightedRays;
    public static List<Vector3> plottedPoints;

    // Update is called once per frame
    void Start()
    {
        plottedPoints = new List<Vector3>();
        CalcRays();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12)) CalcRays();
        DrawRaysInOrder();
    }

    private void DrawRaysInOrder()
    {
        float percentile = 1f / plottedPoints.Count;

        for (int i = 0; i < plottedPoints.Count; i++)
        {
            Debug.DrawRay(transform.position, plottedPoints[i], new Color(percentile*i,percentile*i,percentile *i ));
        }
    }

    private void CalcRays()
    {
        Debug.DrawRay(transform.position, transform.forward * dist);

        highlightedRays = 0;
        for (int i = 0; i < numOffRays; i++)
        {
            float t = i / (numOffRays - 1f);
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = 2 * Mathf.PI * turnfration * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);

            DrawColloredRay(new Vector3(x, y, z));
        }
        plottedPoints.RemoveAt(0);
        SortPlotedPoints();
    }

    private void SortPlotedPoints()
    {
        if(plottedPoints.Count < 2)
        {
            Debug.LogWarning("Plotted poits are less than 2");
            return;
        }
        for (int j = plottedPoints.Count -1; j >0; j--)
        {
            for (int i = 0; i < j; i++)
            {
                if (Mathf.Abs(Vector3.Angle(plottedPoints[i], transform.forward)) > Mathf.Abs(Vector3.Angle(plottedPoints[i + 1], transform.forward)))
                {
                    SwapPoints(plottedPoints, i, i + 1);
                }
            }
            
        }
        debuggSort();
        
    }

    private void debuggSort()
    {
        string debugginfo = "Start \n";
        for (int i = 0; i < plottedPoints.Count; i++)
        {
            debugginfo += $"index = {i} Angle = {Mathf.Abs(Vector3.Angle(plottedPoints[i], transform.forward))} ";
        }
        Debug.Log(debugginfo);
    }

    private bool pointsAreSorted(List<Vector3> pList)
    {

        for (int i = 1; i < pList.Count -1; i++)
        {
            if(Mathf.Abs(Vector3.Angle(pList[i], transform.forward)) > Mathf.Abs(Vector3.Angle(pList[i-1], transform.forward))){
                return false;
            }
        }
        return true;
    }

    private void SwapPoints(List<Vector3> dataset, int indexM, int indexN)
    {
        Vector3 tempVec;
        tempVec = dataset[indexM];
        dataset[indexM] = dataset[indexN];
        dataset[indexN] = tempVec;
    }
    void DrawColloredRay( Vector3 point)
    {

        if (Mathf.Abs(Vector3.Angle(point, transform.forward)) < highlightAngle)
        {
            highlightedRays++;
            Debug.DrawRay(transform.position, point * dist, Color.blue);
            plottedPoints.Add(point);
        }
        else if (drawUnHighlighted)
        {
            Debug.DrawRay(transform.position * dist, point);
        }
    }
}
