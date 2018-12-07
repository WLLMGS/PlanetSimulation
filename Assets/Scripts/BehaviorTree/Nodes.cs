using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BehaviorTree
{   
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }


    public interface INode
    {
        NodeState Tick();
    }

    public abstract class CompositeNode : INode
    {
        protected INode[] _nodes;

        public CompositeNode(params INode[] nodes)
        {
            _nodes = nodes;
        }

        public abstract NodeState Tick();
    }


    public class SelectorNode : CompositeNode
    {
        public SelectorNode(params INode[] nodes) : base(nodes)
        { }

        public override NodeState Tick()
        {
            foreach(var node in _nodes)
            {
                NodeState result = node.Tick();

                switch(result)
                {
                    case NodeState.Running:
                        return result;
                        break;
                    case NodeState.Failure:
                        break;
                    case NodeState.Success:
                        return result;
                        break;
                }
            }


            return NodeState.Failure;
        }
    }

    public class SequenceNode : CompositeNode
    {
        public SequenceNode(params INode[] nodes) : base(nodes)
        {}

        public override NodeState Tick()
        {

            foreach(var node in _nodes)
            {
                NodeState result = node.Tick();

                switch(result)
                {
                    case NodeState.Running:
                        return result;
                        break;
                    case NodeState.Success:
                        break;
                    case NodeState.Failure:
                        return result;
                        break;

                }
            }

            return NodeState.Success;
        }
    }


    public class ConditionNode : INode
    {

        public delegate bool Condition();
        private Condition _condition;
      
        public ConditionNode(Condition cond)
        {
            _condition = cond;
            
        }

        public NodeState Tick()
        {
            return _condition() ? NodeState.Success : NodeState.Failure;
        }
    }

    public class ActionNode : INode
    {
        public delegate NodeState Action();
        private Action _action;
        

        public ActionNode(Action action)
        {
            _action = action;
            
        }

        public NodeState Tick()
        {
            return _action();
        }
    }

    
}
