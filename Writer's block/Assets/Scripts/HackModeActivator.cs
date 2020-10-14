using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackModeActivator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Text> _textToMakeGreen;
    [SerializeField] private List<Image> _imagesToMakeGrey;
    [SerializeField] private List<Image> _imagesToMakeBlack;

    [Header("Normal")]
    [SerializeField] private AudioSource _normalMusicSource;
    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _normalDocumentColor;
    [SerializeField] private Color _normalBackgroundColor;

    [Header("Hacking")]
    [SerializeField] private AudioSource _hackingMusicSource;
    [SerializeField] private Color _hackingTextColor;
    [SerializeField] private Color _hackingDocumentColor;
    [SerializeField] private Color _hackingBackgroundColor;

    internal void SetHacking(bool hacking)
    {
        // These two methods could easily be combined into one..
        if (hacking)
        {
            StartHacking();
        }
        else
        {
            StopHacking();
        }
    }

    private void StartHacking()
    {
        // Music
        _normalMusicSource.volume = 0;
        _hackingMusicSource.volume = 0.2f;

        foreach (var text in _textToMakeGreen)
        {
            text.color = _hackingTextColor;
        }

        foreach (var image in _imagesToMakeGrey)
        {
            image.color = _hackingBackgroundColor;
        }

        foreach (var image in _imagesToMakeBlack)
        {
            image.color = _hackingDocumentColor;
        }

        Camera.main.backgroundColor = _hackingBackgroundColor;
    }

    private void StopHacking()
    {
        // Music
        _normalMusicSource.volume = 0.2f;
        _hackingMusicSource.volume = 0;

        foreach (var text in _textToMakeGreen)
        {
            text.color = _normalTextColor;
        }

        foreach (var image in _imagesToMakeGrey)
        {
            image.color = _normalBackgroundColor;
        }

        foreach (var image in _imagesToMakeBlack)
        {
            image.color = _normalDocumentColor;
        }

        Camera.main.backgroundColor = _normalBackgroundColor;
    }
}
