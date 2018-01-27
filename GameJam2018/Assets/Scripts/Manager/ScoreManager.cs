using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UberReview
{
    public float Rating;
    public string Name;
    public float Payment;
    public string Message;

    public UberReview(float in_Rating, string in_PassengerName, float in_Payment, string in_Message)
    {
        Rating = in_Rating;
        Name = in_PassengerName;
        Payment = in_Payment;
        Message = in_Message;
    }
}

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public List<UberReview> Reviews = new List<UberReview>();

    public Canvas ReceiptScreen;

    public RawImage UI_Star1;
    public RawImage UI_Star2;
    public RawImage UI_Star3;
    public RawImage UI_Star4;
    public RawImage UI_Star5;

    public Text EarningsText;
    public Text PassangerText;


    public string WinMessage = "You Win";
    /// <summary>
    /// The current score rating being left by the Alien Pax
    /// </summary>
    public static float UberRating { get; protected set; }      

    public void Awake()
    {
        Instance = this;
    }

    public void Reset()
    {
        UberRating = 0;
        Reviews.Clear();      
    }

    public static void DamageUberRating (float in_Damage)
    {
        Debug.Assert(in_Damage > 0, "Damage must be great than 0");
        Debug.Assert(in_Damage < 5, "Damage cannot exceed 5");

        UberRating -= in_Damage;

        if (UberRating < 0)
            UberRating = 0;
    }

    public static void ImproveUberRating(float in_Improvement)
    {
        Debug.Assert(in_Improvement > 0, "Improvement must be great than 0");
        Debug.Assert(in_Improvement < 5, "Improvement cannot exceed 5");

        UberRating += in_Improvement;

        if (UberRating > 5)
            UberRating = 5;
    }

    public static void EvaluateWinCondition()
    {
        float Payment = Mathf.Round(Random.Range(5, 15)) + (Random.Range(0, 99) / 100f);
        UberReview newReview = new UberReview(UberRating, AlienAI.Instance.RacialName, Payment, string.Empty);

        Instance.Reviews.Add(newReview);
    }

    public void ShowReceiptScreen()
    {
        ReceiptScreen.gameObject.SetActive(true);

        Instance.UI_Star1.gameObject.SetActive(UberRating > 4);
        Instance.UI_Star2.gameObject.SetActive(UberRating > 3);
        Instance.UI_Star3.gameObject.SetActive(UberRating > 2);
        Instance.UI_Star4.gameObject.SetActive(UberRating > 1);
        Instance.UI_Star5.gameObject.SetActive(UberRating > 0);

        Instance.PassangerText.text = Reviews[Reviews.Count - 1].Name;
        Instance.EarningsText.text = "$" + Reviews[Reviews.Count - 1].Payment;
    }

    public void HideReceiptScreen()
    {
        ReceiptScreen.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {
        HideReceiptScreen();
    }

    // Update is called once per frame
    void Update ()
    {		
	}
}
