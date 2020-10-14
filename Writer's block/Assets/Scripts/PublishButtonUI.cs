using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PublishButtonUI : MonoBehaviour
{
    private Document _documentToPublish;
    private Animator _animator;

    private int _animateInHash;
    private int _animateOutHash;

    private bool _isActive;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animateInHash = Animator.StringToHash("AnimateIn");
        _animateOutHash = Animator.StringToHash("AnimateOut");
    }

    internal void OpenPublishButtonFor(Document documentToPublish)
    {
        _isActive = true;
        _documentToPublish = documentToPublish;
        _animator.SetTrigger(_animateInHash);
    }

    internal void ClosePublishButton()
    {
        if (!_isActive) { return; }
        _animator.SetTrigger(_animateOutHash);
        _isActive = false;
    }

    // Called by button
    public void PublishDocument()
    {
        _documentToPublish.Publish();
        FindObjectOfType<DocumentUnlocker>().DisableDocumentButton(_documentToPublish);
        FindObjectOfType<WriterProfileUI>().IncrementPublishedDocuments();
        ClosePublishButton();
    }
}
