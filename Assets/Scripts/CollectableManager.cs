using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectableManager : MonoBehaviour
{
    public int count = 0;

    public GameObject majorUnlock;
    
    public TextMeshProUGUI countText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = "Coin Count: " + count.ToString();
    }

    public void checkCount()
    {
        if (count >= 4)
        {
            majorUnlock.SetActive(true);
        }
        
    }
}