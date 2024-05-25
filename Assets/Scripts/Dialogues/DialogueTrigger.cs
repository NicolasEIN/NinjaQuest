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

    [SerializeField] private float textSpeed = 0.5f; // Velocidade de exibição do texto

    private Coroutine dialogueCoroutine; // Referência para a coroutine do diálogo

    private void Start()
    {
        dialogueUI.HideDialogue();
        story = new Story(inkJSON.text);
    }

    public void Interact()
    {
        if (isDialogueActive)
        {
            // Se o diálogo já estiver ativo, retornar para evitar interações duplicadas
            return;
        }

        isDialogueActive = true;
        TriggerDialogue();
    }

    private void TriggerDialogue()
    {
        // Obtém a próxima linha da história
        string text = GetNextStoryLine();

        // Exibe a caixa de diálogo com a imagem do NPC e o texto
        dialogueUI.ShowDialogue(text, gameObject.name, npcImage);

        // Inicia a coroutine para exibir o texto gradualmente
        dialogueCoroutine = StartCoroutine(DisplayTextGradually());
    }

    private string GetNextStoryLine()
    {
        // Obtém a próxima linha da história usando o Ink
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
        // Aguarda um tempo antes de iniciar a exibição gradual
        yield return new WaitForSeconds(textSpeed);

        // Continua enquanto houver texto na história
        while (story != null && story.canContinue)
        {
            // Obtém a próxima linha da história
            string nextLine = story.Continue().ToString();

            // Exibe a próxima linha da história na caixa de diálogo
            dialogueUI.UpdateDialogue(nextLine);

            // Aguarda um tempo antes de exibir a próxima linha
            yield return new WaitForSeconds(textSpeed);
        }

        // Oculta a caixa de diálogo quando o diálogo termina
        dialogueUI.HideDialogue();
        isDialogueActive = false;
    }

    // Método para resetar o diálogo
    public void ResetDialogue()
    {
        // Cancela a coroutine se estiver ativa
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        // Oculta a caixa de diálogo
        dialogueUI.HideDialogue();
        isDialogueActive = false;

        // Reseta o estado do diálogo para o início
        story.ResetState();
    }
        //[Header("NPC Settings")]
        //[SerializeField] private string npcName;
        //[SerializeField] private Sprite npcImage; // Imagem do NPC que será exibida durante o diálogo

        //[Header("Ink JSON")]
        //[SerializeField] private TextAsset inkJSON;

        //private bool isDialogueActive = false; // Flag para verificar se o diálogo já está ativo
        //[SerializeField] private DialogueUI dialogueUI; // Referência para o componente DialogueUI

        //private void Start()
        //{
        //    // Esconde a UI de diálogo no início
        //    dialogueUI.HideDialogue();
        //}

        //public void Interact()
        //{
        //    if (isDialogueActive) return; // Evita iniciar o diálogo novamente se já estiver ativo

        //    isDialogueActive = true;
        //    TriggerDialogue();
        //}

        //private void TriggerDialogue()
        //{
        //    Debug.Log("Diálogo iniciado com " + npcName);
        //    Debug.Log(inkJSON.text);
        //    // Exibe a UI de diálogo com o texto do NPC e a imagem do NPC
        //    dialogueUI.ShowDialogue(inkJSON.text, npcName, npcImage);
        //    // Você pode adicionar o código para iniciar o diálogo usando o Ink aqui, se necessário
        //}

        //// Método para resetar a flag ao sair do alcance do NPC
        //public void ResetDialogue()
        //{
        //    isDialogueActive = false;
        //    dialogueUI.HideDialogue(); // Esconde a UI de diálogo ao sair do alcance do NPC
        //    Debug.Log("Diálogo resetado");
        //}

    }
