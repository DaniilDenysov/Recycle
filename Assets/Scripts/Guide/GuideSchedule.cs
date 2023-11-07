using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSchedule : MonoBehaviour
{

    public static GuideSchedule Instance;
    [SerializeField] private List<GuideEvent> _guideEvents;
    [SerializeField] private int _currentSlide;

    private void Awake()
    {
        Instance = this;
        GuideEvent.OnEventStarted += GuideEvent_OnEventStarted;
        GuideEvent.OnEventEnded += GuideEvent_OnEventEnded;
    }

    private void Start()
    {
        _guideEvents[0].StartEvent();
    }

    public GuideEvent GetCurrentEvent() => _guideEvents[_currentSlide];

    private void GuideEvent_OnEventStarted(object sender, EventArgs e)
    {

    }

    private void GuideEvent_OnEventEnded(object sender, EventArgs e)
    {
        _currentSlide++;
    }
}
