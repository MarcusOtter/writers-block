using UnityEngine;

[CreateAssetMenu(menuName = "Writer")]
public class WriterProfile : ScriptableObject
{
    internal string Biography { get; private set; }
    internal string FirstName { get; private set; }
    internal string LastName { get; private set; }
    internal string FullName => $"{FirstName} {LastName}";

    internal uint CharacterCount { get; private set; }
    internal uint CodeCharacterCount { get; private set; }
    internal uint PublishedDocuments { get; private set; }

    [SerializeField] private bool _resetOnLoad;

    // TODO: Awards

    private void OnEnable()
    {
        if (_resetOnLoad)
        {
            CharacterCount = 0;
            CodeCharacterCount = 0;
            PublishedDocuments = 0;
            FirstName = "John";
            LastName = "Doe";
            Biography = "I am by far the best writer on this planet we call Earth. Experienced button hitter.";
        }
    }

    internal void SetFirstNameTo(string firstName)
    {
        FirstName = firstName;
    }

    internal void SetLastNameTo(string lastName)
    {
        LastName = lastName;
    }

    internal void SetBiographyTo(string biography)
    {
        Biography = biography;
    }

    /// <summary>Increments the writer's character count.</summary>
    /// <returns>The method returns the new value of the <see cref="CharacterCount"/></returns>
    internal uint IncrementCharacterCount(bool isCode)
    {
        if (isCode) { CodeCharacterCount++; }
        return ++CharacterCount;
    }

    /// <summary>Decrements the writer's character count.</summary>
    /// <returns>The method returns the new value of the <see cref="CharacterCount"/></returns>
    internal uint DecrementCharacterCount(bool isCode)
    {
        if (isCode) { CodeCharacterCount--; }
        return --CharacterCount;
    }

    /// <summary>Increments the writer's amount of published documents.</summary>
    /// <returns>The method returns the new value of the <see cref="PublishedDocuments"/></returns>
    internal uint IncrementPublishedDocumentCount()
    {
        return ++PublishedDocuments;
    }
}
