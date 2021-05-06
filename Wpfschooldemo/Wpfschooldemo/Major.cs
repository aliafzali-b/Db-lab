using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfschooldemo
{
    class Major
    {
        string _name;
        int majorid;
        int mcode;

        public int id
        {
            get
            {
                return majorid;
            }
            set
            {
                majorid = value;
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

        public int code
        {
            get
            {
                return mcode;
            }
            set
            {
                mcode = value;
            }
        }
    }
}
