using UnityEngine;
using UnityEngine.UI;
using FayvitSupportSingleton;

namespace FayvitUI
{
    public class SupportCreationUi : MonoBehaviour
    {
        [SerializeField] private InteractiveUiReference iRef;
        [SerializeField] private SingleMessagePanel singleRef;

        private Canvas canvasAtivo;

        private enum UiType
        {
            basicMenu,
            textAndImageMenu,
            imageGridMenu
        }

        private static SupportCreationUi instance;

        public static SupportCreationUi Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject G = new GameObject();
                    G.name = "Fayvit_SupportCreationUI";
                    DontDestroyOnLoad(G);

                    instance = G.AddComponent<SupportCreationUi>();

                    Debug.LogWarning("SupportCreationUI tem elementos que deveriam ser setados no inspector, " +
                        "ele não deve ser criado em tempo de jogo");
                }

                return instance;

            }
        }
        // Use this for initialization
        void Start()
        {
            SupportCreationUi[] g = FindObjectsOfType<SupportCreationUi>();

            if (g.Length > 1)
                Destroy(gameObject);
            else
                instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }

        Canvas CriarCanvas()
        {
            if (canvasAtivo == null)
            {
                GameObject G = new GameObject();

                G.name = "Script Canvas";
                canvasAtivo = G.AddComponent<Canvas>();
                canvasAtivo.renderMode = RenderMode.ScreenSpaceOverlay;

                CanvasScaler canS = G.AddComponent<CanvasScaler>();
                canS.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canS.referenceResolution = new Vector2(800, 600);
                canS.matchWidthOrHeight = .5f;

                G.AddComponent<GraphicRaycaster>();
            }
            return canvasAtivo;
        }

        void SetCanvasPercentSize(RectTransform rt, float xStart, float yStart, float xEnd, float yEnd)
        {
            rt.anchorMin = new Vector2(xStart, 1 - yEnd);
            rt.anchorMax = new Vector2(xEnd, 1 - yStart);//0.5f * Vector2.one;
            rt.anchoredPosition = Vector2.zero;
            rt.offsetMin = Vector2.zero;//new Vector2(xStart * rtt.rect.width, yStart * rtt.rect.height);
            rt.offsetMax = Vector2.zero;
        }

        /// <summary>
        /// Cria um menu basico. Os parametros devem ser usados entre zero e um e se referem a percentagem no Canvas
        /// </summary>
        public InteractiveUiReference CreateInteractiveUi(
            InteractiveUiReference mRef,
            float xStart, float yStart, float xEnd, float yEnd)
        {
            Canvas canvasAlvo = CriarCanvas();
            InteractiveUiReference menu = Instantiate(mRef);
            menu.gameObject.name = "referencia Clone";
            menu.transform.SetParent(canvasAlvo.transform);
            RectTransform rt = menu.bMenu.GetTransformContainer.GetComponent<RectTransform>();
            //RectTransform rtt = canvasAlvo.GetComponent<RectTransform>();

            SetCanvasPercentSize(rt, xStart, yStart, xEnd, yEnd);


            //SupportSingleton.Instance.InvokeOnEndFrame(() =>
            //{
            //    AnOption[] As = rt.GetComponentsInChildren<AnOption>();
            //    RectTransform pai = As[0].transform.parent.GetComponent<RectTransform>();


            //    foreach (var A in As)
            //    {
            //        RectTransform rrtt = A.GetComponent<RectTransform>();
            //        //A.GetComponentInChildren<Text>().resizeTextForBestFit = true;

            //        rrtt.sizeDelta = new Vector2(rrtt.sizeDelta.x, pai.rect.height / (pai.childCount - 1));
            //    }
            //});
            return menu;

        }

        /// <summary>
        /// Cria um painel de mensagem simples. Os parametros devem ser usados entre zero e um e se referem a percentagem no Canvas
        /// </summary>
        /// <param name="xStart"></param>
        /// <param name="yStart"></param>
        /// <param name="xEnd"></param>
        /// <param name="yEnd"></param>
        /// <returns></returns>
        public SingleMessagePanel CreateSingleMessagePanel(float xStart, float yStart, float xEnd, float yEnd)
        {
            Canvas canvasAlvo = CriarCanvas();
            SingleMessagePanel single = Instantiate(singleRef);
            single.transform.SetParent(canvasAlvo.transform);
            RectTransform rt = single.GetComponent<RectTransform>();

            SetCanvasPercentSize(rt, xStart, yStart, xEnd, yEnd);
            single.StartMessagePanel(() => { });

            return single;
        }
    }

    /*
     using FayvitUI;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class TesteCreateUI : MonoBehaviour
    {
        [SerializeField] private MenuBehaviour menuRef;
        [SerializeField] private GridMenuBehaviour gridRef;
        [SerializeField] private SingleMessagePanelBehaviour singleRef;
        [Header("Percent Arguments")]
        [SerializeField,Range(0,1)] private float xStart = 0;
        [SerializeField, Range(0, 1)] private float yStart = 0;
        [SerializeField, Range(0, 1)] private float xEnd = .1f;
        [SerializeField, Range(0, 1)] private float yEnd = .1f;

        private Canvas canvasAlvo;
        private RectTransform rt;

        // Start is called before the first frame update
        void Start()
        {
            CriarCanvas();
            //CriarUiImage();
            //CriarMenuPorScript();
            //CriarGridMenuPorScript();
            CriarPainelUmaMensagemPorScript();
        }


        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) Refazer();
        }

        void CriarPainelUmaMensagemPorScript()
        {
            SingleMessagePanelBehaviour single = Instantiate(singleRef);

            SetRectTransform(single.transform);

            SetCanvasPercentSize();
            single.baseSingle.StartMessagePanel(() => { });
        }

        void SetRectTransform(Transform target)
        {
            target.transform.SetParent(canvasAlvo.transform);
            rt = target.GetComponent<RectTransform>();
        }

        void SetCanvasPercentSize()
        {
            rt.anchorMin = new Vector2(xStart, 1-yEnd);
            rt.anchorMax = new Vector2(xEnd, 1-yStart);//0.5f * Vector2.one;
            rt.anchoredPosition = Vector2.zero;
            rt.offsetMin = Vector2.zero;//new Vector2(xStart * rtt.rect.width, yStart * rtt.rect.height);
            rt.offsetMax = Vector2.zero;
        }

        void CriarGridMenuPorScript()
        {

            GridMenuBehaviour grid = Instantiate(gridRef);
            grid.transform.SetParent(canvasAlvo.transform);
            rt = grid.GetComponent<RectTransform>();

            GridLayoutGroup gLg = grid.GetComponentInChildren<GridLayoutGroup>();
            gLg.cellSize = new Vector2(50, 50);


            SetCanvasPercentSize();


            grid.gameObject.SetActive(true);
            grid.RestartGridHud();

        }

        void CriarMenuPorScript()
        {

            MenuBehaviour menu = Instantiate(menuRef);
            menu.transform.SetParent(canvasAlvo.transform);
            rt = menu.GetComponent<RectTransform>();
            RectTransform rtt = canvasAlvo.GetComponent<RectTransform>();


            SetCanvasPercentSize();


            UiSupportSingleton.Instance.InvokeInRealTime(() =>
            {
                A_MenuOption[] As = rt.GetComponentsInChildren<A_MenuOption>();
                RectTransform pai = As[0].transform.parent.GetComponent<RectTransform>();
                Debug.Log(pai.sizeDelta + " : " + pai.childCount + " : " + pai.rect.height);


                foreach (var A in As)
                {
                    RectTransform rrtt = A.GetComponent<RectTransform>();
                    A.GetComponentInChildren<Text>().resizeTextForBestFit=true;

                    rrtt.sizeDelta = new Vector2(rrtt.sizeDelta.x, pai.rect.height / (pai.childCount - 1));
                }
            },0.5f);


        }

        void CriarUiImage()
        {
            GameObject G = new GameObject();
            G.transform.SetParent(canvasAlvo.transform);
            RectTransform rtt = canvasAlvo.GetComponent<RectTransform>();

            Debug.Log(rtt.rect.height);
            Image img = G.AddComponent<Image>();
            rt = img.rectTransform;

            SetCanvasPercentSize();        

        }

        void CriarCanvas()
        {
            GameObject G = new GameObject();

            G.name = "Script Canvas";
            canvasAlvo = G.AddComponent<Canvas>();
            canvasAlvo.renderMode = RenderMode.ScreenSpaceOverlay;

            CanvasScaler canS = G.AddComponent<CanvasScaler>();
            canS.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canS.referenceResolution = new Vector2(800, 600);
            canS.matchWidthOrHeight = .5f;

            G.AddComponent<GraphicRaycaster>();
        }

        void Refazer()
        {
            Destroy(canvasAlvo.gameObject);

            Start();
        }
    }

         */
}