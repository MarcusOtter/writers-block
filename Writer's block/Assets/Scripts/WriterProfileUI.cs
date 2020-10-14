using UnityEngine;
using UnityEngine.UI;

public class WriterProfileUI : MonoBehaviour
{
    internal static bool WriterCustomizationIsActive { get; private set; }

    [Header("Writer info references")]
    [SerializeField] private Text _firstName;
    [SerializeField] private Text _lastName;
    [SerializeField] private Text _biography;
    [SerializeField] private Text _characterCount;
    [SerializeField] private Text _publishedDocuments;
    [SerializeField] private Text _awardCount;

    [Header("Customization references")]
    [SerializeField] private Animator _writerCustomization;
    [SerializeField] private InputField _firstNameInput;
    [SerializeField] private InputField _lastNameInput;
    [SerializeField] private InputField _biographyInput;

    private int _animateInHash;
    private int _animateOutHash;

    private WriterProfile _writerProfile;

    private void Start()
    {
        _animateInHash = Animator.StringToHash("AnimateIn");
        _animateOutHash = Animator.StringToHash("AnimateOut");
        _writerProfile = FindObjectOfType<PlayerInput>().ActiveWriter;

        SaveWriterInfo();
    }

    public void SetWriterCustomizationActive(bool active)
    {
        _writerCustomization.SetTrigger(active ? _animateInHash : _animateOutHash);
        WriterCustomizationIsActive = active;
    }

    public void SaveWriterInfo()
    {
        if (!string.IsNullOrWhiteSpace(_firstNameInput.text))
        {
            _writerProfile.SetFirstNameTo(_firstNameInput.text);
        }

        if (!string.IsNullOrWhiteSpace(_lastNameInput.text))
        {
            _writerProfile.SetLastNameTo(_lastNameInput.text);
        }

        if (!string.IsNullOrWhiteSpace(_biographyInput.text))
        {
            _writerProfile.SetBiographyTo(_biographyInput.text);
        }

        UpdateWriterInfoUi();
    }

    internal void IncrementPublishedDocuments()
    {
        _publishedDocuments.text = $"{_writerProfile.IncrementPublishedDocumentCount()}";
    }

    internal void IncrementCharacterCount(bool isCode)
    {
        _characterCount.text = $"{_writerProfile.IncrementCharacterCount(isCode)}";
    }

    internal void DecrementCharacterCount(bool isCode)
    {
        _characterCount.text = $"{_writerProfile.DecrementCharacterCount(isCode)}";
    }

    private void UpdateWriterInfoUi()
    {
        _firstName.text = _writerProfile.FirstName;
        _lastName.text = _writerProfile.LastName;
        _biography.text = _writerProfile.Biography;
        _characterCount.text = _writerProfile.CharacterCount.ToString();
        _publishedDocuments.text = _writerProfile.PublishedDocuments.ToString();
    }
}
