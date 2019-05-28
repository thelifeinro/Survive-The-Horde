using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPageUI : MonoBehaviour
{
    public Text nameText;
    public Image icon;
    public Text descText;
    public Text weaknessText;
    public Text resistanceText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(EnemyBookPage p)
    {
        if (p == null)
            return;
        nameText.text = p.Name;
        icon.sprite = p.icon;
        descText.text = p.desc;
        weaknessText.text = p.weakness;
        resistanceText.text = p.resistance;
    }

    public void CloseWindow()
    {
        Destroy(gameObject);
    }
}
