using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfschooldemo
{
    class Courses
    {
        int _id;
        string _name;
        string _classes;
        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string classes
        {
            get
            {
                return _classes;
            }
            set
            {
                _classes = value;
            }
        }
    }
}
