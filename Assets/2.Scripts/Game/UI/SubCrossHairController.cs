using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SubCrossHairController : MonoBehaviour
{
    [SerializeField]
    GameObject[] _crosses;

    [SerializeField]
    float _appearTime;
    [SerializeField]
    float _disappearTime;

    readonly Color _subcrossHairColor = Color.white;

    float _appearDeltaSize;
    float _disappearDeltaAlpha;

    Vector2 _crossSizedelta;

    Coroutine _currentKillEffect;

    Image[] _crossImages;

    void Start()
    {
        _crossImages = new Image[4];

        for (int i = 0; i < 4; i++)
        {
            Image cross = _crosses[i].GetComponent<Image>();
            cross.color = _subcrossHairColor;
            cross.gameObject.SetActive(false);

            _crossImages[i] = cross;
        }

        _crossSizedelta = _crossImages[0].rectTransform.sizeDelta;
        _appearDeltaSize = _crossSizedelta.y / _appearTime;
        _disappearDeltaAlpha = 1 / _disappearTime;
    }

    public void CrossHair_KillEffect()
    {
        Debug.Log("CrossHair_KillEffect");

        if (_currentKillEffect != null)
        {
            StopCoroutine(_currentKillEffect);
        }
        ResetCrossHair();
        _currentKillEffect = StartCoroutine(Co_KillEffect());
    }

    IEnumerator Co_KillEffect()
    {
        Debug.Log("Co_KillEffect");
        float checkTime = 0;

        for (int i = 0; i < 4; i++)
        {
            _crossImages[i].gameObject.SetActive(true);
        }

        while (checkTime < _appearTime)
        {
            checkTime += Time.deltaTime;

            for (int i = 0; i < 4; i++)
            {
                _crossImages[i].rectTransform.sizeDelta = new Vector2(_crossSizedelta.x, _appearDeltaSize * checkTime);
            }

            yield return null;
        }

        for (int i = 0; i < 4; i++)
        {
            _crossImages[i].rectTransform.sizeDelta = _crossSizedelta;
        }

        checkTime = 0;
        while (checkTime < _disappearTime)
        {
            checkTime += Time.deltaTime;

            for (int i = 0; i < 4; i++)
            {
                _crossImages[i].color = new Color(_crossImages[i].color.r, _crossImages[i].color.g, _crossImages[i].color.b, 1 - checkTime *_disappearDeltaAlpha);
            }
            yield return null;
        }
        ResetCrossHair();
    }

    void ResetCrossHair()
    {
        for (int i = 0; i < 4; i++)
        {
            _crossImages[i].rectTransform.sizeDelta = new Vector2(_crossSizedelta.x , 0f);
            _crossImages[i].color = new Color(_subcrossHairColor.r, _subcrossHairColor.g, _subcrossHairColor.b, 1f);
            _crossImages[i].gameObject.SetActive(false);
        }
        _currentKillEffect = null;
    }    
}
