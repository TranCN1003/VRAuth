using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OverallAverage : MonoBehaviour
{
    public RayInteractorUIHit[] TraceStats = new RayInteractorUIHit[4]; // Array to store references to RayInteractorUIHit instances
    public RayInteractorUIHit Mazestats; //maze page stat ref

    public GameObject mazePage; // Reference to the maze UI page
    public GameObject winPage; // Reference to the win UI page
    public GameObject losePage; // Reference to the lose UI page

    private List<float> speeds = new List<float>(); // stores speed values
    private List<float> accelerations = new List<float>(); // stores acceleration values
    private List<float> jerks = new List<float>(); // stores jerk values

    //final stats
    private float Fspeed = 0f;
    private float Facceleration = 0f;
    private float Fjerk = 0f;

    //maze stats
    private float Mspeed = 0f;
    private float Macceleration = 0f;
    private float Mjerk = 0f;

    //final stats
    private float speedDif = 0f;
    private float accelerationDif = 0f;
    private float jerkDif = 0f;

    public void MainAvg()
    {
        for(int i=1;i<4;i++)
            {
                float speed = TraceStats[i].CalculateAverageSpeed();
                float acceleration = TraceStats[i].CalculateAverageAcceleration();
                float jerk = TraceStats[i].CalculateAverageJerk();


                speeds.Add(speed);
                accelerations.Add(acceleration);
                jerks.Add(jerk);

            }
        Fspeed = CalculateAverageSpeed();
        Facceleration = CalculateAverageAcceleration();
        Fjerk = CalculateAverageJerk();

        Mspeed = Mazestats.CalculateAverageSpeed();
        Macceleration = Mazestats.CalculateAverageAcceleration();
        Mjerk = Mazestats.CalculateAverageJerk();
        
        speedDif = (Math.Abs(Fspeed - Mspeed)/((Fspeed + Mspeed)/2))* 100;
        accelerationDif = (Math.Abs(Facceleration - Macceleration)/((Facceleration + Macceleration)/2))* 100;
        jerkDif = (Math.Abs(Fjerk - Mjerk)/((Fjerk + Mjerk)/2))* 100;
        
    }

    public float CalculateAverageSpeed()
    {
        if (speeds.Count == 0)
            return 0f;

        float sum = 0f;
        foreach (float speed in speeds)
        {
            sum += speed;
        }
        return sum / speeds.Count;
    }
   
    public float CalculateAverageAcceleration()
    {
        if (accelerations.Count == 0)
            return 0f;

        float sum = 0f;
        foreach (float acceleration in accelerations)
        {
            sum += acceleration;
        }
        return sum / accelerations.Count;
    }

    public float CalculateAverageJerk()
    {
        if (jerks.Count == 0)
            return 0f;

        float sum = 0f;
        foreach (float jerk in jerks)
        {
            sum += jerk;
        }
        return sum / jerks.Count;
    }

    public void DisplayFStats()
    {
        MainAvg();
        Debug.Log("final stats are: "+ Fspeed + " ," + Facceleration + " , " + Fjerk);
        Debug.Log("maze stats are: "+ Mspeed + " ," + Macceleration + " , " + Mjerk);
        Debug.Log("Speed, acceleration and jerk diff Percentages are " + speedDif + " ," + accelerationDif + " ," + jerkDif );
    }

    public void Authenticate()
    {
        

        if (speedDif <50 && accelerationDif < 50 && jerkDif < 50)
        {
            winPage.SetActive(true);
        }
        else
        {
            losePage.SetActive(true);
        }
    }
}
