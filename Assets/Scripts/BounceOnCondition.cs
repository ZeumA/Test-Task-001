using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BounceOnCondition : MonoBehaviour
{
    public delegate void OnSetup();

    public event OnSetup Complete;

    public bool isRight;

    public Button button;
    public GameObject Particles;

    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(this.transform.DOScale(new Vector3(0, 0, 1), 0.3f));
        seq.Append(this.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f));
        seq.Append(this.transform.DOScale(new Vector3(0.85f, 0.85f, 1), 0.3f));
        seq.Append(this.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f));
    }

    void OnComplete()
    {
        if(Complete != null)
        {
            Complete();
        }
    }

    void RestoreButton()
    {
        button.interactable = true;
        if (isRight)
            OnComplete();
    }

    public void OnClick()
    {

        if (isRight)
        {
            Instantiate(Particles, button.gameObject.transform);
            button.interactable = false;
            var seq = DOTween.Sequence();
            seq.Append(this.transform.DOScale(new Vector3(0, 0, 1), 0.3f));
            seq.Append(this.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.3f));
            seq.Append(this.transform.DOScale(new Vector3(0.85f, 0.85f, 1), 0.3f));
            seq.Append(this.transform.DOScale(new Vector3(1f, 1f, 1), 0.3f));

            seq.OnComplete(RestoreButton);
        }
        else 
        {
            button.interactable = false;
            var seq = DOTween.Sequence();
            seq.Append(
                this.transform.DOMove(this.transform.position - new Vector3(20, 0), 0.1f));
            seq.Append(
                this.transform.DOMove(this.transform.position + new Vector3(20, 0), 0.1f));
            seq.Append(
                this.transform.DOMove(this.transform.position - new Vector3(10, 0), 0.1f));
            seq.Append(
                this.transform.DOMove(this.transform.position, 0.1f));

            seq.OnComplete(RestoreButton);
        }
    }
}
