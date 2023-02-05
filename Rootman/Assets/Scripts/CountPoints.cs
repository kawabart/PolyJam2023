using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CountPoints : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float timeToAddPoint = 1;
    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddPoint", timeToAddPoint, timeToAddPoint);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"Punkty: {points}";
    }

    void AddPoint()
    {
        points++;
    }

    public void AddPoints(int count)
    {
        points += count;
    }
}
