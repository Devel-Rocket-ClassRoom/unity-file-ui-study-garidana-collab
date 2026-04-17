using UnityEngine;

public class WindowManagerCopy : MonoBehaviour
{
    public GenericWindowCopy[] windows;

    public int currentWindowId;
    public int defaultWindowId;

    private void Awake()
    {
        foreach (var window in windows)
        {
            window.gameObject.SetActive(false);
            window.Init(this);
        }
        currentWindowId = defaultWindowId;
        windows[currentWindowId].Open();
    }

    /// <summary>
    /// Opens a GenericWindowCopy with the given id.
    /// First closes the currently open window, then sets the currentWindowId to the given id and opens the corresponding window.
    /// </summary>
    /// <param name="id">The id of the window to open.</param>
    /// <returns>The opened GenericWindowCopy.</returns>
    public GenericWindowCopy Open(int id)
    {
        windows[currentWindowId].Close();
        currentWindowId = id;
        windows[currentWindowId].Open();

        return windows[currentWindowId];
    }
}