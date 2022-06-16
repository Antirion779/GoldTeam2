using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMovement : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private Image _fillImage;
    [SerializeField] private Color _fillColor;
    [SerializeField] Transform _sliderLeft;
    [SerializeField] Transform _sliderRight;

    [SerializeField] Achievement _achievement;
    private float _3stars;
    private float _2stars;
    private float _1stars;

    [SerializeField] private Image _3to2starsImg;
    [SerializeField] private Image _2to1starsImg;
    [SerializeField] private Image _1to0starsImg;

    private bool _playedAnimStar1 = false;
    private bool _playedAnimStar2 = false;
    private bool _playedAnimStar3 = false;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _fillImage.color = _fillColor;
        _1stars = _achievement.etoile1;
        _2stars = _achievement.etoile2;
        _3stars = _achievement.etoile3;

        SetPos();
    }
    public void UpdateSlider()
    {
        _slider.value = (1f - (GameManager.Instance.ActionPoint / _1stars));

        UpdateStars();
    }
    private void SetPos()
    {
        float star3to2percent = (_3stars / _1stars);
        float star2to1percent = (_2stars / _1stars);

        _3to2starsImg.transform.position = new Vector2(Mathf.Lerp(_sliderLeft.transform.position.x, _sliderRight.transform.position.x, star3to2percent), _slider.transform.position.y);
        _2to1starsImg.transform.position = new Vector2(Mathf.Lerp(_sliderLeft.transform.position.x, _sliderRight.transform.position.x, star2to1percent), _slider.transform.position.y);
    }

    public void UpdateStars()
    {
        if (GameManager.Instance.ActionPoint > _3stars && !_playedAnimStar3)
        {
            _playedAnimStar3 = true;
            _3to2starsImg.GetComponent<Animator>().SetTrigger("Loose");
        }
        else if (GameManager.Instance.ActionPoint > _2stars && !_playedAnimStar2)
        {
            _playedAnimStar2 = true;
            _2to1starsImg.GetComponent<Animator>().SetTrigger("Loose");
        }
        else if (GameManager.Instance.ActionPoint > _1stars && !_playedAnimStar1)
        {
            _playedAnimStar1 = true;
            _1to0starsImg.GetComponent<Animator>().SetTrigger("Loose");
        }      
    }

 }
