using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfschooldemo
{
    class Karname
    {
       
        string _courseName;
        float _g1;
        float _g2;
        float _g3;
        float _g4;
        float _g5;
        float _g6;
        float _g7;
        float _g8;
        float _g9;
        float _g10;
        float _grade;

        public string courseName
        {
            get
            {
                return _courseName;
            }
            set
            {
                _courseName = value;
            }
        }
       
        public float g1
        {
            get
            {
                return _g1;
            }
            set
            {
                _g1 = value;
            }
        }
        public float g2
        {
            get
            {
                return _g2;
            }
            set
            {
                _g2 = value;
            }
        }
        public float g3
        {
            get
            {
                return _g3;
            }
            set
            {
                _g3 = value;
            }
        }
        public float g4
        {
            get
            {
                return _g4;
            }
            set
            {
                _g4 = value;
            }
        }
        public float g5
        {
            get
            {
                return _g5;
            }
            set
            {
                _g5 = value;
            }
        }
        public float g6
        {
            get
            {
                return _g6;
            }
            set
            {
                _g6 = value;
            }
        }
        public float g7
        {
            get
            {
                return _g7;
            }
            set
            {
                _g7 = value;
            }
        }
        public float g8
        {
            get
            {
                return _g8;
            }
            set
            {
                _g8 = value;
            }
        }
        public float g9
        {
            get
            {
                return _g9;
            }
            set
            {
                _g9 = value;
            }
        }
        public float g10
        {
            get
            {
                return _g10;
            }
            set
            {
                _g10 = value;
            }
        }

        public float grade
        {
            get
            {
                if (_g1==-1)
                    return -1;
                else if (_g2 == -1)
                    return _g1;
                else if (_g3 == -1)
                    return (_g1+g2)/2;
                else if (_g4 == -1)
                    return (_g1 + _g2 + _g3) / 3;
                else if (_g5 == -1)
                    return (_g1 + _g2 + _g3 + _g4) / 4;
                else if (_g6 == -1)
                    return (_g1 + _g2 + _g3 + _g4 + _g5) / 5;
                else if (_g7 == -1)
                    return (_g1 + _g2 + _g3 + _g4 + _g5 + _g6) / 6;
                else if (_g8 == -1)
                    return (_g1 + _g2 + _g3 + _g4 + _g5 + _g6 + _g7) / 7;
                else if (_g9 == -1)
                    return (_g1 + _g2 + _g3 + _g4 + _g5 + _g6 + _g7 + _g8) / 8;
                else if (_g10 == -1)
                    return (_g1 + _g2 + _g3 + _g4 +_g5 + _g6 + _g7 + _g8 + _g9) / 9;
                else
                    return (_g1 + _g2 + _g3 + _g4 + _g5 + _g6 + _g7 + _g8 + _g9 + _g10) / 10;
                return -1;
            }
            set
            {
            }
        }

    }
}
