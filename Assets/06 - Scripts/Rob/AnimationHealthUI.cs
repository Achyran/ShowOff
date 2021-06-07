using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectMask2D))]
public class AnimationHealthUI : MonoBehaviour
{
    
    private Vector2Int softness;
    [Range(0.0f, 100.0f)]
    public float health = 100;

    private float softX;
    private float softY;

    private float healthPercentage;

    // Start is called before the first frame update
    void Start()
    {
        softness = gameObject.GetComponent<RectMask2D>().softness;
        softX = softness.x;
        softY = softness.y;


    }

    // Update is called once per frame
    void Update()
    {

        healthPercentage = health / 100f;
        
        softX = softX * (health / 100);
        softY = softY * (health / 100);
              
                       
        softness = new Vector2Int((int)softX, (int)softY);
       
        gameObject.GetComponent<RectMask2D>().softness = softness;






    }
}
