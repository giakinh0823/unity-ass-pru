using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public bool isRunning = false;
    public bool isFinish = true;
    public float elapseTime = 0;
    public float alarmTime = 3;
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        elapseTime += Time.deltaTime;
        if (elapseTime >= alarmTime)
        {
            isFinish = true;
        }

    }

    public void StartTime()
    {
        isRunning = true;
        isFinish = false;
        elapseTime = 0;
    }
}
