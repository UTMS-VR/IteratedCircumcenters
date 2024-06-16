using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InputManager;
using ContextMenu;
using DrawCurve;
using TMPro;

public class Main : MonoBehaviour
{
    private OculusTouch oculusTouch;
    // private ContextMenu.ContextMenu contextMenu;
    private Points points;
    private Tetrahedra tetrahedra;
    private int numberOfPoints = 11;
    [SerializeField] TextMeshProUGUI text0;
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] TextMeshProUGUI text5;
    [SerializeField] TextMeshProUGUI text6;
    [SerializeField] TextMeshProUGUI text7;
    [SerializeField] TextMeshProUGUI text8;
    [SerializeField] TextMeshProUGUI text9;
    [SerializeField] TextMeshProUGUI text10;
    private Canvas canvas;
    private TextMeshProUGUI tmpText;

    // Start is called before the first frame update
    void Start()
    {
        this.oculusTouch = new OculusTouch
        (
            buttonMap: LiteralKeysPlus,
            leftStickKey: PredefinedMaps.Arrows,
            rightHandKey: PredefinedMaps.OKLSemiIComma,
            handScale: 0.03f,
            handSpeed: 0.01f
        );

        // this.contextMenu = new ContextMenu.ContextMenu(
        //     this.oculusTouch,
        //     upButton: LogicalOVRInput.RawButton.LStickUp,
        //     downButton: LogicalOVRInput.RawButton.LStickDown,
        //     confirmButton: LogicalOVRInput.RawButton.X,
        //     toggleMenuButton: LogicalOVRInput.RawButton.LIndexTrigger,
        //     lockLevel: null
        // );
        // this.contextMenu.AddItem(new MenuItem("left index finger : open and close the menu window", () => {}));
        // this.contextMenu.AddItem(new MenuItem("left stick : move the cursor", () => {}));
        // this.contextMenu.AddItem(new MenuItem("X : select", () => {}));
        // this.contextMenu.AddItem(new MenuItem("", () => {}));
        // this.contextMenu.Open();
        
        Point point0 = new Point(this.oculusTouch, new Vector3(0, 0, 1));
        Point point1 = new Point(this.oculusTouch, new Vector3(0.5f, 0, 0.7f));
        Point point2 = new Point(this.oculusTouch, new Vector3(-0.5f, 0, 0.7f));
        Point point3 = new Point(this.oculusTouch, new Vector3(0, 0.4f, 0.7f));

        this.points = new Points(this.oculusTouch, point0, point1, point2, point3, this.numberOfPoints);

        this.tetrahedra = new Tetrahedra(this.points);

        // Canvasの生成
        canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.gameObject.AddComponent<CanvasScaler>();
        canvas.gameObject.AddComponent<GraphicRaycaster>();

        // Textの生成
        GameObject textObj = new GameObject("TextMeshPro Text");
        textObj.transform.SetParent(canvas.transform);
        tmpText = textObj.AddComponent<TextMeshProUGUI>();
        tmpText.text = "Initial Text";
        tmpText.fontSize = 24;
        tmpText.alignment = TextAlignmentOptions.Center;
        RectTransform textRect = tmpText.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(400, 100);
        textRect.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        this.oculusTouch.UpdateFirst();
        // this.contextMenu.Update();

        if (oculusTouch.GetButtonDown(Point.moveButton))
        {
            this.points.ChangeMovingPoint();
        }

        for (int i = 0; i < 4; i++)
        {
            if (this.points.GetMovingPoint() == i)
            {
                this.points.Move(this.points.GetMovingPoint());
            }
        }

        this.points.Update();

        this.tetrahedra.Update(points);
        for (int i = 0; i < numberOfPoints - 3; i++)
        {
            this.tetrahedra.Get(i).DrawMesh();
        }

        this.text0.text = "0";
        this.text0.transform.position = this.points.Get(0).GetPosition();
        this.text1.text = "1";
        this.text2.text = "2";
        this.text3.text = "3";
        this.text4.text = "4";
        this.text5.text = "5";
        this.text6.text = "6";
        this.text7.text = "7";
        this.text8.text = "8";
        this.text9.text = "9";
        this.text10.text = "10";

        this.oculusTouch.UpdateFirst();
    }

    private static ButtonMap LiteralKeysPlus
    = new ButtonMap(new List<(LogicalButton logicalButton, IPhysicalButton physicalButton)>
    {
        ( LogicalOVRInput.RawButton.A, new PhysicalKey(KeyCode.A) ),
        ( LogicalOVRInput.RawButton.B, new PhysicalKey(KeyCode.B) ),
        ( LogicalOVRInput.RawButton.X, new PhysicalKey(KeyCode.X) ),
        ( LogicalOVRInput.RawButton.Y, new PhysicalKey(KeyCode.Y) ),
        ( LogicalOVRInput.RawButton.RIndexTrigger, new PhysicalKey(KeyCode.R) ),
        ( LogicalOVRInput.RawButton.RHandTrigger, new PhysicalKey(KeyCode.E) ),
        ( LogicalOVRInput.RawButton.LIndexTrigger, new PhysicalKey(KeyCode.Q) ),
        ( LogicalOVRInput.RawButton.LHandTrigger, new PhysicalKey(KeyCode.W) )
    });
}
