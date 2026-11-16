using System.Collections.Generic;
using UnityEngine;

public class MiniGameGainLossGraph : MonoBehaviour
{
	private float width;

	private GameObject graphObject;

	private GameObject filledTopBackgroundObject;

	private GameObject filledBottomBackgroundObject;

	private GameObject filledContainerObject;

	private List<double> plotPoints;

	private Vector2 origin;

	private float plotLineSize;

	private Color axisColor;

	private float verticalDivisionPixels;

	private int numberofDivisions;

	private double graphCeilingValue;

	private double divisionValueIncrement;

	private bool negativeControlledGraph;

	private int submissiveSideDivisions;

	public void InitWithParameters(float width, List<double> thePlotPoints, int horizontalDivisions, string title)
	{
	}

	private void CalculateDivisions(double largestValue)
	{
	}

	private string GetTruncatedLabelFromValue(double value)
	{
		return null;
	}

	private void AddLabel(string label, Vector2 pos, int fontSize)
	{
	}

	private void DashedLine(float yPos, GameObject parent)
	{
	}

	private void AddEndCap(Vector2 pos, float size, Color color)
	{
	}

	private void AddConnectionLine(Vector2 startPos, Vector2 endPos, float size, Color color)
	{
	}
}
