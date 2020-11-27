using System;
using System.Collections.Generic;
using System.Text;

namespace StrCalc
{
    class ExpressionTree<T> where T : IComparable<T>
    {
        private ExpressionTree<T> _parent, _left, _right, _position;
        private T _val = default;
        private int _commandPriority;

        public ExpressionTree(ExpressionTree<T> parent = null)
        {
            _parent = parent;
            _position = this;
        }

        public void Add(T val, T left, T right)
        {
            _position._left._val = left;
            _position._right._val = right;
        }

        public void SetValue(T val)
        {
            _position._val = val;
        }

        public void ToLeft()
        {
            _position._left = new ExpressionTree<T>(_position);
            _position = _position._left;
        }

        public void ToRight()
        {
            _position._right = new ExpressionTree<T>(_position);
            _position = _position._right;
        }

        public void PositionUp()
        {
            if (_position._parent == null)
            {
                ExpressionTree<T> tmp = new ExpressionTree<T>();
                _position._parent = tmp;
                tmp._left = _position;
            }
            _position = _position._parent;
        }

        public void SetPriority(int priority)
        {
            _position._commandPriority = priority;
        }

        public void AddCommandAndLeft(T val, T left, int priority)
        {
            SetValue(val);
            SetPriority(priority);
            ToLeft();
            SetValue(left);
            PositionUp();
            ToRight();
        }

        public void AddRight(T right)
        {
            SetValue(right);
        }
    }
}
