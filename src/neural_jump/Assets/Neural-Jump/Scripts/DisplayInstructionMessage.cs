using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayInstructionMessage : MonoBehaviour
{
    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

	void Update()
	{
        text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Sin(Time.time * 8));

        if(Input.GetKey("space"))
        {
            text.GetComponent<Text>().enabled = false;
        }
	}

	
}