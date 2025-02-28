using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class Blueprint : MonoBehaviour
{
    [Header("Layouts for blueprints")]
    [SerializeField]
    private GameObject[] blueprint_layouts;

    private Print[] blueprints =
    {
        //MUST SET THE BLUEPRINT'S LAYOUT IN "SetPrintVisuals" METHOD TO HAVE THE VISUAL INSTRUCTIONS ON SCREEN!
        new Print( //Example
            new Vector3[] { new Vector3(0, -0.5f, 0), Vector3.right},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            ),

        new Print( //Honourable Knight - Wall
            new Vector3[] { new Vector3(0, -0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(-1, 1.5f, 0), new Vector3(-2, 2.5f, 0)},
            12f
            ),

        new Print( //City Guard - Wagon
            new Vector3[] { new Vector3(0, -0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0.5f, -0.5f, 0), new Vector3(-0.5f, 0.5f, 0), new Vector3(0.5f, 1.5f, 0), new Vector3(-0.5f, 2.5f, 0)},
            12f
            ),

        new Print( //Adventurer - Fire
            new Vector3[] { new Vector3(0, -0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(1f, -0.5f, 0), new Vector3(2f, 0, 0), new Vector3(3f, -0.5f, 0), new Vector3(4f, -1.35f, 0)},
            12f
            ),

        new Print( //Farmer - Sickle
            new Vector3[] { new Vector3(0, -0.5f, 0), new Vector3(-1.15f, 0.5f, 0), new Vector3(-1f, 1.5f, 0), new Vector3(1f, 1.6f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            ),

        new Print( //Adventurer - Bow
            new Vector3[] { new Vector3(0.6f, 0.8f, 0), new Vector3(4.3f, 1f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            ),

        new Print( //Royal - Picture Frame
            new Vector3[] { new Vector3(0f, 1.5f, 0), new Vector3(3f, 1.5f, 0), new Vector3(3f, -1.35f, 0), new Vector3(1.3f, -1.35f, 0)},
            new Vector3[] {new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            ),

        new Print( //Royal - Sine
            new Vector3[] { new Vector3(-0.8f, -0.5f, 0), new Vector3(0.8f, 0.5f, 0), new Vector3(-0.8f, 1.5f, 0), new Vector3(0.8f, 2.5f, 0)},
            new Vector3[] { new Vector3(0, -1.35f, 0), new Vector3(0, -0.5f, 0), new Vector3(0, 0.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(0, 2.5f, 0)},
            12f
            ),
    };

    [SerializeField]
    private List<DialogueStorage> dialogueStorage;

    [Header("Other needed objects")]
    [SerializeField]
    private AccuracyCalculator accuracyCalculator;

    [SerializeField]
    private GameObject goal_prefab;
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private DialogueSystem dialogueBox;

    private GameManager game_manager;

    private Spline sword_spline;

    private GameObject current_sword;
    private Transform[] goal_objects;
    private GameObject current_layout;

    private void Start()
    {
        game_manager = FindAnyObjectByType<GameManager>();
        SetPrintVisuals();
    }

    private void SetPrintVisuals()
    {
        blueprints[0].SetPrintGuide(blueprint_layouts[0]); //straight sword
        blueprints[1].SetPrintGuide(blueprint_layouts[5]);
        blueprints[2].SetPrintGuide(blueprint_layouts[5]);
        blueprints[3].SetPrintGuide(blueprint_layouts[5]);
        blueprints[4].SetPrintGuide(blueprint_layouts[1]); //sickle shape
        blueprints[5].SetPrintGuide(blueprint_layouts[2]); //bow shape
        blueprints[6].SetPrintGuide(blueprint_layouts[3]); // frame
        blueprints[7].SetPrintGuide(blueprint_layouts[4]); // sine
    }

    public void PlacePrint(int index)
    {
        StartCoroutine(WaitForDialogueBeforePlacePrint(index));
    }

    public IEnumerator WaitForDialogueBeforePlacePrint(int index)
    {
        dialogueBox.StartDialogue(dialogueStorage[index - 1]);

        while (dialogueBox.coroutineRunning)
        {
            yield return new WaitForEndOfFrame();
        }

        Vector3[] coords = blueprints[index].GetCoords();

        goal_objects = new Transform[coords.Length];

        for (int i = 0; i < coords.Length; i++)
        {
            goal_objects[i] = Instantiate(goal_prefab, coords[i], new Quaternion()).transform;
        }
        accuracyCalculator.SetGoalPoints(goal_objects);

        current_sword = Instantiate(sword, Vector3.zero, new Quaternion());
        sword_spline = current_sword.gameObject.GetComponent<SpriteShapeController>().spline;

        for (int i = 0; i < sword_spline.GetPointCount(); i++)
        {
            sword_spline.SetPosition(i, blueprints[index].GetSplinePos()[i]);
        }

        current_layout = Instantiate(blueprints[index].GetPrintGuide());

        game_manager.SetTimer(blueprints[index].GetPar());
    }

    public Print GetBlueprint(int index)
    {
        return blueprints[index];
    }

    public void CleanPrint()
    {
        foreach(Transform goal in goal_objects)
        {
            Destroy(goal.gameObject);
        }
        Destroy(current_sword);
        Destroy(current_layout);
    }

    public int PrintCount()
    {
        return blueprints.Length;
    }
}

public class Print
{
    private Vector3[] goal_coordinates;
    private Vector3[] spline_start_positions;
    private float par_time;
    private GameObject print_guide;
    public Print(Vector3[] g_coords, Vector3[] start_pos, float par)
    {
        this.goal_coordinates = g_coords;
        this.spline_start_positions = start_pos;
        this.par_time = par;
        this.print_guide = null;
    }

    public Vector3[] GetCoords()
    {
        return this.goal_coordinates;
    }

    public Vector3[] GetSplinePos()
    {
        return this.spline_start_positions;
    }

    public float GetPar()
    {
        return this.par_time; 
    }

    public void SetPrintGuide(GameObject layout)
    {
        this.print_guide = layout;
    }
    public GameObject GetPrintGuide()
    {
        return this.print_guide;
    }
}
