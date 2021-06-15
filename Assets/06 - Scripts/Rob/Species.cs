using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Species", menuName = "Discoverables", order = 1)]
public class Species : ScriptableObject
{
    [SerializeField]
    private string speciesname;
    [SerializeField]
    private string description;
    [SerializeField]
    private Texture2D image;

    [SerializeField]
    private string UIText;
    [SerializeField]
    private Texture2D UIimage;

}
