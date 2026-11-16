using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIHierarchy : MonoBehaviour
{
	[Serializable]
	private class Node
	{
		public string name;

		public RectTransform rectTransform;

		public Vector2 anchorMin;

		public Vector2 anchorMax;

		public Vector2 anchoredPosition;

		public Vector2 size;

		public Vector2 sizeDelta;

		public Vector2 pivot;

		public Vector2 offsetMin;

		public Vector2 offsetMax;

		public Vector2 scale;

		public bool useAllActiveChildren;

		public List<Node> children;
	}

	private UIHierarchyNode nodePrefab;

	private Transform Content;

	private List<Node> TreeNodes;

	private string indentStep;

	private string currentIndent;

	private int currentIndentCount;

	private void OnEnable()
	{
	}

	private void PresentData()
	{
	}

	private void PresentTree(Node treeNode)
	{
	}

	private void SetIndent()
	{
	}

	public void FillData()
	{
	}

	private void FillTree(Node treeNode)
	{
	}

	private void FillNodeData(Node node)
	{
	}
}
