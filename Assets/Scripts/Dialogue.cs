using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public float typingSpeed;
    public string[] sentences;
    public GameObject player;
    public TextMeshProUGUI textDisplay;
    public GameObject continueButton;
    public GameObject nextLevelButton;

    private int index = 0;
    private Rigidbody2D playerRb;
    private GameObject shootingSCript;
    private GameObject movementSCript;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody2D>();
        shootingSCript = GameObject.FindWithTag("Player");
        shootingSCript.GetComponent<Weapon>().enabled = true;
        movementSCript = GameObject.FindWithTag("Player");
        movementSCript.GetComponent<PlayerController>().enabled = true;
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index])
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            nextLevelButton.SetActive(true);
            textDisplay.text = "";
            continueButton.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerRb.velocity = Vector2.zero;
            shootingSCript.GetComponent<Weapon>().enabled = false;
            movementSCript.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(Type());
        }
    }
}
