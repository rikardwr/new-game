using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public GameObject gameObj;
    public GameObject gameObj2;
    

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        gameObj2.SetActive(true);
        if (gameObj2 == true)
        {

            animator.SetBool("isOpen", true);
            Cursor.visible = true;
            gameObj.SetActive(false);


            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
                DisplayNextSentence();
            
        }
    }
    public void DisplayNextSentence()
    {
        //if (other.tag == "Player" && Input.GetKeyDown(KeyCode.Space))
        //{
            if (sentences.Count == 0)
            {
                EndDialogue();
                Cursor.visible = false;
                return;
            }


            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        //}
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        gameObj2.SetActive(false);

    }
    }
