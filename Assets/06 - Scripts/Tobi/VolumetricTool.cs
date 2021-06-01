using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class VolumetricTool : MonoBehaviour
{
    [SerializeField]
    private PlaneTransform area;
    [SerializeField]
    private bool draw;
    [SerializeField]
    private Mesh mesh;
    [SerializeField]
    private bool previewMesh;
    private PlaneTransform oldArea;
    private Plane _plane;

    [System.Serializable]
    private struct PlaneTransform
    {
        public Vector3 position;
        public Vector2 scale;
    }

    private void OnDrawGizmos()
    {
        if (draw)
        {
            ReCalculatePlane();
            Gizmos.color = new Color(0, 1, 0,0.5f);
            Gizmos.DrawCube(area.position,new Vector3(area.scale.x *10,0.1f, area.scale.y *10));
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Vector3 hitpos = hitPlane(ray);
            if(hitpos != Vector3.zero)
            {
                Gizmos.DrawCube(hitpos, Vector3.one);
            }
            

        }
    }

    private void ReCalculatePlane()
    {
        if(area.position != oldArea.position)
        {
            _plane = new Plane(new Vector3(0, area.position.y, 0), new Vector3(1, area.position.y, 0), new Vector3(0, area.position.y, 1));
            oldArea = area;
        }
    }

    private Vector3 hitPlane(Ray pRay)
    {
        float dist;
        if (_plane.Raycast(pRay,out dist))
        {
            Vector3 output = pRay.origin + pRay.direction * dist;
            return output;
        }
        return Vector3.zero;
    }
}
