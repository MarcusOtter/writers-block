using UnityEngine;

public class StartupScript : MonoBehaviour
{
    [SerializeField] private DocumentOpenButton _firstDocumentOpenButton;

    private void Start()
    {
        FindObjectOfType<Editor>().LoadDocument(_firstDocumentOpenButton);
    }
}
