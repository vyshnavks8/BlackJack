using System;

public struct TimedContent
{
    public readonly int Time;
    public readonly Action TimedCallback;

    public TimedContent(int timeValue, Action timedCallback)
    {
        Time = timeValue;
        TimedCallback = timedCallback;
    }
}