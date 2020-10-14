using UnityEngine;
using UnityEngine.UI;

public class DocumentOpenButton : MonoBehaviour
{
    [SerializeField] internal Document DocumentToLoad;
    [SerializeField] internal Text DocumentCompletion;

    private Editor _editor;

    private void Start()
    {
        _editor = FindObjectOfType<Editor>();
        DisplayDocumentCompletion();
    }

    public void SendDocumentToEditor()
    {
        DocumentToLoad.SetAuthor(FindObjectOfType<PlayerInput>().ActiveWriter);
        _editor.LoadDocument(this);
    }

    internal void DisplayDocumentCompletion()
    {
        DocumentCompletion.text = $"{DocumentToLoad.CompletionPercent}%";
    }
}
