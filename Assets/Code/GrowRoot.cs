using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GrowRoot : MonoBehaviour
{
    [SerializeField] private int rootAmount = 2;
    [SerializeField] private float rootGap = .1f;
    [SerializeField] private float startGap = .1f;
    [SerializeField] private float timeBtwRoots = 1;
    [SerializeField] private float _initialDelay;
    [SerializeField] private GameObject coreScan;
    [SerializeField] private Transform rootTransform;
    [SerializeField] private GameObject root;
    [SerializeField] private Sprite[] rootImages;
    [SerializeField] private Transform rootContainer;
    [SerializeField] private Transform planetDepth;

    private List<Tween> _allTween = new();


    public void StartGenerateRoots()
    {
        StartCoroutine(RootGenerator());
    }

    public void StopRoots()
    {
        for (int i = rootContainer.childCount - 1; i >= 0; i--)
        {
            rootContainer.GetChild(i).DOScale(Vector3.zero, 0.2f)
                .SetEase(Ease.OutBack)
                .SetDelay(i * 0.1f);
        }

        StopAllCoroutines();
    }

    private IEnumerator RootGenerator()
    {
        yield return new WaitForSeconds(_initialDelay);
        Vector3 currentPos = new(
                    rootTransform.position.x,
                    rootTransform.position.y,
                    planetDepth.position.z
                );

        var growingDir = -transform.up;
        var i = 0;
        while (true)
        {
            yield return new WaitForSeconds(timeBtwRoots);
            if (!UIDialogModule.Current.IsActive)
            {
                if (i >=  rootAmount) yield break;

                var gapDir = growingDir.normalized * (i == 0 ? startGap : rootGap) * (i + 1);
                GenerateRoot(currentPos + gapDir, i % 2, i);
                i++;

            }
        }
    }

    void GenerateRoot(Vector3 position, int spriteIndex, int rootIndex)
    {
        GameObject gobRoot;
        gobRoot = Instantiate(root, position, transform.rotation) as GameObject;

        gobRoot.GetComponent<SpriteRenderer>().sprite = rootImages[spriteIndex];
        if (rootIndex == rootAmount - 1)
            gobRoot.GetComponent<SpriteRenderer>().sprite = rootImages[rootImages.Length - 1];
        // Inverting images zig zig mode
        gobRoot.GetComponent<SpriteRenderer>().flipX = rootIndex % 2 == 0;
        gobRoot.transform.parent = rootContainer;

        var initalScale = gobRoot.transform.localScale;

        _allTween.Add(
            gobRoot.transform.DOScale(initalScale, 0.2f)
                .SetEase(Ease.OutBack)
        );
        gobRoot.transform.localScale = Vector3.zero;

        _allTween.Add(
            coreScan.transform.DOMove(gobRoot.transform.position, 0.2f)
        );
    }

    private void OnDestroy()
    {
        foreach (var item in _allTween)
        {
            item?.Kill();
        }
    }

    void OnDrawGizmos()
    {
        // Debug.DrawRay(transform.position, )
    }

    // Update is called once per frame
    void Update()
    {

    }
}
