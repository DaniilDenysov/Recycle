using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GuideManager : MonoBehaviour
{
    public static GuideManager Instance;
    [Header("If true it isn't updating in editor, but updating on start of game")]
    [SerializeField] private bool _smartListUpdate;
    [SerializeField] private List<Slide> _slides;
    [SerializeField] private bool _guide;
    [SerializeField] private Animator _camera;
    private int _currentSlide;

    private void Start()
    {
        if (!_smartListUpdate) return;
        GameBrakeManager.Instance.ChangeState(_guide);
        UpdateSlidesList();
        _slides[0].Enable();
    }

    [System.Serializable]
    public class Slide
    {
        private GameObject _slideObject;
      //  private Button _button;
       // private Action<Slide> _myAction;

        public Slide (GameObject _slideObject)
        {
            this._slideObject = _slideObject;
            Disable();
          //  _button = _slideObject.GetComponent<Button>();
          //  _button.onClick.AddListener(_buttonAction);
        }

        public void Disable() => _slideObject.SetActive(false);

        public void Enable() => _slideObject.SetActive(true);

        public override string ToString() => _slideObject.name;

        public void ChangeName(string name) => _slideObject.name = name;
    }

    public int GetSlideIndex (Slide _slide) => _slides.IndexOf(_slide);

    public void NextSlide (int _dir)
    {
        if (_currentSlide + _dir <= _slides.Count - 1 && _currentSlide + _dir >= 0)
        {
           
            _slides[_currentSlide].Disable();
            _currentSlide+= _dir;
            _slides[_currentSlide].Enable();
        }
        else
        {
            _slides[_currentSlide].Disable();
            GameBrakeManager.Instance.ChangeState(false);
            gameObject.SetActive(false);
        }
    }

    private Slide ToSlide(GameObject _slide) => new Slide(_slide);

    private List<Slide> GetSlides ()
    {
        List<Slide> _buffer = new List<Slide>();
        foreach (Transform _temp in transform)
        {
            Slide _tmp = ToSlide(_temp.gameObject);
            _buffer.Add(_tmp);
            _tmp.ChangeName("Slide" + _buffer.IndexOf(_tmp));
        }
        return _buffer;
    }

    public void UpdateSlidesList ()
    {
        int _slideCount = GetComponentsInChildren<Transform>().Length;
        if (_slideCount != _slides.Capacity)
        {
            _slides.Clear();
            _slides.AddRange(GetSlides());
        }
    }

    private void OnDrawGizmos ()
    {
        if (_smartListUpdate) return;
        _slides = new List<Slide>();
        UpdateSlidesList();
    }
}
