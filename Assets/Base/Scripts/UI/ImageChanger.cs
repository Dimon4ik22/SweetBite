using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ImageSlideshow : MonoBehaviour
{
    [SerializeField] private Sprite[] _images; // Массив изображений для слайд-шоу
    [SerializeField] private Image _displayImage; // UI элемент для отображения картинки
    [SerializeField] private Image _fadeImage; // UI элемент для затемнения
    [SerializeField] private float _changeTime; // Время в секундах между сменой картинок
    [SerializeField] private string _mainSceneName = "Level-Test"; // Имя основной сцены
    [SerializeField] private float _fadeDuration;

    private int currentIndex = 0;
    private float timer;

    private void Start()
    {
        StartCoroutine(ChangeImage());
    }

    IEnumerator ChangeImage()
    {
        while (currentIndex < _images.Length)
        {
            // Затемнение
            yield return StartCoroutine(FadeTo(1, _fadeDuration));
            // Смена картинки
            _displayImage.sprite = _images[currentIndex];
            // Светлеем
            yield return StartCoroutine(FadeTo(0, _fadeDuration));

            currentIndex++;
            yield return new WaitForSeconds(_changeTime);
        }

        // Загрузка основной сцены
        SceneManager.LoadScene(_mainSceneName);
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
