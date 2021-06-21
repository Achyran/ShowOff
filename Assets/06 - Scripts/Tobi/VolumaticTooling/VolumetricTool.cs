
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class VolumetricTool : MonoBehaviour
{
    [SerializeField]
    private bool draw;
    [SerializeField]
    private PlaneTransform area;
    [SerializeField]
    private GameObject prefab;
    private Mesh mesh;
    [SerializeField]
    private bool preview;
    [SerializeField]
    private Vector3 meshRotation;
    [SerializeField]
    private Vector3 meshScale;
    [SerializeField]
    private GameObject targetParent;

    private PlaneTransform oldArea;
    private Plane _plane;
    private GameObject _oldPrefab;

    [System.Serializable]
    public struct PlaneTransform
    {
        public Vector3 position;
        public Vector2 scale;
    }


    private void OnDrawGizmos()
    {
        if (draw)
        {

            ReCalculatePlane();
            UpdatePrefab();
            Gizmos.color = new Color(0, 1, 0,0.3f);
            Gizmos.DrawCube(area.position,new Vector3(area.scale.x ,0.1f, area.scale.y ));
            Ray ray = new Ray();// HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            Vector3 hitpos = hitPlane(ray);
            if (preview) DrawPreview();
            if(hitpos != Vector3.zero)
            {
                if (mesh == null)
                    Gizmos.DrawCube(hitpos, Vector3.one);
                else
                {
                    Gizmos.DrawMesh(mesh,hitpos,Quaternion.Euler(meshRotation), meshScale);

                    Event e = Event.current;

                    if (e.type == EventType.MouseUp)
                    {
                        InstanPrefab(hitpos);
                    }
                }
            }
        }
    }

    private void DrawPreview()
    {
        
        if (mesh != null)
        {
            mesh.RecalculateNormals();
            Gizmos.DrawMesh(mesh, area.position, Quaternion.Euler(meshRotation), meshScale);
        }
    }


    private void InstanPrefab(Vector3 poss)
    {
        if(targetParent == null)
        {
            Debug.LogError("Object could not be instancated, Target parent was Null", this);
            return;
        }
        GameObject obj = Instantiate(prefab, targetParent.transform);
        obj.transform.localScale = meshScale;
        obj.transform.localPosition = poss - targetParent.transform.position;
        obj.transform.localRotation = Quaternion.Euler( meshRotation);

    }

    private void UpdatePrefab()
    {
        if(prefab != _oldPrefab)
        {
            _oldPrefab = prefab;
            meshScale = prefab.transform.localScale;
            meshRotation =  prefab.transform.localRotation.eulerAngles;
            MeshFilter meshFilter = prefab.GetComponent<MeshFilter>();
            mesh = meshFilter.sharedMesh;
            if (mesh != null)
            {
                mesh.RecalculateNormals();
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
            if (isInRec(new Vector2(output.x, output.z))) return output;
        }
        return Vector3.zero;
    }

    private bool isInRec(Vector2 vec)
    {

        if((area.position.x- area.scale.x/2) < vec.x && vec.x < (area.position.x + area.scale.x/2) &&
            (area.position.z - area.scale.y / 2) < vec.y && vec.y < (area.position.z + area.scale.y / 2))
        return true;
        return false;
    }
}
