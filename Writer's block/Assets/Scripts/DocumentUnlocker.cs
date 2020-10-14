using System;
using System.Linq;
using UnityEngine;

public class DocumentUnlocker : MonoBehaviour
{
    [SerializeField] private DocumentOpenButton[] _documentOpenButtons;

    private void OnEnable()
    {
        Document.OnDocumentCompleted += UnlockDocuments;
    }

    private void UnlockDocuments(object sender, EventArgs e)
    {
        var completedDocument = (Document) sender;
        
        var documentButtonsToEnable = _documentOpenButtons
            .Where(x => completedDocument.DocumentsUnlockedOnComplete.Contains(x.DocumentToLoad))
            .ToArray();

        foreach (var buttonToEnable in documentButtonsToEnable)
        {
            buttonToEnable.gameObject.SetActive(true);
        }
    }

    // Fulhack to just have something happen when you publish it
    internal void DisableDocumentButton(Document documentToDisable)
    {
        _documentOpenButtons.FirstOrDefault(x => x.DocumentToLoad == documentToDisable)?.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Document.OnDocumentCompleted -= UnlockDocuments;
    }
}
