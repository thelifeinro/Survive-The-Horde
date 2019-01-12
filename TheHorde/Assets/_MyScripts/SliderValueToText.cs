using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueToText : MonoBehaviour
{
    public Text TextValue;
    Slider mySlider;
    
    // Start is called before the first frame update
    void Start()
    {
        mySlider = this.GetComponent<Slider>();
        mySlider.onValueChanged.AddListener(delegate { UpdateValue(); });
    }

    // Update is called once per frame
    void UpdateValue()
    {
        TextValue.text = mySlider.value.ToString();
    }
}
