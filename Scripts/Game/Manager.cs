using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private float maxTemp;
    [SerializeField] private float minTemp;
    [SerializeField] private float maxEnemies;

    [SerializeField] private Enemy baseEnemy;

    [SerializeField] private GameObject[] Notes;

    [SerializeField] private Text TempText;

    private float currentTemp;
    private float numEnemies = 0;
    void Start()
    {
        currentTemp = minTemp;
    }
    void Update()
    {
        if (numEnemies < maxEnemies)
        {
            Instantiate(baseEnemy, Vector3.up, Quaternion.identity);
            numEnemies++;
        }
        TempText.text = "Temperature: " + currentTemp + "°C";
    }
    public void IncreaseTemp(float increase)
    {
        currentTemp += increase;
        if (currentTemp >= maxTemp) Win();
    }
    public void RemoveEnemy()
    {
        numEnemies--;
    }
    private void Win()
    {
        Debug.Log("Win");
    }
}
