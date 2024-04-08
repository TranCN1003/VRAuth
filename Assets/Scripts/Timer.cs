using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameObject nextPage; // Reference to the next UI page
    public GameObject thisPage; // Reference to the current UI page
    public TextMeshProUGUI textBox;  // Reference to text box to update
    public float displayTime = 5f; // Time in seconds to display this page
    public float appendInterval = 1f;

    private float timer = 0f;
    private Coroutine appendCoroutine;

    private void OnEnable()
    {
        // Start the coroutine when the GameObject is enabled
        appendCoroutine = StartCoroutine(AppendTextRoutine());
    }

    private void OnDisable()
    {
        // Stop the coroutine when the GameObject is disabled
        if (appendCoroutine != null)
        {
            StopCoroutine(appendCoroutine);
            appendCoroutine = null;
        }
    }

    private IEnumerator AppendTextRoutine()
    {
        while (true)
        {
            // Append the new text to the existing text
            textBox.text += ".";

            // Wait for the specified interval before appending again
            yield return new WaitForSeconds(appendInterval);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Check if it's time to switch to the next page
        if (timer >= displayTime)
        {
            // Reset timer and text box content
            timer = 0f;
            textBox.text = "";

            // Activate the next page and deactivate this page
            thisPage.SetActive(false);
            nextPage.SetActive(true);
        }
    }
}
