using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FayvitUI
{
    public class FayvitUiUtility
    {
        public static Vector2 GetMinPositionInTheParent(RectTransform pai, RectTransform filho)
        {
            return new Vector2(filho.anchorMin.x + filho.offsetMin.x / pai.rect.width,
                1 - (filho.anchorMax.y + filho.offsetMax.y / pai.rect.height));
        }

        public static Vector2 GetMaxPositionInTheParent(RectTransform pai, RectTransform filho)
        {
            Vector2 extends = GetPercentExtendsInTheParent(pai.rect, filho.rect);
            Vector2 min = GetMinPositionInTheParent(pai, filho);
            return new Vector2(extends.x + min.x, extends.y + min.y);
        }

        public static Vector2 GetPercentExtendsInTheParent(Rect pai, Rect filho)
        {
            return new Vector2(filho.width / pai.width, filho.height / pai.height);
        }

        #region tentativaGetInTheCanvas
        //public static Vector4 GetMinMaxPositionInTheCanvas(RectTransform filho)
        //{
        //    Vector4 r = Vector4.zero;
        //    RectTransform pai = filho.parent.GetComponent<RectTransform>();

        //    Vector2 min = GetMinPositionInTheParent(pai, filho);
        //    Vector2 max = GetMaxPositionInTheParent(pai, filho);
        //    r = new Vector4(min.x, min.y, max.x, max.y);
        //    Vector2 extends;
        //    while (pai.parent != null)
        //    {
        //        filho = pai;
        //        pai = pai.parent.GetComponent<RectTransform>();
        //        extends = GetPercentExtendsInTheParent(pai.rect, filho.rect);
        //        min = GetMinPositionInTheParent(pai, filho);
        //        max = GetMaxPositionInTheParent(pai, filho);
        //        Debug.Log("ant min: " + min + "ant max: " + max + " extends: " + extends+" ant r"+r+" nomes: "+pai+" : "+filho);
        //        r = new Vector4(min.x + r.x * extends.x, min.y + r.y * extends.y, max.x + r.z * extends.x, max.y + r.w * extends.y);

        //    }
        //    return r;
        //}
        #endregion
    }
}
