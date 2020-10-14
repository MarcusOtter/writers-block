using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Text _textBox;
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private Text _header;
    [SerializeField] private Text _subheader;

    [Header("Settings")]
    [SerializeField] private int _additionalContentHeight = 150;

    // I don't really like these 2 references but 1 week game is a 1 week game ¯\_(ツ)_/¯
    private WriterProfileUI _writerProfileUi;
    private PublishButtonUI _publishButtonUi;

    private RectTransform _contentTransform;

    private DocumentOpenButton _loadedDocumentOpenButton;
    private Document _loadedDocument;
    private int _letterIndex;

    private void OnEnable()
    {
        PlayerInput.OnKeyDown += HandleInput;
        _writerProfileUi = FindObjectOfType<WriterProfileUI>();
        _publishButtonUi = FindObjectOfType<PublishButtonUI>();
    }

    // Called when the OnKeyDown event is called in PlayerInput.cs
    private void HandleInput(object sender, KeyType keyType)
    {
        if (_loadedDocument == null) { return; }

        // Don't register editor presses if the user is changing their name / bio
        if (WriterProfileUI.WriterCustomizationIsActive) { return; }

        switch (keyType)
        {
            case KeyType.Backspace:
                RemoveCharacter();
                break;

            case KeyType.KeyboardKey:
                AddCharacter();
                break;
            
            // If it's not a key on the keyboard press, return from the method.
            default:
                return;
        }

        // Recalculate the height of the scroll view and 
        // scroll all the way down to the bottom.
        StartCoroutine(CalculateTextBoxHeight(withDelay: false));
        _scrollbar.value = 0;
    }

    /// <summary>
    /// Adds the upcoming character of the loaded document to the editor text box.
    /// </summary>
    private void AddCharacter()
    {
        if (_textBox.text.Length >= _loadedDocument.FullText.Length) { return; }

        if (_loadedDocument.IsPublished) { return; }

        _textBox.text += _loadedDocument.FullText[_letterIndex];  
        _letterIndex++;

        _loadedDocument.SaveDocumentProgress(_letterIndex, _textBox.text);
        _loadedDocumentOpenButton.DisplayDocumentCompletion();
        _writerProfileUi.IncrementCharacterCount(_loadedDocument.IsCode);

        if (_loadedDocument.IsCompleted())
        {
            _publishButtonUi.OpenPublishButtonFor(_loadedDocument);
        }
    }

    /// <summary>
    /// Removes the last character in the editor text box.
    /// </summary>
    private void RemoveCharacter()
    {
        // Might want to be able to hold this key down and remove characters faster.
        // Could start a coroutine when it is pressed down, and after x amount of seconds
        // the coroutine would check if they are still holding the button, and then just remove
        // a character in a loop while the button is being pressed.

        if (_textBox.text.Length <= 0) { return; }
        if (_loadedDocument.IsPublished) { return; }

        _textBox.text = _textBox.text.Remove(_textBox.text.Length -1);
        _letterIndex--;

        _loadedDocument.SaveDocumentProgress(_letterIndex, _textBox.text);
        _loadedDocumentOpenButton.DisplayDocumentCompletion();
        _writerProfileUi.DecrementCharacterCount(_loadedDocument.IsCode);

        _publishButtonUi.ClosePublishButton();
    }

    private void Start()
    {
        _contentTransform = _textBox.transform.parent.GetComponent<RectTransform>();
    }

    internal void ReloadActiveDocument()
    {
        LoadDocument(_loadedDocumentOpenButton);
    }

    internal void LoadDocument(DocumentOpenButton documentOpenButton)
    {
        if (_loadedDocument != null)
        {
            UnloadActiveDocument();
        }

        FindObjectOfType<HackModeActivator>().SetHacking(documentOpenButton.DocumentToLoad.IsCode);
        
        _loadedDocument = documentOpenButton.DocumentToLoad;
        _letterIndex = _loadedDocument.Index;
        _textBox.text = _loadedDocument.WrittenText;
        _header.text = _loadedDocument.Title;

        if (_subheader == null) {
            print("subheader null yo");
        }

        if (_loadedDocument == null)
        {
            print("loaded document null yo");
        }

        if (_loadedDocument.Author == null)
        {
            print("author null yo");
        }

        _subheader.text = _loadedDocument.Author.FullName;
        _loadedDocumentOpenButton = documentOpenButton;

        StartCoroutine(CalculateTextBoxHeight(withDelay: true));
    }

    private void UnloadActiveDocument()
    {
        if (_loadedDocument == null) { return; }

        _loadedDocument.SaveDocumentProgress(_letterIndex, _textBox.text);

        _loadedDocument = null;
        _textBox.text = string.Empty;
        _header.text = string.Empty;
        _subheader.text = string.Empty;
    }

    /// <summary>
    /// Recalculates the height of the textbox depending on how many lines of text there are in the <see cref="_textBox"/>.
    /// </summary>
    /// <remarks>
    /// If <see cref="withDelay"/> is true, the method will wait 100 milliseconds before recalculating the size.
    /// This is so Unity has enough time to add the character to the text box.
    /// </remarks>
    private IEnumerator CalculateTextBoxHeight(bool withDelay)
    {
        if (withDelay)
        {
            yield return new WaitForSeconds(0.1f); // Delay to give Unity some time to add the character
        }

        if (_textBox.text.Length <= 0)
        {
            _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x, 500);
            yield break;
        }

        _contentTransform.sizeDelta = new Vector2(_contentTransform.sizeDelta.x,
            _textBox.rectTransform.sizeDelta.y + _additionalContentHeight);
    }

    public void ReloadHeader()
    {
        _subheader.text = FindObjectOfType<PlayerInput>().ActiveWriter.FullName;
    }

    private void OnDisable()
    {
        UnloadActiveDocument();
        PlayerInput.OnKeyDown -= HandleInput;
    }
}
