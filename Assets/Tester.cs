using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tester : MonoBehaviour
{
    [SerializeField] GameObject anotherObject;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
       // rect.anchorMax-= new Vector2(0.00001f, 0.0001f);
    }
}
