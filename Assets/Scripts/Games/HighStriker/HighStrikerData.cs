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
    public const string MARKER_SPEED = "markerSpeed";

    /// <summary>
    /// The horizontal scale used for the marker's world space to score value.
    /// </summary>
    private float horizontalScale = 1.0f;
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

    #endregion


    public HighStrikerData(XmlElement elem) 
		: base(elem)
	{
	}


	public override void ParseElement(XmlElement elem)
	{
		base.ParseElement(elem);
		XMLUtil.ParseAttribute(elem, HORIZONTAL_SCALE, ref horizontalScale);
        XMLUtil.ParseAttribute(elem, MARKER_SPEED, ref markerSpeed);
    }


	public override void WriteOutputData(ref XElement elem)
	{
		base.WriteOutputData(ref elem);
		XMLUtil.CreateAttribute(HORIZONTAL_SCALE, horizontalScale.ToString(), ref elem);
        XMLUtil.CreateAttribute(MARKER_SPEED, markerSpeed.ToString(), ref elem);
    }
}
