using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAchievement : MonoBehaviour 
{
	private Animator _animator;
	private void Start()
	{
		_animator = GetComponent<Animator>();
	}
	public void PlayUiAnimation()
	{
		_animator.SetTrigger("AnimateIn");
	}	
}
