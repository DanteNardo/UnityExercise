using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;


/// <summary>
/// Contains general Game data for the High Striker gametype.
/// </summary>
public class HighStrikerData : GameData
{
	public const string HORIZONTAL_SCALE = "horizontalScale";
    public const string MARKER_DIRECTION = "markerDirection";
    public const string MARKER_SPEED = "markerSpeed";

    /// <summary>
    /// The horizontal scale used for the marker's world space to score value.
    /// </summary>
    private float horizontalScale = 1.0f;
    /// <summary>
    /// The direction the marker is moving.
    /// </summary>
    private int markerDirection = 1;
    /// <summary>
    /// The speed of the marker moving across the score bar.
    /// </summary>
    private float markerSpeed = 1.0f;


    #region ACCESSORS

    public float HorizontalScale
    {
        get
        {
            return horizontalScale;
        }
    }
    public float MarkerSpeed
    {
        get
        {
            return markerSpeed;
        }
    }
    public int MarkerDirection
    {
        get
        {
            return markerDirection;
        }
        set
        {
            markerDirection = value == 0 || value == 1 ? value : 0;
        }
    }

    #endregion


    public HighStrikerData(XmlElement elem) 
		: base(elem)
	{
	}


	public override void ParseElement(XmlElement elem)
	{
		base.ParseElement(elem);
		XMLUtil.ParseAttribute(elem, HORIZONTAL_SCALE, ref horizontalScale);
        XMLUtil.ParseAttribute(elem, MARKER_DIRECTION, ref markerDirection);
        XMLUtil.ParseAttribute(elem, MARKER_SPEED, ref markerSpeed);
    }


	public override void WriteOutputData(ref XElement elem)
	{
		base.WriteOutputData(ref elem);
		XMLUtil.CreateAttribute(HORIZONTAL_SCALE, horizontalScale.ToString(), ref elem);
        XMLUtil.CreateAttribute(MARKER_DIRECTION, markerDirection.ToString(), ref elem);
        XMLUtil.CreateAttribute(MARKER_SPEED, markerSpeed.ToString(), ref elem);
    }
}
