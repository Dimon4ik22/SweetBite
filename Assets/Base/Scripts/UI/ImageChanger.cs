using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ImageSlideshow : MonoBehaviour
{
    [SerializeField] private Sprite[] _images; // ������ ����������� ��� �����-���
    [SerializeField] private Image _displayImage; // UI ������� ��� ����������� ��������
    [SerializeField] private Image _fadeImage; // UI ������� ��� ����������
    [SerializeField] private float _changeTime; // ����� � �������� ����� ������ ��������
    [SerializeField] private string _mainSceneName = "Level-Test"; // ��� �������� �����
    [SerializeField] private float _fadeDuration;

    private int currentIndex = 0;
    private float timer;
    private bool hasShownSlideshow;

    private void Start()
    {
        hasShownSlideshow = PlayerPrefs.GetInt("HasShownSlideshow", 0) == 1;
        if (!hasShownSlideshow)
        {
            StartCoroutine(ChangeImage());
        }
        else
        {
            SceneManager.LoadScene(_mainSceneName);
        }
    }

    IEnumerator ChangeImage()
    {
        while (currentIndex < _images.Length)
        {
            // ����������
            yield return StartCoroutine(FadeTo(1, _fadeDuration));

            // ����� ��������
            _displayImage.sprite = _images[currentIndex];

            // ��������
            yield return StartCoroutine(FadeTo(0, _fadeDuration));

            currentIndex++;
            yield return new WaitForSeconds(_changeTime);
        }

        // ���������� ��������� ��������
        yield return StartCoroutine(FadeTo(1, _fadeDuration));

        // �������� �������� �����
        SceneManager.LoadScene(_mainSceneName);

        PlayerPrefs.SetInt("HasShownSlideshow", 1);
    }

    IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = _fadeImage.color.a;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, alpha);
            yield return null;
        }

        _fadeImage.color = new Color(_fadeImage.color.r, _fadeImage.color.g, _fadeImage.color.b, targetAlpha);
    }
}


    //private void Update()
    //{
    //    _timer += Time.deltaTime;
    //    if (_timer > _changeTime)
    //    {
    //        _currentIndex++;
    //        if (_currentIndex >= _images.Length)
    //        {
    //            SceneManager.LoadScene(_mainSceneName);
    //        }
    //        else
    //        {
    //            UpdateImage();
    //        }
    //        _timer = 0;
    //    }
    //}
    //private void UpdateImage()
    //{
    //    if (_images.Length > 0 && _currentIndex < _images.Length)
    //    {
    //        _displayImage.sprite = _images[_currentIndex];
    //    }
    //}
