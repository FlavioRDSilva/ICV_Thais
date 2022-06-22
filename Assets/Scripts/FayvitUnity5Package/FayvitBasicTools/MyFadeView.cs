using FayvitMessageAgregator;
using FayvitSupportSingleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FayvitBasicTools
{
    public class MyFadeView : MonoBehaviour, IFadeView
    {
#pragma warning disable 0649
        [SerializeField] private Image escurecedorUpper;
        [SerializeField] private Image escurecedorLower;
#pragma warning restore 0649
        private Color corDoFade = Color.black;
        private FaseDaqui fase = FaseDaqui.emEspera;
        private float tempoDeEscurecimento = 1;
        private float tempoDecorrido = 0;
        private float tempoBaseDoEscurecimento = 1;
        private float tempoBaseDoClareamento = 0.75f;
        private System.Action acaoDoFade;


        private bool escurecer = false;
        private bool clarear = false;

        public bool Darken { get {return escurecer; } set { escurecer = value; } }
        public bool Lighten { get { return clarear; } set { clarear = value; } }

        private enum FaseDaqui
        {
            emEspera,
            escurecendo,
            clareando
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (escurecer)
            {
                StartFadeOut();
                escurecer = false;
            }

            if (clarear)
            {
                StartFadeIn();
                clarear = false;
            }
            switch (fase)
            {
                case FaseDaqui.escurecendo:
                    tempoDecorrido += Time.fixedDeltaTime;
                    corDoFade.a = tempoDecorrido / tempoDeEscurecimento;
                    escurecedorUpper.color = corDoFade;
                    escurecedorLower.color = corDoFade;

                    if (tempoDecorrido > tempoDeEscurecimento)
                    {
                        fase = FaseDaqui.emEspera;
                        ChamarAcao();
                        MessageAgregator<FadeOutComplete>.Publish();
                        //EventAgregator.Publish(EventKey.fadeOutComplete, null);
                    }
                    break;
                case FaseDaqui.clareando:
                    tempoDecorrido += Time.fixedDeltaTime;
                    corDoFade.a = (tempoDeEscurecimento - tempoDecorrido) / tempoDeEscurecimento;
                    escurecedorUpper.color = corDoFade;
                    escurecedorLower.color = escurecedorUpper.color;
                    if (tempoDecorrido > tempoDeEscurecimento)
                    {
                        fase = FaseDaqui.emEspera;
                        //EventAgregator.Publish(EventKey.fadeInComplete, null);
                        MessageAgregator<FadeInComplete>.Publish();

                        ChamarAcao();

                        escurecedorLower.gameObject.SetActive(false);
                        escurecedorUpper.gameObject.SetActive(false);
                    }
                    break;
            }
        }

        public void ClearFade()
        {
            escurecedorUpper.color = new Color(0, 0, 0, 0);
            escurecedorLower.color = new Color(0, 0, 0, 0);
        }

        void ComunsDeFadeOut(Color corDoFade)
        {
            MessageAgregator<FadeOutStart>.Publish();
            //EventAgregator.Publish(EventKey.fadeOutStart, null);
            escurecedorLower.gameObject.SetActive(true);
            escurecedorUpper.gameObject.SetActive(true);
            this.corDoFade = corDoFade;
            this.corDoFade.a = 0;
            fase = FaseDaqui.escurecendo;
            tempoDecorrido = 0;
        }




        IEnumerator AcaoDequadro(System.Action acao)
        {
            yield return new WaitForEndOfFrame();
            acaoDoFade += acao;
        }

        void ChamarAcao()
        {
            if (acaoDoFade != null)
            {
                acaoDoFade();
                acaoDoFade = null;
            }
        }




        void ComunsDoFadeIn(Color corDoFade)
        {
            this.corDoFade = corDoFade;
            this.corDoFade.a = 1;
            fase = FaseDaqui.clareando;
            tempoDecorrido = 0;
        }


        public void StartFadeOut(Color fadeColor = default(Color),float darkenTime=0)
        {
            if (darkenTime <= 0)
                tempoDeEscurecimento = tempoBaseDoEscurecimento;
            else
                tempoDeEscurecimento = darkenTime;
            ComunsDeFadeOut(fadeColor);
        }

        public void StartFadeOutWithAction(System.Action acao, Color fadeColor = default(Color))
        {
            StartFadeOutWithAction(acao, 1, corDoFade);
        }

        public void StartFadeOutWithAction(System.Action acao, float darkenTime, Color fadeColor = default(Color))
        {
            this.tempoDeEscurecimento = darkenTime;
            ComunsDeFadeOut(fadeColor);

            SupportSingleton.Instance.InvokeOnEndFrame(acao);

        }

        public void StartFadeInWithAction(System.Action acao, Color fadeColor = default(Color))
        {
            StartFadeInWithAction(acao, 1, fadeColor);
        }

        public void StartFadeInWithAction(System.Action acao, float lightenTime, Color fadeColor = default(Color))
        {
            this.tempoDeEscurecimento = lightenTime;
            ComunsDoFadeIn(fadeColor);
            SupportSingleton.Instance.InvokeOnEndFrame(acao);
        }

        public void StartFadeIn(Color fadeColor = default(Color),float lightenTime = 0)
        {
            if (lightenTime <= 0)
                tempoDeEscurecimento = tempoBaseDoClareamento;
            else
                tempoDeEscurecimento = lightenTime;
            ComunsDoFadeIn(fadeColor);
            MessageAgregator<FadeInStart>.Publish();
            //EventAgregator.Publish(EventKey.fadeInStart, null);
        }
    }

    public struct FadeInStart : IMessageBase { }
    public struct FadeOutStart : IMessageBase { }
    public struct FadeOutComplete : IMessageBase { }
    public struct FadeInComplete : IMessageBase { }
}