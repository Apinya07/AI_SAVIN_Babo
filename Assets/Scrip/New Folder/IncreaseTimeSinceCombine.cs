using UnityEngine;

public class IncreaseTimeSinceCombine : MonoBehaviour
{
    public float timeSinceCombine;

    void Start()
    {
        // เริ่มนับเวลา
        StartCoroutine(UpdateTimer());
    }

    System.Collections.IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeSinceCombine += 1f;
            // Debug.Log("TimeSinceCombine = " + timeSinceCombine);
        }
    }
}
