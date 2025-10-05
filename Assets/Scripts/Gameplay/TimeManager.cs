using UnityEngine;

[CreateAssetMenu(fileName = "TimeManager", menuName = "Scriptable Objects/TimeManager")]
public class TimeManager : ScriptableObject
{
    public float startTime;
    public float endTime;
    public float finalRunLength;

    public float returnStartTime() // used if another script needs to know when this run started
    {
        return startTime;
    }

    void calculateRunLength()
    {
        finalRunLength = endTime - startTime;
    }

    public float fetchRunLength() // used for other scripts to be able to fetch the final run time easily
    {
        return finalRunLength;
    }

    void cleanUp() // tidy up stored times, ready for a new run!
    {
        startTime = 0f;
        endTime = 0f;
        finalRunLength = 0f;
    }

    public void startNewRun() // wipes old finish times and sets a new start time
    {
        cleanUp(); // run cleanup BEFORE setting startTime, or startTime will be set correctly only to be reset on the next line
        startTime = Time.time;
    }

    public void concludeRun() // when a run is over, timestamp when the run ended and use that to calculate how long the run took
    {
        endTime = Time.time;
        calculateRunLength();
    }
}
