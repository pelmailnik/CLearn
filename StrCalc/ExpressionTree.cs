using System;

namespace StrCalc
{
    internal class ExpressionTree
    {
        private ExpressionTree _parent, _left, _right, _position, _head;
        private int _val = default;
        private CommandInfo _command;

        public ExpressionTree(ExpressionTree parent = null)
        {
            _parent = parent;
            _position = this;
            _head = this;
        }

        public void SetValue(string val)
        {
            _position._val = Convert.ToInt32(val);
        }

        public int GetValue()
        {
            return _position._val;
        }

        public void SetCommand(CommandInfo command, int buf)
        {
            _position._command = new CommandInfo(
                command.Priority + buf,
                command.Symbol,
                command.IsBinary,
                command.MathematicalOperation
                );
        }

        public void NewLeft(string val)
        {
            _position._left = new ExpressionTree(_position);
            _position = _position._left;
            SetValue(val);
        }

        public void NewRight()
        {
            _position._right = new ExpressionTree(_position);
            _position = _position._right;
        }

        public void NewRight(string right)
        {
            _position._right = new ExpressionTree(_position);
            _position = _position._right;
            SetValue(right);
        }

        public void NewParent()
        {
            var tmp = new ExpressionTree();
            _position._parent = tmp;
            tmp._left = _position;
            _position = _position._parent;
            _head = _position;
        }

        public void PositionUp()
        {
            _position = _position._parent;
        }

        public void ToLeft()
        {
            _position = _position._left;
        }

        public void ToRight()
        {
            _position = _position._right;
        }

        public ExpressionTree GetLeft()
        {
            return _position._left;
        }

        public ExpressionTree GetRight()
        {
            return _position._right;
        }

        public void TreeCompute(ExpressionTree left, ExpressionTree right)
        {
            _position._val = _position._command.Compute(left._val, right._val);
            _position._left = null;
            _position._right = null;
            _position._command = null;
        }

        public bool IsHigherPriorityThanParent(int priority)
        {
            if (_position._parent != null)
            {
                return priority > _position._parent._command.Priority;
            }

            return true;
        }

        public bool IsParentExists()
        {
            return _position._parent != null;
        }

        public bool InLeftCommand()
        {
            return _position._left._command != null;
        }

        public bool InRightCommand()
        {
            return _position._right._command != null;
        }

        public bool IsTreeExists()
        {
            return (_head._left != null) & (_head._right != null);
        }

        public ExpressionTree GetHead()
        {
            return _head;
        }

        public void ResetPosition()
        {
            _position = _head;
        }
    }
}
