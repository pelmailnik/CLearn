using System;
using System.Collections.Generic;
using System.Text;

namespace StrCalc
{
    class ExpressionTree<T> where T : IComparable<T>
    {
        private ExpressionTree<T> _parent, _left, _right, _position;
        private T _val = default;
        private int _commandPriority = 0;

        public ExpressionTree(ExpressionTree<T> parent = null)
        {
            _parent = parent;
            _position = this;
        }

        public void SetValue(T val)
        {
            _position._val = val;
        }

        public void ToLeft(T val)
        {
            _position._left = new ExpressionTree<T>(_position);
            _position = _position._left;
            SetValue(val);
        }

        public void ToRight()
        {
            _position._right = new ExpressionTree<T>(_position);
            _position = _position._right;
        }

        public void ToRight(T right)
        {
            _position._right = new ExpressionTree<T>(_position);
            _position = _position._right;
            SetValue(right);
        }

        public void PositionUp()
        {
            _position = _position._parent;
        }

        public void NewParent()
        {
            var tmp = new ExpressionTree<T>();
            _position._parent = tmp;
            tmp._left = _position;
            _position = _position._parent;
        }

        public void SetPriority(int priority)
        {
            _position._commandPriority = priority;
        }

        public bool IsHigherPriorityThanParent(int priority)
        {
            if (_position._parent != null)
            {
                return priority >= _position._parent._commandPriority;
            }

            return true;
        }

        public bool IsParentExists()
        {
            return _position._parent != null;
        }

        public ExpressionTree<T> GetHead()
        {
            return _position;
        }
    }
}
