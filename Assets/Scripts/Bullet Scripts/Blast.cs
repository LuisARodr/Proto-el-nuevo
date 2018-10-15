using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class Blast : PooledObjectBehavior {
    
    [SerializeField]
    GameObject ring, blast;

    [SerializeField, Range(0, 2)]
    float scaleFactor;
    [SerializeField, Range(0, 1)]
    float ringInitialScaleFactor;
    [SerializeField, Range(0, 1)]
    float ringDisappearingTime;
    [SerializeField, Range(0, 1)]
    float blastStartDecresingTime;
    [SerializeField, Range(0, 5)]
    float blastScaleDecresingMultiplier;

    Vector3 ringInitialScale, blastInitialScale;
    
    protected override void Start () {
        base.Start();
        ringInitialScale = new Vector3(ringInitialScaleFactor, ringInitialScaleFactor, ringInitialScaleFactor);
        blastInitialScale = blast.transform.localScale;
        ring.transform.localScale = ringInitialScale;
    }
	
	protected override void Update () {
        base.Update();
        float scale = scaleFactor * Time.deltaTime;
        //Blast scale
        if (lifeTime > maxLifeTime * blastStartDecresingTime && blast.transform.localScale.x > 0f) {
            float scaleMultiplier = scale * blastScaleDecresingMultiplier;
            blast.transform.localScale -= new Vector3(scaleMultiplier, scaleMultiplier, scaleMultiplier);
        }
        //Ring scale
        if (lifeTime < maxLifeTime * ringDisappearingTime) {
            ring.transform.localScale += new Vector3(scale, scale, scale);
        }
        else {
            ring.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }

    protected override void ReturnBulletToPool() {
        ring.transform.localScale = ringInitialScale;
        blast.transform.localScale = blastInitialScale;
        base.ReturnBulletToPool();
    }
}
