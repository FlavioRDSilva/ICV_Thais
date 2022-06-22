using UnityEngine;

namespace FayvitBasicTools
{
    public interface IFadeView
    {
        bool Darken { get; set; }
        bool Lighten { get; set; }
        void ClearFade();
        void StartFadeOut(Color fadeColor = default(Color),float darkenTime=0);
        void StartFadeOutWithAction(System.Action acao, Color fadeColor = default(Color));
        void StartFadeOutWithAction(System.Action acao, float darkenTime, Color fadeColor = default(Color));
        void StartFadeInWithAction(System.Action acao, Color fadeColor = default(Color));
        void StartFadeInWithAction(System.Action acao, float lightenTime, Color fadeColor = default(Color));
        void StartFadeIn(Color fadeColor = default(Color),float lightenTime=0);
    }
}
