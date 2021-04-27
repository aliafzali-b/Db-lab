using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTeacherDemo
{
    class Teachers
    {
        int _id;
        string _username;
        string _password;
        string _name;
        string _takhasos;
        string _classes1;
        string _classes2;
        string _classes3;
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
        public string takhasos
        {
            get
            {
                return _takhasos;
            }
            set
            {
                _takhasos = value;
            }
        }
        public string classes1
        {
            get
            {
                return _classes1;
            }
            set
            {
                _classes1 = value;
            }
        }
        public string classes2
        {
            get
            {
                return _classes2;
            }
            set
            {
                _classes2 = value;
            }
        }
        public string classes3
        {
            get
            {
                return _classes3;
            }
            set
            {
                _classes3 = value;
            }
        }
    }
}
