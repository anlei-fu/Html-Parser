using System;

namespace Fal.StateMachine
{
    /*******************************************
 * copyright(©) all rights deserved by fal 2017
 * e-mail:18108342263@163.com
 * qq:767550758
 * ******************************************/
    public class PaserException:Exception
    {
        public PaserException(int line,int indexofline,int index,int error_number, string filename,string error_description)
        {
            _description = error_description;
            _line = line;
            _index = index;
            _indexofline = indexofline;
            _filename = filename;
            _errornumber = error_number;
        }
        public string _description;
        public int _line;
        public int _index;
        public string _filename ;
        public int _indexofline;
        public int _errornumber;

        public override string ToString()
        {
            return $"{_description}\r\nLine:{_line}\tIndex:{_indexofline}\r\nFile:{_filename}\r\nErrorNumber:{_errornumber}";
        }
    }
}
