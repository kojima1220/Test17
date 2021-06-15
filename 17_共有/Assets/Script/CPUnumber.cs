using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPUnumber : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
    public int num;
    // Start is called before the first frame update
    void Start()
    {
        num = 0;
    }

    // Update is called once per frame
    public void OnValueChanged(int result)
    {
        num = result;
    }
    void Update()
    {
        
    }
}
