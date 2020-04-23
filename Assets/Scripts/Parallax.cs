using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    [SerializeField] private float backgroundScrollSpeed = 0.02f;

    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;   
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += new Vector2(0, backgroundScrollSpeed) * Time.deltaTime;
    }
}
