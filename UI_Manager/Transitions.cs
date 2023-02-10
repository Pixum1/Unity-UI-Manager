using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Transitions
{
    public static IEnumerator ZoomIn(RectTransform _transform, float _speed, UnityEvent _onEnd = null)
    {
        float time = 0;
        while (time < 1)
        {
            _transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

        _transform.localScale = Vector3.one;

        _onEnd?.Invoke();
    }

    public static IEnumerator ZoomOut(RectTransform _transform, float _speed, UnityEvent _onEnd = null)
    {
        float time = 0;
        while (time < 1)
        {
            _transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

        _transform.localScale = Vector3.zero;
        _onEnd?.Invoke();
    }

    public static IEnumerator FadeIn(CanvasGroup _canvasGroup, float _speed, UnityEvent _onEnd = null)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;

        float time = 0;
        while (time < 1)
        {
            _canvasGroup.alpha = Mathf.Lerp(0, 1, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

        _canvasGroup.alpha = 1;
        _onEnd?.Invoke();
    }

    public static IEnumerator FadeOut(CanvasGroup _canvasGroup, float _speed, UnityEvent _onEnd = null)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;

        float time = 0;
        while (time < 1)
        {
            _canvasGroup.alpha = Mathf.Lerp(1, 0, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

        _canvasGroup.alpha = 0;
        _onEnd?.Invoke();
    }

    public static IEnumerator SlideIn(RectTransform _transform, EDirection _EDirection, float _speed, UnityEvent _onEnd = null)
    {
        Vector2 startPosition;
        switch (_EDirection)
        {
            case EDirection.UP:
                startPosition = new Vector2(0, -Screen.height);
                break;
            case EDirection.RIGHT:
                startPosition = new Vector2(-Screen.width, 0);
                break;
            case EDirection.DOWN:
                startPosition = new Vector2(0, Screen.height);
                break;
            case EDirection.LEFT:
                startPosition = new Vector2(Screen.width, 0);
                break;
            default:
                startPosition = new Vector2(0, -Screen.height);
                break;
        }

        float time = 0;
        while (time < 1)
        {
            _transform.anchoredPosition = Vector2.Lerp(startPosition, Vector2.zero, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

        _transform.anchoredPosition = Vector2.zero;
        _onEnd?.Invoke();
    }

    public static IEnumerator SlideOut(RectTransform _transform, EDirection _EDirection, float _speed, UnityEvent _onÉnd = null)
    {
        Vector2 endPosition;
        switch (_EDirection)
        {
            case EDirection.UP:
                endPosition = new Vector2(0, Screen.height);
                break;
            case EDirection.RIGHT:
                endPosition = new Vector2(Screen.width, 0);
                break;
            case EDirection.DOWN:
                endPosition = new Vector2(0, -Screen.height);
                break;
            case EDirection.LEFT:
                endPosition = new Vector2(-Screen.width, 0);
                break;
            default:
                endPosition = new Vector2(0, Screen.height);
                break;
        }

        float time = 0;
        while (time < 1)
        {
            _transform.anchoredPosition = Vector2.Lerp(Vector2.zero, endPosition, time);
            yield return null;
            time += Time.deltaTime * _speed;
        }

        _transform.anchoredPosition = endPosition;
        _onÉnd?.Invoke();
    }
}