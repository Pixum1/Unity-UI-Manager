using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas))]
[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    [SerializeField, Tooltip("The initial page, typically the main menu")] private Page m_initialPage;
    [SerializeField] private GameObject m_firstFocusedItem;

    private Canvas rootCanvas;
    private Stack<Page> pageStack = new Stack<Page>();

    /// <summary>
    /// Checks if a page is either open in the background or is the one on top ov every page
    /// </summary>
    /// <param name="_page">The page to check</param>
    /// <returns></returns>
    public bool IsPageInStack(Page _page) => pageStack.Contains(_page);

    /// <summary>
    /// Checks if a page is the page on top of every page
    /// </summary>
    /// <param name="_page">The page to check</param>
    /// <returns></returns>
    public bool IsPageOnTopOfStack(Page _page) => pageStack.Count > 0 && _page == pageStack.Peek();

    private void Awake() => rootCanvas = GetComponent<Canvas>();

    private void Start()
    {
        if(m_firstFocusedItem != null)
            EventSystem.current.SetSelectedGameObject(m_firstFocusedItem);

        if (m_initialPage != null)
            PushPage(m_initialPage);
    }

    // Input sytem callback method to close the current page
    public void OnCancel()
    {
        if (!rootCanvas.enabled) return;
        if (!rootCanvas.gameObject.activeInHierarchy) return;
        if (pageStack.Count == 0) return;

        PopPage();
    }

    /// <summary>
    /// Opens a new page and pushes it on top of the pagestack
    /// </summary>
    /// <param name="_page">The page to open</param>
    public void PushPage(Page _page)
    {
        _page.Push();

        if(pageStack.Count > 0)
        {
            Page currentPage = pageStack.Peek();

            if (currentPage.ExitOnNewPagePush)
                currentPage.Pop();
        }

        pageStack.Push(_page);
    }

    /// <summary>
    /// Closes the current page that is on the top of the pagestack
    /// </summary>
    public void PopPage()
    {
        if (pageStack.Count <= 1) return;

        Page page = pageStack.Pop();
        page.Pop();

        Page newCurrentPage = pageStack.Peek();

        if (newCurrentPage.ExitOnNewPagePush)
            newCurrentPage.Push();
    }

    /// <summary>
    /// Closes all pages and clears the pagestack
    /// </summary>
    public void PopAllPages()
    {
        for (int i = 0; i < pageStack.Count; i++)
            PopPage();
    }
}
