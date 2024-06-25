using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager;
using ContextMenu;
using DrawCurve;

public class Main : MonoBehaviour
{
    private OculusTouch oculusTouch;
    private ContextMenu.ContextMenu contextMenu;
    private Points points;
    private Tetrahedra tetrahedra;
    private int numberOfPoints = 11;
    private MenuItem reductionRatio = new MenuItem("", () => {});

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

        this.contextMenu = new ContextMenu.ContextMenu(
            this.oculusTouch,
            upButton: LogicalOVRInput.RawButton.LStickUp,
            downButton: LogicalOVRInput.RawButton.LStickDown,
            confirmButton: LogicalOVRInput.RawButton.X,
            toggleMenuButton: LogicalOVRInput.RawButton.LIndexTrigger,
            lockLevel: null
        );
        this.contextMenu.AddItem(new MenuItem("left index finger : open and close the menu window", () => {}));
        // this.contextMenu.AddItem(new MenuItem("left stick : move the cursor", () => {}));
        this.contextMenu.AddItem(new MenuItem("X : select", () => {}));
        this.contextMenu.AddItem(new MenuItem("", () => {}));
        this.contextMenu.AddItem(this.reductionRatio);
        this.contextMenu.AddItem(new MenuItem("cos^5 pi/5 = " + Math.Pow(Math.Cos(Math.PI / 5), 5), () => {}));
        this.contextMenu.AddItem(new MenuItem("restriction on", () => {
            this.points.RestrictionStateOn();
        }));
        this.contextMenu.AddItem(new MenuItem("restriction off", () => {
            this.points.RestrictionStateOff();
        }));
        this.contextMenu.Open();
        
        Point point0 = new Point(this.oculusTouch, new Vector3(0.4f, 0, 0.3f), 0);
        Point point1 = new Point(this.oculusTouch, new Vector3(-0.4f, 0, 0.3f), 1);
        Point point2 = new Point(this.oculusTouch, new Vector3(0, 0, 0.7f), 2);
        Point point3 = new Point(this.oculusTouch, new Vector3(0, 0.3f, 0.3f), 3);

        this.points = new Points(this.oculusTouch, point0, point1, point2, point3, this.numberOfPoints);

        this.tetrahedra = new Tetrahedra(this.points);
    }

    // Update is called once per frame
    void Update()
    {
        this.oculusTouch.UpdateFirst();
        // if (this.contextMenu.cursorIndex == 0)
        // {
        //     this.contextMenu.cursorIndex = 3;
        // } else if (this.contextMenu.cursorIndex < 3)
        // {
        //     this.contextMenu.cursorIndex = 4;
        // }
        this.contextMenu.Update();

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

        float ratio5601 = this.points.ReductionRatio();
        this.contextMenu.ChangeItemMessage(this.reductionRatio, "四面体0123 : 四面体5678 = 1 : " + ratio5601);

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
