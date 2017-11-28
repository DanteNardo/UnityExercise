using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// In this game, High Striker, we want to display an oscillating marker on a score bar.
/// During that duration, the player wants to respond when the marker is close to the center of the score bar.
/// There are ten Trials that define the player's total score for the game. Each Trial has a different marker speed.
/// Some appropriate visual feedback is also displayed according to the player's response.
/// </summary>
public class HighStriker : GameBase
{
	const string INSTRUCTIONS = "Press <color=cyan>Spacebar</color> to get the points at the marker's location.";
	const string FINISHED = "FINISHED!";
    const string HIGH_RESPONSE = "EXCELLENT! ";
    const string MEDIUM_RESPONSE = "Good! ";
    const string LOW_RESPONSE = "Ok. ";
    const string LOWEST_RESPONSE = "Bad... ";
    const float MAX_X_POS = 10.0f;
    const int HIGH_SCORE = 10;
    const int MEDIUM_SCORE = 9;
    const float LOW_SCORE = 6;
    const float LOWEST_SCORE = 4;
    Color RESPONSE_COLOR_HIGH = Color.green;
    Color RESPONSE_COLOR_MEDIUM = Color.yellow;
    Color RESPONSE_COLOR_LOW = Color.red;
    Color RESPONSE_COLOR_LOWEST = Color.black;
    int markerDirection = 1;

    /// <summary>
    /// A reference to the UI canvas so we can instantiate the feedback text.
    /// </summary>
    public GameObject uiCanvas;
	/// <summary>
	/// The marker that will be displayed to the player.
	/// </summary>
	public GameObject marker;
    /// <summary>
    /// The score bar that will be displayed to the player.
    /// </summary>
    public GameObject scoreBar;
    /// <summary>
    /// A prefab for an animated text label that appears when a trial ends.
    /// The prefab's text value is equivalent to the player's generated score.
    /// </summary>
    public GameObject feedbackTextPrefab;
	/// <summary>
	/// The instructions text label.
	/// </summary>
	public Text instructionsText;
    /// <summary>
    /// The initial position of the marker in the world.
    /// </summary>
    private Vector3 originalMarkerPosition;


    /// <summary>
    /// Called when the game session has started.
    /// </summary>
    public override GameBase StartSession(TextAsset sessionFile)
	{
		base.StartSession(sessionFile);

		instructionsText.text = INSTRUCTIONS;
		StartCoroutine(RunTrials(SessionData));

		return this;
	}


	/// <summary>
	/// Iterates through all the trials, and calls the appropriate Start/End/Finished events.
	/// </summary>
	protected virtual IEnumerator RunTrials(SessionData data)
	{
		foreach (Trial t in data.trials)
		{
			StartTrial(t);
			yield return StartCoroutine(MoveMarker(t));
			EndTrial(t);
		}
		FinishedSession();
		yield break;
	}


	/// <summary>
	/// Displays the marker and updates it's position on the score bar.
	/// The player wants to respond when the marker's position is close to 0.
	/// </summary>
	protected virtual IEnumerator MoveMarker(Trial t)
	{
        //GameObject mark = marker;
        //      mark.SetActive(false);
        //yield return new WaitForSeconds(t.delay);

        //StartInput();
        //      mark.SetActive(true);
        //      HighStrikerData data = sessionData.gameData as HighStrikerData;
        //      Vector3 destination = originalMarkerPosition + (new Vector3(MAX_X_POS, 0, 0) * markerDirection);
        //      float elapsed = 0.0f;
        //      float distance = 0.0f;

        //      while (listenForInput)
        //      {
        //          float frac = distance / MAX_X_POS;
        //          mark.transform.position = Vector3.Lerp(mark.transform.position, destination, frac);
        //          if (mark.transform.position == destination)
        //          {
        //              destination = -destination;
        //              markerDirection = -markerDirection;
        //          }
        //          elapsed += Time.deltaTime;
        //          distance = elapsed * 1;
        //          yield return null;
        //      }

        //      mark.SetActive(false);
        //yield break;
        GameObject mark = marker;
        mark.SetActive(false);
        yield return new WaitForSeconds(t.delay);

        StartInput();
        mark.SetActive(true);

        HighStrikerData data = sessionData.gameData as HighStrikerData;
        Vector3 destination = originalMarkerPosition + (new Vector3(MAX_X_POS, 0, 0) * markerDirection);
        Vector3 newPosition = mark.transform.position;

        while (listenForInput)
        {
            newPosition = new Vector3(newPosition.x + (data.MarkerSpeed * markerDirection), newPosition.y, newPosition.z);

            // If we have reached our destination
            if ((newPosition.x > destination.x && markerDirection == 1) || (newPosition.x < destination.x && markerDirection == -1))
            {
                destination = -destination;
                markerDirection = -markerDirection;
            }
            else
            {
                mark.transform.position = newPosition;
            }
            yield return null;
        }

        mark.SetActive(false);
        yield break;
    }


	/// <summary>
	/// Called when the game session is finished.
	/// e.g. All session trials have been completed.
	/// </summary>
	protected override void FinishedSession()
	{
		base.FinishedSession();
		instructionsText.text = FINISHED;
	}


	/// <summary>
	/// Called when the player makes a response during a Trial.
	/// StartInput needs to be called for this to execute, or override the function.
	/// </summary>
	public override void PlayerResponded(KeyCode key, float time)
	{
		if (!listenForInput)
		{
			return;
		}
		base.PlayerResponded(key, time);
		if (key == KeyCode.Space)
		{
			EndInput();
			AddResult(CurrentTrial, marker.transform.position.x);
		}
	}


	/// <summary>
	/// Adds a result to the SessionData for the given trial.
    /// Stores the accuracy of the marker position when player responded.
	/// </summary>
	protected override void AddResult(Trial t, float xPos)
	{
        TrialResult r = new TrialResult(t);
		r.accuracy = GetAccuracy(xPos);
        int score = GetScore(xPos);

		if (IsHighResponse(score))
		{
			DisplayFeedback(HIGH_RESPONSE, RESPONSE_COLOR_HIGH, score);
            r.success = true;
			GUILog.Log("EXCELLENT! Score = {0}", score);
		}
		else if (IsMediumResponse(score))
		{
			DisplayFeedback(MEDIUM_RESPONSE, RESPONSE_COLOR_MEDIUM, score);
			r.success = true;
			GUILog.Log("Good! Score = {0}", score);
		}
        else if (IsLowResponse(score))
        {
            DisplayFeedback(LOW_RESPONSE, RESPONSE_COLOR_LOW, score);
            r.success = true;
            GUILog.Log("Ok. Score = {0}", score);
        }
		else
        {
            DisplayFeedback(LOWEST_RESPONSE, RESPONSE_COLOR_LOWEST, score);
            r.success = false;
            GUILog.Log("Bad... Score = {0}", score);
        }
		sessionData.results.Add(r);
	}


    /// <summary>
    /// Display visual feedback to let the player know their score for the Trial.
    /// </summary>
    private void DisplayFeedback(string text, Color color, int score)
	{
		GameObject g = Instantiate(feedbackTextPrefab);
		g.transform.SetParent(uiCanvas.transform);
		g.transform.localPosition = feedbackTextPrefab.transform.localPosition;
		Text t = g.GetComponent<Text>();
        t.text = text + "Score = " + score.ToString();
		t.color = color;
	}


    /// <summary>
    /// Returns the player's response as an int score from 0 to 10.
    /// </summary>
    protected int GetScore(float xPos)
    {
        HighStrikerData data = sessionData.gameData as HighStrikerData;

        // As x position gets larger (in either direction), score becomes lower
        return (int)(Mathf.Round(MAX_X_POS - Mathf.Abs(xPos)) / data.HorizontalScale);
    }


	/// <summary>
	/// Returns the players response accuracy.
	/// The perfect accuracy would be 1, most inaccuracy is 0.
	/// </summary>
	protected float GetAccuracy(float xPos)
	{
		// Converts score from 0-10 scale to 0.0->1.0 scale
        return GetScore(xPos) / 10.0f;
	}


	/// <summary>
	/// Returns True if the given score is between mid and high value.
	/// </summary>
	protected bool IsHighResponse(int score)
	{
        return score > MEDIUM_SCORE && score <= HIGH_SCORE;
    }


    /// <summary>
    /// Returns True if the given score is between low and mid value.
    /// </summary>
    protected bool IsMediumResponse(int score)
    {
        return score > LOW_SCORE && score <= MEDIUM_SCORE;
    }


    /// <summary>
    /// Returns True if the given score is between lowest and low value.
    /// </summary>
    protected bool IsLowResponse(int score)
    {
        return score > LOWEST_SCORE && score <= LOW_SCORE;
    }
}
