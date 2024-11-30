using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public enum TransitionPanels
{
	NONE = 0,
	WHITE = 1,
}

public class SceneTransition : MonoBehaviour
{
    private const string trigger_fadein = "fade_in";
    private const string trigger_fadeout = "fade_out";
    private const string anim_fadevisible = "scene_transition_fade_visible";
    [SerializeField] private SceneTransitionPanel[] panels;

	private Animator animator;
	private WaitForSeconds delayFade = new WaitForSeconds(0.1f);

    public static SceneTransition Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
		
		animator = GetComponent<Animator>();
	}

	public void ChangeSceneTransition(TransitionPanels transitionName, string sceneName)
	{
		StartCoroutine(ChangeSceneTransitionSequence(transitionName, sceneName));
	}

	private IEnumerator ChangeSceneTransitionSequence(TransitionPanels transitionName, string sceneName)
	{
		HideAllPanels();
		ShowPanel(transitionName);
		
		animator.SetTrigger(trigger_fadein);

		while(!animator.GetCurrentAnimatorStateInfo(0).IsName(anim_fadevisible))
			yield return delayFade;
		

		LevelManager.Instance.LoadScene(sceneName);
		animator.SetTrigger(trigger_fadeout);
	}

	private void HideAllPanels()
	{
		for(int a=0; a<panels.Length; a++)
			panels[a].panelReference.enabled = false;
	}

	private void ShowPanel(TransitionPanels transitionName)
	{
		for(int a=0; a<panels.Length; a++)
		{
			if(panels[a].panelName == transitionName)
			{
				panels[a].panelReference.enabled = true;
				return;
			}
		}
	}
}

[Serializable]
public class SceneTransitionPanel
{
	[EnumPaging]
	public TransitionPanels panelName;
	public Canvas panelReference;
}