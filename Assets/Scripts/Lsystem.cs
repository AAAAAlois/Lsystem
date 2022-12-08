using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lsystem : MonoBehaviour
{
    public UiController UI;

    private string axiom = "F";
    private string currentString;
    private Dictionary<char, string> rules = new Dictionary<char, string>();
    private Stack<TransformInfo> transformStack = new Stack<TransformInfo>();
    private Vector3 initialPosition = Vector3.zero;

    public float length;
    public float angle;
    public float width;
    public int iteration;
    public int index;   //ruleIndex
    public int colorIndex;
    public bool hasLeaf;
    public bool hasFlower;
    public bool hasStochastic;


    public GameObject tree = null;

    public Material branch1;
    public Material branch2;
    public Material branch3;
    public Material branchColor;


    public GameObject treeParent;
    public GameObject branch;
    public GameObject leaf;
    public GameObject flower;


    private int currentIteration;
    private int currentIndex;
    private float currentLength;
    private float currentAngle;
    private float currentWidth;
    private bool currentLeaf;
    private bool currentFlower;
    private bool currentSto;
    private int currentColorIndex;

    // Start is called before the first frame update
    void Start()
    {
        length = 1f;
        angle = 22.5f;
        width = 0.1f;
        iteration = 1;
        index = 0;
        colorIndex = 0;

        hasLeaf = false;
        hasFlower = false;
        hasStochastic = false;

        branchColor = branch1;
        branch.GetComponent<LineRenderer>().material = branchColor;

        ChooseTree(index);
        currentString = axiom;

        currentIndex = index;
        currentIteration = iteration;
        currentLength = length;
        currentAngle = angle;
        currentWidth = width;
        currentLeaf = hasLeaf;
        currentFlower = hasFlower;
        currentSto = hasStochastic;
        currentColorIndex = colorIndex;
        

        _isGenerate();


    }

    // Update is called once per frame
    void Update()
    {
        ChooseTree(index);
        ChooseColor(colorIndex);

        if (currentIndex != index || currentIteration != iteration || currentWidth != width || currentLength != length || currentAngle != angle ||
            currentLeaf != hasLeaf || currentFlower != hasFlower || currentSto!= hasStochastic || currentColorIndex!=colorIndex)
        {
            Reset();
            _isGenerate();
            //Debug.Log("regenerate tree");
        }
    }

    void Generate()
    {
        

        string newString = "";

        char[] stringCharacters = currentString.ToCharArray();


        for (int i = 0; i < stringCharacters.Length; i++)
        {
            char currentCharacter = stringCharacters[i];

            if (rules.ContainsKey(currentCharacter))
            {
                if (hasStochastic)
                {
                    int r = Random.Range(0, 2);   //stochastic L-system
                    if (currentCharacter == 'F')
                    {
                        if (r == 0)
                        {
                            currentCharacter = 'F';
                        }
                        else
                        {
                            currentCharacter = 'f';
                        }
                    }

                    if (currentCharacter == 'X')
                    {
                        if (r == 0)
                        {
                            currentCharacter = 'x';
                        }
                        else
                        {
                            currentCharacter = 'X';
                        }
                    }
                }
               

                newString += rules[currentCharacter];
            }
            else
            {
                newString += currentCharacter.ToString();
            }
        }


        currentString = newString;
        Debug.Log(currentString);
    }

    void Draw()
    {
        Destroy(tree);
        tree = Instantiate(treeParent);
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;

        char[] stringCharacters = currentString.ToCharArray();

        for (int i = 0; i < stringCharacters.Length; i++)
        {
            char currentCharacter = stringCharacters[i];

            switch (currentCharacter)
            {
                case 'F':
                    initialPosition = transform.position;
                    transform.Translate(Vector3.up * length);

                    GameObject currentLine = Instantiate(branch);
                    currentLine.transform.SetParent(tree.transform);
                    currentLine.GetComponent<LineRenderer>().SetPosition(0, initialPosition);
                    currentLine.GetComponent<LineRenderer>().SetPosition(1, transform.position);
                    currentLine.GetComponent<LineRenderer>().startWidth = width;
                    currentLine.GetComponent<LineRenderer>().endWidth = width;

                    //Debug.Log("F");
                    break;

                case 'X':
                    //Debug.Log("X");
                    break;


                case '[':
                    transformStack.Push(new TransformInfo()
                    {
                        position = transform.position,
                        rotation = transform.rotation
                    });
                    break;

                case ']':
                    if (hasLeaf)
                    {
                        GameObject currentLeaf1 = Instantiate(leaf);
                        currentLeaf1.transform.SetParent(tree.transform);
                        currentLeaf1.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                        currentLeaf1.GetComponent<LineRenderer>().SetPosition(1, transform.position + Vector3.up * 1f);
                    }

                    if (hasFlower)
                    {
                        int flowerNumber = Random.Range(0, 6);
                        if (flowerNumber >= 5)
                        {
                            GameObject currentflower = Instantiate(flower);
                            currentflower.transform.SetParent(tree.transform);
                            currentflower.GetComponent<LineRenderer>().SetPosition(0, transform.position);
                            currentflower.GetComponent<LineRenderer>().SetPosition(1, transform.position + Vector3.up * 1f);
                        }

                    }

                    TransformInfo ti = transformStack.Pop();
                    transform.position = ti.position;
                    transform.rotation = ti.rotation;
                    break;

                case '+':
                    transform.Rotate(Vector3.forward * angle);
                    break;

                case '-':
                    transform.Rotate(Vector3.forward * -angle);
                    break;

                case '*':
                    transform.Rotate(Vector3.up * 120);
                    break;

                case '/':
                    transform.Rotate(Vector3.up * -120);
                    break;

                case '<':
                    transform.Rotate(Vector3.left * angle);
                    break;

                case '>':
                    transform.Rotate(Vector3.right * angle);
                    break;

            }
        }
    }

    public void Tree1()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "F[+F]F[-F]F" },
            {'f', "F[-F]F[+F]F" }
        };
        axiom = "F";
    }

    public void Tree2()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "F[+F]F[-F][F]" },
            {'f', "F[-F]F[+F][F]" }
        };
        axiom = "F";
    }

    public void Tree3()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "FF-[-F+F+F]+[+F-F-F]" },
            {'f', "FF+[+F-F-F]-[-F+F+F]" }
        };
        axiom = "F";
    }

    public void Tree4()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "FF" },
            {'f', "FF" },
            {'X', "F[+X]F[-X]+X" },
            {'x', "F[-X]F[+X]-X" }
        };
        axiom = "X";
    }

    public void Tree5()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "FF" },
            {'f', "FF" },
            {'X',"F[+X][-X]FX" },
            {'x',"F[-X][+X]FX" }
        };
        axiom = "X";
    }

    public void Tree6()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "FF" },
            {'f', "FF" },
            {'X',"F-[[X]+X]+F[+FX]-X" },
            {'x',"F+[[X]-X]-F[-FX]+X" }
        };
        axiom = "X";
    }

    public void Tree7()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "FF" },
            {'f', "FF" },
            {'X',"[*+FX]X[+FX][/+F-FX]" },
            {'x',"[/-FX]X[-FX][*-F+FX]" }
        };
        axiom = "X";
    }

    public void Tree8()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "FF" },
            {'f', "FF" },
            {'X',"[F[-X+F[+FX]][*-X+F[+FX]][/-X+F[+FX]-X]]" },
            {'x',"[F[+X-F[-FX]][/+X-F[-FX]][*+X-F[-FX]+X]]" }
        };
        axiom = "X";
    }

    public void Tree9()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "F[*+F]F[->F][->F][*F]" },
            {'f', "F[/-F]F[+<F][+<F][/F]" }


        };
        axiom = "F";
    }

    public void Tree10()
    {
        rules = new Dictionary<char, string>
        {
            {'F', "F-F+F+FF-F-F+F" },
            {'f', "F-F+F+FF-F-F+F" }


        };
        axiom = "F-F-F-F";
        angle = 90f;
    }


    void ChooseTree(int index)
    {
        switch (index)
        {
            case 0:
                Tree1();
                //Debug.Log("tree1");
                break;

            case 1:
                Tree2();
                //Debug.Log("tree");
                break;

            case 2:
                Tree3();
                break;

            case 3:
                Tree4();
                break;

            case 4:
                Tree5();
                break;

            case 5:
                Tree6();
                break;

            case 6:
                Tree7();
                break;

            case 7:
                Tree8();
                break;

            case 8:
                Tree9();
                break;

            case 9:
                Tree10();
                break;

            default:
                Tree1();
                break;

        }
    }

    void ChooseColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 0:
                branch.GetComponent<LineRenderer>().material = branch1;
                break;

            case 1:
                branch.GetComponent<LineRenderer>().material = branch2;
                break;

            case 2:
                branch.GetComponent<LineRenderer>().material = branch3;
                break;

            default:
                branch.GetComponent<LineRenderer>().material = branch1;
                break;
        }
    }

    void _isGenerate()
    {
        currentString = axiom;
        Debug.Log(axiom);
        for(int i = 0; i < iteration; i++)
        {
            Generate();
        }
        Draw();

    }

    private void Reset()
    {
        currentString = axiom;
        currentIndex = index;
        currentIteration = iteration;
        currentLength = length;
        currentAngle = angle;
        currentWidth = width;
        currentLeaf = hasLeaf;
        currentFlower = hasFlower;
        currentSto = hasStochastic;
        currentColorIndex = colorIndex;
        Debug.Log("reset");
    }

}
