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
	[SerializeField] private SceneTransitionPanel[] panels;

	private Animator animator;

    public static SceneTransition Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
		
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
		
		animator.SetTrigger("fade_in");

		while(!animator.GetCurrentAnimatorStateInfo(0).IsName("scene_transition_fade_visible"))
			yield return new WaitForSeconds(0.1f);
		
		LevelManager.Instance.LoadScene(sceneName);
		animator.SetTrigger("fade_out");
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