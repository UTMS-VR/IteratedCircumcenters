using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private TextMeshPro textMeshPro;

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

        GameObject textObj = new GameObject("TextMeshPro 3D Text");
        textObj.transform.position = new Vector3(0, 0, 1);
        textMeshPro = textObj.AddComponent<TextMeshPro>();
        textMeshPro.text = "Initial 3D Text";
        textMeshPro.fontSize = 0.5f;
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.color = Color.white;
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
