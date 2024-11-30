using Sirenix.OdinInspector;
using UnityEngine;

public class LevelChangerWithTransition : LevelChanger
{
	[EnumPaging]
	[SerializeField] private TransitionPanels transitionPanel; 

	public override void ChangeLevel()
	{
		SceneTransition.Instance.ChangeSceneTransition(transitionPanel, sceneName);
	}

}
