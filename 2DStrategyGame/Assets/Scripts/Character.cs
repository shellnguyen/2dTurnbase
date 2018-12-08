using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private int m_Id;
    private string m_Tag;
    [SerializeField]private Utils.Node[,] m_nodeGraph;

	// Use this for initialization
	private void Awake ()
	{
        m_nodeGraph = MainManager.Instance.NodeGraph;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		
	}
}
