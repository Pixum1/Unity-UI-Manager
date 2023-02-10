using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource), typeof(CanvasGroup))]
[DisallowMultipleComponent]
public class Page : MonoBehaviour
{
    // Components & References
    private AudioSource audioSource;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [Tooltip("Close this page when a new page is opened")] public bool ExitOnNewPagePush = false;

    [SerializeField] private float m_animationSpeed = 1f;

    // Audio clips that will be played when the page is opened or closed
    [SerializeField] private AudioClip m_entryClip;
    [SerializeField] private AudioClip m_exitClip;

    // Transition mode and direction of the page
    [SerializeField] private ETransitionMode m_entryMode = ETransitionMode.SLIDE;
    [SerializeField] private EDirection m_entryDirection = EDirection.LEFT;
    [SerializeField] private ETransitionMode m_exitMode = ETransitionMode.SLIDE;
    [SerializeField] private EDirection m_exitDirection = EDirection.LEFT;

    // UnityEvents that will be called if the page has been opened or closed
    [SerializeField, Tooltip("Called before the new Page is being pushed")] private UnityEvent m_prePushAction;
    [SerializeField, Tooltip("Called after the new Page has been pushed")] private UnityEvent m_postPushAction;
    [SerializeField, Tooltip("Called before the new Page is being popped")] private UnityEvent m_prePopAction;
    [SerializeField, Tooltip("Called after the new Page has been popped")] private UnityEvent m_postPopAction;

    // Coroutines for transition effect and playing audio
    private Coroutine AnimationCoroutine;
    private Coroutine AudioCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        // Initialize audiosource
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
        audioSource.enabled = false;    // disable audiosource to prevent cpu overhead
    }

    /// <summary>
    /// Opens the page with the specified animation
    /// </summary>
    public void Push()
    {
        m_prePushAction?.Invoke();

        switch (m_entryMode)
        {
            case ETransitionMode.SLIDE:
                PlayAnimation(Transitions.SlideIn(rectTransform, m_entryDirection, m_animationSpeed, m_postPushAction), m_entryClip);
                break;
            case ETransitionMode.ZOOM:
                PlayAnimation(Transitions.ZoomIn(rectTransform, m_animationSpeed, m_postPushAction), m_entryClip);
                break;
            case ETransitionMode.FADE:
                PlayAnimation(Transitions.FadeIn(canvasGroup, m_animationSpeed, m_postPushAction), m_entryClip);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Closes the page with the specified animation
    /// </summary>
    public void Pop()
    {
        m_prePopAction?.Invoke();

        switch (m_exitMode)
        {
            case ETransitionMode.SLIDE:
                PlayAnimation(Transitions.SlideOut(rectTransform, m_exitDirection, m_animationSpeed, m_postPopAction), m_exitClip);
                break;
            case ETransitionMode.ZOOM:
                PlayAnimation(Transitions.ZoomOut(rectTransform, m_animationSpeed, m_postPopAction), m_exitClip);
                break;
            case ETransitionMode.FADE:
                PlayAnimation(Transitions.FadeOut(canvasGroup, m_animationSpeed, m_postPopAction), m_exitClip);
                break;
            default:
                break;
        }
    }

    private void PlayAnimation(IEnumerator _animation, AudioClip _audioClip, bool _playAudio = true)
    {
        if (AnimationCoroutine != null)
            StopCoroutine(AnimationCoroutine);

        AnimationCoroutine = StartCoroutine(_animation);

        if (!_playAudio) return;
        if (_audioClip == null) return;

        if (AudioCoroutine != null)
            StopCoroutine(AudioCoroutine);

        AudioCoroutine = StartCoroutine(PlayClip(_audioClip));
    }
    private IEnumerator PlayClip(AudioClip _clip)
    {
        audioSource.enabled = true;

        audioSource.PlayOneShot(_clip);
        yield return new WaitForSeconds(_clip.length);

        audioSource.enabled = false;        // disable audiosource to prevent CPU overhead when too many audiosources are active
    }
}
