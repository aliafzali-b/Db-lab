using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfschooldemo
{
    class Classes
    {
        public int classid;
        string _name;
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
        public int id
        {
            get
            {
                return classid;
            }
            set
            {
                classid = value;
            }
        }
    }
}
