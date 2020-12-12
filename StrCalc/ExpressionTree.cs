using System;

namespace StrCalc
{
    internal class ExpressionTree
    {
        private ExpressionTree _parent, _left, _right, _position, _head;
        private int _val;
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
            _position._left.SetValue(val);
        }

        public void NewRight()
        {
            _position._right = new ExpressionTree(_position);
        }

        public void NewRight(string val)
        {
            _position._right = new ExpressionTree(_position);
            _position._right.SetValue(val);
        }

        public void NewHead()
        {
            var tmp = new ExpressionTree();
            _position._parent = tmp;
            tmp._left = _position;
            _head = tmp;
        }

        public void AddNode()
        {
            try
            {
                var tmp = new ExpressionTree { _parent = _position, _left = _position._right };

                _position._right._parent = tmp;
                _position._right = tmp;
            }
            catch (NullReferenceException)
            {
                NewHead();
            }
        }

        public void DeleteValue()
        {
            _position._val = 0;
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

        public ExpressionTree GetParent()
        {
            return _position._parent;
        }

        public void TreeCompute(ExpressionTree left, ExpressionTree right)
        {
            _position._val = right != null ? _position._command.Compute(left._val, right._val) : _position._command.Compute(left._val);
            
            _position._left = null;
            _position._right = null;
            _position._command = null;
        }

        public bool IsHigherPriorityThanParent(int priority)
        {
            return priority > _position._parent._command.Priority;
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
            //return _position._right._command != null;
            try
            {
                return _position._right._command != null;
            }
            catch (NullReferenceException)
            {
                return false;
            }
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
