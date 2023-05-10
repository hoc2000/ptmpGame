using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using My.Tool;

public class ButtonScaler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 startScale;
    public Vector2 endScale;
    [SerializeField] bool useAudio = true;

    [SerializeField] bool useAnimLoop;
    //#if UNITY_EDITOR
    //    private void OnValidate()
    //    {
    //        startScale = transform.localScale;

    //        endScale = new Vector2(startScale.x + 0.1f, startScale.y + 0.1f);
    //    }
    //#endif
    void Start()
    {
        if (useAnimLoop)
        {
            AnimButtonReward();
            //this.RegisterListener(EventID.AnimComplete, (sender, pram) => AnimButtonReward());
            //this.RegisterListener(EventID.CloseAnim, (sender, pram) => KillDotwen());
        }
    }

    //private void OnEnable()
    //{
    //    if (useAnimLoop)
    //    {
    //        AnimButtonReward();
    //        //this.RegisterListener(EventID.AnimComplete, (sender, pram) => AnimButtonReward());
    //        //this.RegisterListener(EventID.CloseAnim, (sender, pram) => KillDotwen());
    //    }

    //}
    //private void OnDisable()
    //{
    //    //if (useAnimLoop)
    //    //{
    //    //    this.RemoveListener(EventID.AnimComplete, (sender, pram) => AnimButtonReward());
    //    //    this.RemoveListener(EventID.CloseAnim, (sender, pram) => KillDotwen());
    //    //}  
    //}
    public void OnPointerDown(PointerEventData eventData)
    {
        // transform.DOScale(endScale, 0.1f).SetEase(Ease.Linear);
        if (useAudio)
            AudioManager.Instance.Shot("button");

        //VibrationManager.Instance.VibrateSelection();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        //transform.DOScale(startScale, 0.1f).SetEase(Ease.Linear);
    }

    void AnimButtonReward()
    {
        transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    //void KillDotwen()
    //{
    //    DOTween.Kill(transform);
    //}
}
