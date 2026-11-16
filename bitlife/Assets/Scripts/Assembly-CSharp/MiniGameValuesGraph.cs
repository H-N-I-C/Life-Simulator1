using System.Collections.Generic;
using UnityEngine;

public class MiniGameValuesGraph : MonoBehaviour
{
	private GameObject graphObject;

	private GameObject filledBackgroundObject;

	private float width;

	private Vector2 origin;

	private List<double> plotPoints;

	private float plotLineSize;

	private Color axisColor;

	private float verticalSegmentSpacer;

	private bool negativeDirection;

	private int numberofDivisions;

	private double graphCeiling;

	private double divisionIncrement;

	public void InitWithParameters(float width, List<double> thePlotPoints, int horizontalDivisions, string title)
	{
	}

	private string GetTruncatedLabelFromValue(double value)
	{
		return null;
	}

	private void CalculateDivisions(double largestValue)
	{
	}

	private void DashedLine(float yPos)
	{
	}

	private void AddLabel(string label, Vector2 pos, int fontSize)
	{
	}

	private void AddEndCap(Vector2 pos, float size, Color color)
	{
	}

	private void AddConnectionLine(Vector2 startPos, Vector2 endPos, float size, Color color)
	{
	}

	private void PieChart()
	{
	}
}
