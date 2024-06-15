using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager;
using ContextMenu;

public class Main : MonoBehaviour
{
    private OculusTouch oculusTouch;
    private ContextMenu.ContextMenu contextMenu;
    private List<Point> points = new List<Point>();
    private int state = -1;

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
        this.contextMenu.AddItem(new MenuItem("左人差し指 : メニューウィンドウの開閉", () => {}));
        this.contextMenu.AddItem(new MenuItem("左スティック : カーソルの移動", () => {}));
        this.contextMenu.AddItem(new MenuItem("Xボタン : 決定", () => {}));
        this.contextMenu.AddItem(new MenuItem("", () => {}));
        this.contextMenu.Open();

        this.points.Add(new Point(oculusTouch, new Vector3(0, 0, 1)));
        this.points.Add(new Point(oculusTouch, new Vector3(0.5f, 0, 0.7f)));
        this.points.Add(new Point(oculusTouch, new Vector3(-0.5f, 0, 0.7f)));
        this.points.Add(new Point(oculusTouch, new Vector3(0, 0.4f, 0.7f)));

        Vector3 p1 = points[0].GetPosition();
        Vector3 p2 = points[1].GetPosition();
        Vector3 p3 = points[2].GetPosition();
        Vector3 p4 = points[3].GetPosition();
        Vector3 p5 = Points.VectorCircumcenter3D(p1, p2, p3, p4);
        this.points.Add(new Point(oculusTouch, p5));

        // for (int i = 0; i < 4; i++)
        // {
        //     this.contextMenu.AddItem(new MenuItem("P" + i, () => {
        //         this.state = i;
        //     }));
        // }
        this.contextMenu.AddItem(new MenuItem("P" + 0, () => {
            this.state = 0;
        }));
        this.contextMenu.AddItem(new MenuItem("P" + 1, () => {
            this.state = 1;
        }));
        this.contextMenu.AddItem(new MenuItem("P" + 2, () => {
            this.state = 2;
        }));
        this.contextMenu.AddItem(new MenuItem("P" + 3, () => {
            this.state = 3;
        }));
    }

    // Update is called once per frame
    void Update()
    {
        this.oculusTouch.UpdateFirst();
        this.contextMenu.Update();

        for (int i = 0; i < 4; i++)
        {
            if (this.state == i)
            {
                this.points[i].Move();
            }
        }

        Vector3 p1 = points[0].GetPosition();
        Vector3 p2 = points[1].GetPosition();
        Vector3 p3 = points[2].GetPosition();
        Vector3 p4 = points[3].GetPosition();
        Vector3 p5 = Points.VectorCircumcenter3D(p1, p2, p3, p4);
        this.points[4].SetPosition(p5);

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
