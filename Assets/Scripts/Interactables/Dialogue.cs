using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public bool IsActive => gameObject.activeSelf;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.05f;
    public AudioClip dialogueSound;
    public AudioSource audioSource;

    private int index;
    private bool dialogueStarted = false;

    void Awake()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!dialogueStarted)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        gameObject.SetActive(true);
        dialogueStarted = true;

        index = 0;
        textComponent.text = string.Empty;

        if (audioSource != null && dialogueSound != null)
        {
            audioSource.clip = dialogueSound;
            audioSource.Play();
        }

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        StopAllCoroutines();
        dialogueStarted = false;
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        gameObject.SetActive(false);
    }
}