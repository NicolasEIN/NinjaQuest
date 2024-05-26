using Ink.Runtime;
using Ink.UnityIntegration;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    [Header("UI")]
    [SerializeField] private DialogueUI dialogueUI;

    [Header("NPC Image")]
    [SerializeField] private Sprite npcImage;

    private bool isDialogueActive = false;
    private Story story;

    [SerializeField] private float textSpeed = 0.5f; // Velocidade de exibi��o do texto

    private Coroutine dialogueCoroutine; // Refer�ncia para a coroutine do di�logo

    private void Start()
    {
        dialogueUI.HideDialogue();
        story = new Story(inkJSON.text);
    }

    public void Interact()
    {
        if (isDialogueActive)
        {
            // Se o di�logo j� estiver ativo, retornar para evitar intera��es duplicadas
            return;
        }

        isDialogueActive = true;
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        // Obt�m a pr�xima linha da hist�ria
        string text = GetNextStoryLine();

        // Exibe a caixa de di�logo com a imagem do NPC e o texto
        dialogueUI.ShowDialogue(text, gameObject.name, npcImage);

        // Inicia a coroutine para exibir o texto gradualmente
        dialogueCoroutine = StartCoroutine(DisplayTextGradually());
    }

    private string GetNextStoryLine()
    {
        // Obt�m a pr�xima linha da hist�ria usando o Ink
        string nextLine = "";
        if (story != null && story.canContinue)
        {
            nextLine = story.Continue().ToString();
        }
        return nextLine;
    }

    // Coroutine para exibir o texto gradualmente
    private IEnumerator DisplayTextGradually()
    {
        // Aguarda um tempo antes de iniciar a exibi��o gradual
        yield return new WaitForSeconds(textSpeed);

        // Continua enquanto houver texto na hist�ria
        while (story != null && story.canContinue)
        {
            // Obt�m a pr�xima linha da hist�ria
            string nextLine = story.Continue().ToString();

            // Exibe a pr�xima linha da hist�ria na caixa de di�logo
            dialogueUI.UpdateDialogue(nextLine);

            // Aguarda um tempo antes de exibir a pr�xima linha
            yield return new WaitForSeconds(textSpeed);
        }

        // Oculta a caixa de di�logo quando o di�logo termina
        dialogueUI.HideDialogue();
        isDialogueActive = false;
    }

    // M�todo para resetar o di�logo
    public void ResetDialogue()
    {
        // Cancela a coroutine se estiver ativa
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        // Oculta a caixa de di�logo
        dialogueUI.HideDialogue();
        isDialogueActive = false;

        // Reseta o estado do di�logo para o in�cio
        story.ResetState();
    }
    

    }
