using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TextUpdateValue : MonoBehaviour
{
    private string _value;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (text.text != _value)
        {
            text.text = _value;
        }
    }
}
