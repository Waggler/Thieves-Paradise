using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BehaviorTree
{
    public enum NodeState
    {
        PASSIVE,
        WARY,
        SUSPICIOUS,
        HOSTILE,
        REPORT,
        ATTACK,
        RANGEDATTACK,
        STUNNED,
        FAILURE
    }

    public class Node
    {
        #region Node

        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            //Does it for each child in the list
            foreach (Node child in children)
                _Attach(child);
        }

        //---------------------------------//

        private void _Attach(Node node)
        {
            //References this Node class
            node.parent = this;

            //Adds the inserted node to the children list
            children.Add(node);
        }//End Attatch


        //LAMBDA EXPRESSED PROTOTYPE FOR THE EVALUATE METHOD, DO NOT LEAVE IN FINAL PRODUCT
        public virtual NodeState Evaluate() => NodeState.FAILURE;


        //Used to store shared data between behavior tree nodes
        private Dictionary<string, object> _dataContext =
            new Dictionary<string, object>();



        //Dictionary key for setting data
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }//End SetData

        //---------------------------------//
        //Used to pull data
        //Recursive and climbs up the tree until data is found or root is reached
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }//End GetData


        //---------------------------------//
        //
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }//End ClearData

        #endregion Node


        public abstract class Tree : MonoBehaviour
        {

            private Node _root = null;

            protected void Start()
            {
                _root = SetupTree();
            }

            private void Update()
            {
                if (_root != null)
                    _root.Evaluate();
            }

            protected abstract Node SetupTree();

        }
    }

}