using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    [SerializeField]
    private List<Transform> points = new List<Transform>();
    Tree tree;

    [SerializeField]
    private List<Transform> spawn = new List<Transform>();

    public enum Status
    {
        ROOM,
        NOTHING,
        INTERSPACE
    }

    public class Point
    {
        public Vector3 coord;
        public Status stat;

        public Point(Vector3 vec, Status st)
        {
            this.coord = vec;
            this.stat = st;
        }
    }

    public class TreeNode
    {
        public Point value;
        public TreeNode parent;
        public TreeNode left;
        public TreeNode right;

        public TreeNode(Point val, TreeNode p = null, TreeNode l = null, TreeNode r = null)
        {
            this.value = val;
            this.left = l;
            this.right = r;
            this.parent = p;
        }
        public TreeNode() { }
    }

    public class Tree
    {
        private List<Transform> points;

        private TreeNode _head;
        private TreeNode way = new TreeNode();

        public Tree(Point val)
        {
            _head = new TreeNode(val);
            way = _head;
            this.create_map();
        }

        public Tree() { }

        public Tree(List<Transform> trans)
        {
            points = trans;
            _head = new TreeNode(new Point(points[0].position, Status.ROOM));
            way = _head;
            this.create_map();
        }

        //side = 0 - left
        //side = 1 -right
        public TreeNode add(Point val, TreeNode head, bool side)
        {
            if (!side)
            {
                head.left = new TreeNode(val);
                head.left.parent = head;
                return head.left;
            }
            else
            {
                head.right = new TreeNode(val);
                head.right.parent = head;
                return head.right;
            }
        }

        public void create_map()
        {
            TreeNode fL = this.add(new Point(points[2].position, Status.INTERSPACE), _head, false); //правильно
            TreeNode fR = this.add(new Point(points[1].position, Status.INTERSPACE), _head, true);  //правильно

            TreeNode fLsL = this.add(new Point(points[12].position, Status.INTERSPACE), fL, false); //правильно
            TreeNode fLsR = this.add(new Point(points[4].position, Status.ROOM), fL, true);         //правильно

            TreeNode fLsRthL = this.add(new Point(points[3].position, Status.ROOM), fLsR, false);   //правильно
            TreeNode fLsRthR = this.add(null, fLsR, true);                                          //правильно

            TreeNode fLsLthR = this.add(new Point(points[13].position, Status.ROOM), fLsL, false);  //
            TreeNode fLsLthRL = this.add(new Point(points[14].position, Status.ROOM), fLsL, true);  //

            TreeNode fRsL = this.add(null, fR, false);  //
            TreeNode fRsR = this.add(new Point(points[5].position, Status.ROOM), fR, true); //

            TreeNode fRsRthR = this.add(new Point(points[6].position, Status.INTERSPACE), fRsR, true);
            TreeNode fRsRthL = this.add(null, fRsR, false);

            TreeNode fRsRthRfR = this.add(new Point(points[7].position, Status.INTERSPACE), fRsRthR, true);
            TreeNode fRsRthRfL = this.add(null, fRsRthR, false);

            TreeNode fRsRthRfRfR = this.add(new Point(points[8].position, Status.INTERSPACE), fRsRthRfR, true);
            TreeNode fRsRthRfRfL = this.add(null, fRsRthRfR, false);
            TreeNode fRsRthRfRfRsR = this.add(new Point(points[9].position, Status.INTERSPACE), fRsRthRfRfR, true);
            TreeNode fRsRthRfRfRsL = this.add(null, fRsRthRfRfR, false);

            TreeNode fRsRthRfRfRsRsL = this.add(new Point(points[10].position, Status.ROOM), fRsRthRfRfRsR, false);
            TreeNode fRsRthRfRfRsRsR = this.add(new Point(points[11].position, Status.ROOM), fRsRthRfRfRsR, true);

            TreeNode fLsLthRfR = this.add(new Point(points[15].position, Status.ROOM), fLsLthR, false);
            TreeNode fLsLthsL = this.add(null, fLsLthR, true);

        }

        public TreeNode get()
        {
            return _head;
        }

        public void permutation(ref TreeNode _newHead)
        {
            if ((_newHead.left != null) && (_newHead.left == null))
            {
                _newHead.left = _newHead.parent;
                helper(ref _newHead.parent);
                _newHead.parent = null;
            }
            else if ((_newHead.right != null) && (_newHead.right == null))
            {
                _newHead.right = _newHead.parent;
                helper(ref _newHead.parent);
                _newHead.parent = null;
            }

            if (_newHead.left == null && _newHead.right == null)
            {
                _newHead.left = _newHead.parent;
                helper(ref _newHead.parent);
                _newHead.parent = null;
                _newHead.right = null;
            }
        }

        public void helper(ref TreeNode list)
        {
            TreeNode buff = new TreeNode();
            if (list.parent != null)
            {
                if (list.left != null)
                {
                    if (list == list.left.left || list == list.left.right)
                    {
                        buff.parent = list.parent;
                        list.parent = list.left;
                        list.left = buff.parent;
                        helper(ref list.left);
                    }
                }
                if (list.right != null)
                {
                    if (list == list.right.left || list == list.right.right)
                    {
                        buff.parent = list.parent;
                        list.parent = list.right;
                        list.right = buff.parent;
                        helper(ref list.right);
                    }
                }
            }
            else
            {
                if (list == list.left.right || list == list.left.left)
                {
                    list.parent = list.left;
                    list.left = null;
                    return;
                }
                if (list == list.right.right || list == list.right.left)
                {
                    list.parent = list.right;
                    list.right = null;
                    return;
                }
            }
        }

        public Vector3 Move()
        {
            while (true)
            {
                if (way.left == null && way.right == null)
                {
                    this.permutation(ref way);
                }

                if (RandomNumbers.NextNumber() % 2 == 0)
                {
                    if (way.left.value != null)
                    {
                        way = way.left;
                        return way.value.coord;
                    }
                }
                else
                {
                    if (way.right.value != null)
                    {
                        way = way.right;
                        return way.value.coord;
                    }
                }


            }
        }

        internal static class RandomNumbers
        {
            private static System.Random r;

            public static int NextNumber()
            {
                if (r == null)
                    Seed();

                return r.Next();
            }

            public static int NextNumber(int ceiling)
            {
                if (r == null)
                    Seed();

                return r.Next(ceiling);
            }

            public static void Seed()
            {
                r = new System.Random();
            }

            public static void Seed(int seed)
            {
                r = new System.Random(seed);
            }
        }

    }

    public float speed = 4.0f;
    private float startTime;
    private float distance;
    private Transform startMarker;
    private Transform endMarker;

    // Start is called before the first frame update
    void Start()
    {
        var test = GameObject.Find("SpawnPoints/SpawnPointOne");
        //spawn[0] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointTwo");
        //spawn[1] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointThree");
        //spawn[2] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointFour");
        //spawn[3] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointFive");
        //spawn[4] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointSix");
        //spawn[5] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointSeven");
        //spawn[6] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointEight");
        //spawn[7] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointNine");
        //spawn[8] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointTen");
        //spawn[9] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointEleven");
        //spawn[10] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointTwelwe");
        //spawn[11] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointThirteen");
        //spawn[12] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointFourteen");
        //spawn[13] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointFivteen");
        //spawn[14] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointSixteen");
        //spawn[15] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointSeventeen");
        //spawn[16] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointEighteen");
        //spawn[17] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointNinteen");
        //spawn[18] = test.transform;
        //test = GameObject.Find("SpawnPoints/SpawnPointTwenty");
        //spawn[19] = test.transform;

        test = GameObject.Find("RootPoint/Point0");
        points[0] = test.transform;
        test = GameObject.Find("RootPoint/Point1");
        points[1] = test.transform;
        test = GameObject.Find("RootPoint/Point2");
        points[2] = test.transform;
        test = GameObject.Find("RootPoint/Point3");
        points[3] = test.transform;
        test = GameObject.Find("RootPoint/Point4");
        points[4] = test.transform;
        test = GameObject.Find("RootPoint/Point5");
        points[5] = test.transform;
        test = GameObject.Find("RootPoint/Point6");
        points[6] = test.transform;
        test = GameObject.Find("RootPoint/Point7");
        points[7] = test.transform;
        test = GameObject.Find("RootPoint/Point8");
        points[8] = test.transform;
        test = GameObject.Find("RootPoint/Point9");
        points[9] = test.transform;
        test = GameObject.Find("RootPoint/Point10");
        points[10] = test.transform;
        test = GameObject.Find("RootPoint/Point11");
        points[11] = test.transform;
        test = GameObject.Find("RootPoint/Point12");
        points[12] = test.transform;
        test = GameObject.Find("RootPoint/Point13");
        points[13] = test.transform;
        test = GameObject.Find("RootPoint/Point14");
        points[14] = test.transform;

        tree = new Tree(points);
        startTime = Time.time;
    }

    void journeyoPoint()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractOfDist = distCovered / distance;
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractOfDist);

        if (Vector3.Distance(transform.position, endMarker.position) < 0.01f)
        {
            startMarker.position = endMarker.position;
            endMarker.position = tree.Move();
            distance = Vector3.Distance(startMarker.position, endMarker.position);
            startTime = Time.time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        journeyoPoint();
    }
}