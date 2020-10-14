using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Document")]
public class Document : ScriptableObject
{
    internal static event EventHandler OnDocumentCompleted;

    [SerializeField] internal bool IsCode;
    [SerializeField] internal string Title;
    [SerializeField] internal List<Document> DocumentsUnlockedOnComplete;

    internal bool IsPublished { get; private set; }

    internal int Index { get; private set; }
    internal int CompletionPercent => GetCompletionPercent();

    internal string WrittenText { get; private set; }
    internal string FullText { get; private set; }
    [SerializeField] internal WriterProfile Author;

    [SerializeField] private string _fileName = "beemovie";

    [Header("DEBUG ONLY - LEAVE FALSE IN GAME")]
    [SerializeField] private bool _resetOnLoad;
    
    private void OnEnable()
    {
        var textAsset = (TextAsset) Resources.Load(_fileName, typeof(TextAsset));

        FullText = textAsset.text;

        if (_resetOnLoad)
        {
            WrittenText = string.Empty;
            Index = 0;
            IsPublished = false;
        }
    }

    internal void SaveDocumentProgress(int newIndex, string writtenText)
    {
        Index = newIndex;
        WrittenText = writtenText;
    }

    internal void SetAuthor(WriterProfile newAuthor)
    {
        Author = newAuthor;
    }

    internal void Publish()
    {
        IsPublished = true;
        OnDocumentCompleted?.Invoke(this, EventArgs.Empty); // EventArgs can be sent for achievements
    }

    internal bool IsCompleted()
    {
        return GetCompletionPercent() == 100;
    }

    private int GetCompletionPercent()
    {
        return (int) ((Index / (float)FullText.Length) * 100);
    }
}
