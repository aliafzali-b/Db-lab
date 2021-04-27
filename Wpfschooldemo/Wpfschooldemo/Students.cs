using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfschooldemo
{
    class Students
    {
        int _id;
        string _username;
        string _password;
        string _name;
        string _fathername;
        long _phonenumber;
        string _major;
        int _classid;
        string _classname;

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
        public string username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }
        public string password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
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
        public string fathername
        {
            get
            {
                return _fathername;
            }
            set
            {
                _fathername = value;
            }
        }
        public long phonenumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
            }
        }
        public string major
        {
            get
            {
                return _major;
            }
            set
            {
                _major = value;
            }
        }
        public int classid
        {
            get
            {
                return _classid;
            }
            set
            {
                _classid = value;
            }
        }
        public string classname
        {
            get
            {
                return _classname;
            }
            set
            {
                _classname = value;
            }
        }
    }
}
