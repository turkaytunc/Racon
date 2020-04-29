using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 0.02f;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;   
    }

    void Update()
    {
        material.mainTextureOffset += new Vector2(0, backgroundScrollSpeed) * Time.deltaTime;
    }
}
