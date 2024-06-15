using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputManager;
using ContextMenu;

public class Main : MonoBehaviour
{
    private OculusTouch oculusTouch;
    private ContextMenu.ContextMenu contextMenu;
    private int state = -1;
    private Points points;

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
        
        Point point0 = new Point(this.oculusTouch, new Vector3(0, 0, 1));
        Point point1 = new Point(this.oculusTouch, new Vector3(0.5f, 0, 0.7f));
        Point point2 = new Point(this.oculusTouch, new Vector3(-0.5f, 0, 0.7f));
        Point point3 = new Point(this.oculusTouch, new Vector3(0, 0.4f, 0.7f));

        this.points = new Points(this.oculusTouch, point0, point1, point2, point3);

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
                this.points.Move(i);
            }
        }

        this.points.Update();

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
