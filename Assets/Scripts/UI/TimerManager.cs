using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private TextMeshProUGUI _timer;

    [HideInInspector]
    public int minutes;
    [HideInInspector]
    public int seconds;

    void Start()
    {
        _timer = GetComponent<TextMeshProUGUI>();
        InvokeRepeating(nameof(TickClock), 1, 1);
    }

    private void TickClock()
    {
        if(++seconds == 60)
        {
            seconds = 0;
            minutes++;
        }
        string minutesTxt = minutes >= 10 ? minutes.ToString() : $"0{minutes}";
        string secondsTxt = seconds >= 10 ? seconds.ToString() : $"0{seconds}";
        _timer.text = $"{minutesTxt}:{secondsTxt}";
    }
}
