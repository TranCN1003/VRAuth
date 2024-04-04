using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallAverage : MonoBehaviour
{
    public RayInteractorUIHit[] TraceStats = new RayInteractorUIHit[4]; // Array to store references to RayInteractorUIHit instances

    private List<float> speeds = new List<float>(); // stores speed values
    private List<float> accelerations = new List<float>(); // stores acceleration values
    private List<float> jerks = new List<float>(); // stores jerk values

    private float Fspeed = 0f;
    private float Facceleration = 0f;
    private float Fjerk = 0f;

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
        Debug.Log("final stats are: "+ Fspeed + " ," + Facceleration + " , " + Fjerk);
    }
}
