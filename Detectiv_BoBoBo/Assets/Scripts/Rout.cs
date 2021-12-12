using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rout : MonoBehaviour
{
    [SerializeField]
    private List<Transform> points;
    Tree tree;

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

            TreeNode fL = this.add(new Point(points[1].position, Status.ROOM), _head, false);

            TreeNode fR = this.add(new Point(points[2].position, Status.ROOM), _head, true);
            //TreeNode fLsR = this.add(new Point(points[2].position, Status.NOTHING), fL, true);
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
                        if (way.left != null)
                        {
                            way = way.left;
                            return way.value.coord;
                        }   
                    }
                    else
                    {
                        if (way.right != null)
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

    public float speed = 0.0004f;

    // Start is called before the first frame update
    void Start()
    {
        tree = new Tree(points);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        //Vector3 vec = tree.Move() - transform.position;
        //double speed = System.Math.Sqrt(vec.x*vec.x+vec.y*vec.y);
        //transform.position = Vector3.Lerp(transform.position, tree.Move(),10.0f);
        transform.position = Vector3.MoveTowards(transform.position, tree.Move(), speed);
    }
}