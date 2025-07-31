using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
public class DialogueProgression : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] PlayableDirector timelineDirector;
    [SerializeField] Text dialogueText;
    public string[] dialogueStrings;
    int progress = 0;
    bool canAdvance = true;

    private void Start()
    {
        timelineDirector.Play();
        StartCoroutine(SceneTransition.instance.TransitionIn());
        //StartDialogue();
    }

    void Update()
    {
        // Using old input system because it was easier/quicky to input this as a fix to the following issue: One press of "space" cause the dialogue to jump about 4 lines ahead
        if (Input.GetKeyDown(KeyCode.Space) && canAdvance)
        {
            OnClick();
        }
    }

    public void StartDialogue()
    {
        //timelineDirector.Pause();
        timelineDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
        progress = 0;
        dialogueText.text = dialogueStrings[progress];
        canvas.SetActive(true);
    }

    // Also works on space bar, I just named it poorly :D
    public void OnClick()
    {
        progress++;
        if (progress >= dialogueStrings.Length)
        {
            Debug.Log("Dialogue progress out of bounds, resuming timeline");
            canvas.SetActive(false);
            timelineDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        } else
        {
            dialogueText.text = dialogueStrings[progress];
        }
    }

}
