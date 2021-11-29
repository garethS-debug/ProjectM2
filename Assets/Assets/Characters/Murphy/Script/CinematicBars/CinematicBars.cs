using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CinematicBars : MonoBehaviour
{

    public static CinematicBars Instance { get; private set; }

    [SerializeField] private GameObject cinematicBarContainerObj;
    [SerializeField] private Animator cinematicBarAnimator;

    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    
    public void ShowBars()
    {
        cinematicBarContainerObj.SetActive(true);
    }

    public void HideBars()
    {
        if (cinematicBarContainerObj.activeSelf)
        {
            StartCoroutine(HideAndDisable());
        }
    }

    private IEnumerator HideAndDisable()
    {
        cinematicBarAnimator.SetTrigger("HideBars");
       // cinematicBarAnimator.SetBool(")

        yield return new WaitForSeconds(0.5f);

        cinematicBarContainerObj.SetActive(false);

    }


    //private RectTransform topBar, bottomBar;
    //private float changeSizeAmount;
    //public float targetSize;

    //public bool Test;

    //// Start is called before the first frame update
    //void Awake()
    //{
    //    Test = false;

    //    GameObject gameObject = new GameObject("topBar", typeof(Image));
    //    gameObject.transform.SetParent(transform, false);
    //    gameObject.GetComponent<Image>().color = Color.black;
    //    topBar = gameObject.GetComponent<RectTransform>();
    //    topBar.anchorMin = new Vector2(0, 1);
    //    topBar.anchorMax = new Vector2(1, 1);
    //    topBar.sizeDelta = new Vector2(0, 100);

    //    gameObject  = new GameObject("bottomBar", typeof(Image));
    //    gameObject.transform.SetParent(transform, false);
    //    gameObject.GetComponent<Image>().color = Color.black;
    //    topBar = gameObject.GetComponent<RectTransform>();
    //    topBar.anchorMin = new Vector2(0, 0);
    //    topBar.anchorMax = new Vector2(1, 0);
    //    topBar.sizeDelta = new Vector2(0, 100);

    //}

    //private void Update()
    //{
    //    Vector2 sizeDelta = topBar.sizeDelta;
    //    sizeDelta.y += changeSizeAmount * Time.deltaTime;
    //    topBar.sizeDelta = sizeDelta;
    //    bottomBar.sizeDelta = sizeDelta;

    //    while (Test == true)
    //    {
    //        Show(600, 0.3f);
    //        break;
    //    }

    //    while (Test == false)
    //    {
    //        Hide(.3f);
    //        break;
    //    }
    //}

    //public void   Show (float targetSize, float time)
    //{
    //    this.targetSize = targetSize;
    //    changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
    //}

    //public void Hide( float time)
    //{
    //    this.targetSize = 0f;
    //    changeSizeAmount = (targetSize - topBar.sizeDelta.y) / time;
    //}


}
