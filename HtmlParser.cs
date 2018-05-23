 /************************************
     * html parser
     * it can parser the html text and transnate to a domtree
     * design by fauanlei,all rights reservevd,Oct 13th,2017
     * email:18108342266@163.com
     * 
     * **********************************/
    public class HtmlParser
    {
        /// <summary>
        /// record errors
        /// </summary>
        public List<Html_Exception> Errors = new List<Html_Exception>();
        /// <summary>
        /// 解析html string 返回 dom 根节点
        /// </summary>
        /// <param name="str">html string</param>
        /// <param name="ignorDenote">
        /// 如果为真，不加入注释标签到文档树中
        /// 否则 加入文档树中
        /// </param>
        /// <returns></returns>
        public Element Parse(string str, bool ignorDenote = true)
        {
            if (str is null)
                throw new Exception($"{nameof(str)} is null");
            _ignorDenote = ignorDenote;
            _root = null;
            _str = str;
            return parse(str);

        }
        /// <summary>
        /// ignore denote
        /// </summary>
        private bool _ignorDenote;
        /// <summary>
        /// use to break main function
        /// </summary>
        private bool _isfinished;
        /// <summary>
        /// use to check attribute value has beeen set
        /// </summary>
        private bool _isValueHasBeenSet;
        /// <summary>
        /// use to check attribute key has beeen set
        /// </summary>
        private bool _isAttibuteKeyHasBeenSet;
        /// <summary>
        /// use to check is working in element cotext
        /// </summary>
        private bool _isWorkingInElement;
        /// <summary>
        /// use to check has element name been set
        /// </summary>
        private bool _isElementNameHasBeenSet;
        /// <summary>
        /// use to check has element start-end been set
        /// </summary>
        private bool _isStartOrEndHasBeenSet;
        /// <summary>
        /// temp Element
        /// </summary>
        private Element _temp;
        /// <summary>
        ///  the _stack
        /// </summary>
        private Stack<Element> _stack = new Stack<Element>();
        /// <summary>
        /// use to sava Name or Value string Temperarily
        /// </summary>
        private StringBuilder _sb = new StringBuilder();
        /// <summary>
        /// use to save current Keychar
        /// </summary>
        private char _token;
        /// <summary>
        /// temp item
        /// </summary>
        private Attribute_Item _item;
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// use to save input string
        /// </summary>
        private string _str;
        /// <summary>
        /// no use
        /// </summary>
        private string _FILE_ { get; set; } = "";
        /// <summary>
        /// use to save current line
        /// </summary>
        private int _LINE_ { get; set; }
        /// <summary>
        /// use to save current index of line
        /// </summary>
        private int _INDEXOFLINE_ { get; set; }
        /// <summary>
        /// use to save currnt index ,it's important
        /// </summary>
        private int _INDEX_ { get; set; }
        /// <summary>
        /// 错误计数
        /// </summary>
        private int _Error_Number { get; set; }
        /// <summary>
        /// jrecord first double element is found
        /// </summary>
        private bool _isDoubleElementFound;
        /// <summary>
        /// record first double element
        /// </summary>
        private Element _firstDoubleElemen;
        /// <summary>
        /// use for return
        /// </summary>
        private Element _root;
        /// <summary>
        /// main function
        /// </summary>
        /// <param name="str">htmlstring</param>
        /// <param name="ignorDenote"></param>
        /// <returns>return root node of html domtree</returns>
        private Element parse(string str)
        {

            while (true)
            {

                /**********************
                * SkipWhiteSpace and get token
                * ****************************/
                skipWhiteSpaceAndGetToken();
                /**********************************************
                 * check is finish
                 * ******************************/
                if (_isfinished)
                {
                    getRoot();
                    return _root;
                }

                switch (_token)
                {
                    case '<':
                        /******************
                         * got two <
                         * *********************/
                        if (_isWorkingInElement)
                        {
                            error("Invalid <");
                            break;
                        }

                        else
                        {
                            /*************************
                             * 生成新节点
                             * ***********************/
                            _temp = new Element() { Start_Or_End = Start_Or_End.START, Position_start = _INDEX_ - 1 };
                            _isWorkingInElement = true;
                            /*********************
                             * 处理注释问题
                             * ****************/
                            handDenote();
                            break;
                        }

                    case '>':
                        if (_temp != null)
                        {

                            /*********************
                             * 记录结束位置
                             * ****************/
                            _temp.Position_end = _INDEX_ - 1;
                            /*****************
                             * 处理未知元素问题
                             * **********************/
                            if (_temp.Element_Type == Element_Type.unknow)
                            {
                                error($"unknow element {_temp.ToString()}");
                                break;
                            }

                            /****************
                            * 处理脚本问题
                            * *********************/
                            if (_temp.Start_Or_End == Start_Or_End.START)
                                handScript();
                            /*************
                             * 添加进栈
                             * ********************/
                            setStack();
                            /***********************
                             * Reset paramers
                             * *******************************/
                            reset();
                        }
                        else
                          goto _out;
                        break;
                    case '/':
                        /**************
                         * 检查是否位于元素中
                         * ******************/
                        if (!_isWorkingInElement)
                            goto _out;
                        /*******************
                         * 检查起始字符是否被设置
                         * **********************/
                        if (!_isStartOrEndHasBeenSet)
                            _temp.Start_Or_End = Start_Or_End.END;
                        /*******************
                         * two '/'
                         * ****************************/
                        else
                            goto _out;
                        break;
                    case '=':
                        /**************
                         * 检查是否位于元素中
                         * ******************/
                        if (!_isWorkingInElement)
                            goto _out;

                        /**************
                         * 检查属性键是否被设置
                         * ******************/
                        if (!_isAttibuteKeyHasBeenSet)
                            _isAttibuteKeyHasBeenSet = true;
                        /**********
                         * two '='
                         * **************************/
                        else
                            goto _out;
                        break;
                    case '\'':
                        if (!_isWorkingInElement)
                            goto _out;
                        /************
                         * hasn't got key ,but got the value
                         * ***********************/
                        if (!_isAttibuteKeyHasBeenSet)
                            goto _out;
                        else
                        {    /*********
                             * has two value
                             * ***********/
                            if (_isValueHasBeenSet)
                                error($"Invalia Syntax {_sb.ToString()}");
                            else
                            {
                                getValueSingleQuotation();
                                _item.Value = _sb.ToString();
                                _temp.Attributes.Add(_item);
                                _sb.Clear();
                                _isAttibuteKeyHasBeenSet = false;
                            }
                        }
                        break;

                    case '"':
                        if (!_isWorkingInElement)
                            goto _out;
                        /************
                         * hasn't got key ,but got the value
                         * ***********************/
                        if (!_isAttibuteKeyHasBeenSet)
                            goto _out;
                        else
                        {    /*********
                             * has two value
                             * ***********/
                            if (_isValueHasBeenSet)
                                error($"Invalia Syntax {_sb.ToString()}");
                            else
                            {
                                getValueBiQuotation();
                                _item.Value = _sb.ToString();
                                _temp.Attributes.Add(_item);
                                _sb.Clear();
                                _isAttibuteKeyHasBeenSet = false;
                            }
                        }
                        break;
                    default:
                        /*******************
                         * 唯一跳转点
                         * ****************/
                        _out:
                        /*************************
                         * 检查是否在元素中，
                         * 是，可能是获取标签名，或者属性名，或者属性值
                         * 有些网页可能会出现 name=ch这样的属性形式
                         * 还可能 name='ch'单引号的形式
                         * *****************************/
                        if (_isWorkingInElement)
                        {
                            /************
                             * 获取名字字符串
                             * **************/
                            get_Name();
                            /******************
                             * 检查是否元素名被设置
                             * ***************/
                            if (!_isElementNameHasBeenSet)
                            {
                                _temp.Name = _sb.ToString().ToLower();
                                _isElementNameHasBeenSet = true;
                                _temp.Element_Type = Semi_Element.get_Type(_temp.Name);
                                _temp.Single_Or_Double = Semi_Element.get_Single_Or_Double(_temp.Element_Type);

                            }
                            /*******************
                             * 检查属性键是否被设置
                             * *****************************/
                            else if (!_isAttibuteKeyHasBeenSet)
                                _item = new Attribute_Item() { Key = _sb.ToString() };
                            /*******************
                             * name=ch 这么奇葩形式 
                             * *******************/
                            else
                            {
                                _item.Value = _sb.ToString();
                                _sb.Clear();
                                _temp.Attributes.Add(_item);

                                _isAttibuteKeyHasBeenSet = false;
                            }

                        }
                        /*******************
                         * 获取innertext
                         * ********************/
                        else
                        {
                            get_InnerText();
                            /**********************
                             * 出现在文档开头
                             * ***************/
                            if (_stack.Count == 0)
                                error(_sb.ToString());
                           /***********************************
                            * 单标签不能包含innertext
                            * **********************************/
                           else if (_stack.Peek().Single_Or_Double == Single_Or_Double.SINGLE)
                                error(_sb.ToString());
                            /**********************
                             * 正确
                             * *****************/
                           else
                                _stack.Peek().InnerText = _sb.ToString();

                        }
                        /****************************
                         * 清理_sb
                         * *************************/
                        _sb.Clear();
                        break;
                }
            }
        }
        /// <summary>
        /// 获取元素名，属性名，或者属性值
        /// </summary>
        private void get_Name()
        {
            _sb.Append(_token);
            while (true)
            {
                /**************************
                 * is ending
                 * **********************/
                if (_INDEX_ == _str.Length)
                {
                    _isfinished = true;
                    return;
                }
                switch (_str[_INDEX_])
                {
                    /********************************************
                     * end process and _Index_ Increase 1 
                     * *****************************************/
                    case ' ':
                    case '\r':
                    case '\t':
                        _INDEXOFLINE_++;
                        _INDEX_++;
                        return;
                    case '\n':
                        _LINE_++;
                        _INDEXOFLINE_ =-1;
                        _INDEX_++;
                        return;
                    /*********************************
                     * end process
                     * ********************************/
                    case '=':
                    case '/':
                    case '>':
                    case '"':
                    case '<':
                        return;
                    default:
                        _sb.Append(_str[_INDEX_]);
                        break;
                }
                _INDEXOFLINE_++;
                _INDEX_++;

            }
        }
        /// <summary>
        /// 获取innertext
        /// </summary>
        private void get_InnerText()
        {

            _INDEXOFLINE_--;
            _INDEX_--;
            _sb.Append(_str[_INDEX_]);
            while (true)
            {
                _INDEXOFLINE_++;
                _INDEX_++;
                if (_INDEX_ == _str.Length)
                {
                    _isfinished = true;
                    return;
                }
                _token = _str[_INDEX_];
                switch (_token)
                {
                    case '<':
                        return;
                    case '\t':
                    case ' ':
                    case '\r':
                    case '\f':
                        break;
                    case '\n':
                        _INDEXOFLINE_ = -1;
                        _LINE_++;
                        break;
                        /******************
                         * 处理html转义字符
                         * ****************************/
                    case '&':
                        if (_INDEX_ + 5 < _str.Length)
                        {
                            var t = _str.Substring(_INDEX_, 5);
                            if (HtmlEscapCharHelper.Contains(_token + t))
                            {
                                _sb.Append(t);
                                _INDEX_ += 5;
                                _INDEXOFLINE_ += 5;
                                break;
                            }

                        }
                       else if (_INDEX_ + 4 < _str.Length)
                        {
                            var t = _str.Substring(_INDEX_, 4);
                            if (HtmlEscapCharHelper.Contains(_token + t))
                            {
                                _sb.Append(t);
                                _INDEX_ += 4;
                                _INDEXOFLINE_ += 4;
                                break;
                            }
                        }
                      else  if (_INDEX_ + 3 < _str.Length)
                        {
                            var t = _str.Substring(_INDEX_, 3);
                            if (HtmlEscapCharHelper.Contains(_token + t))
                            {
                                _sb.Append(t);
                                _INDEX_ += 3;
                                _INDEXOFLINE_ += 3;
                                break;
                            }
                        }
                     else  if (_INDEX_ + 2 < _str.Length)
                        {
                            var t = _str.Substring(_INDEX_, 2);
                            if (HtmlEscapCharHelper.Contains(_token + t))
                            {
                                _sb.Append(t);
                                _INDEX_ += 2;
                                _INDEXOFLINE_ += 2;
                                break;
                            }
                        }

                        _sb.Append(_token);
                        break;


                    default:
                        _sb.Append(_str[_INDEX_]);
                        break;
                }
            }
        }
        /// <summary>
        /// 跳过无效字符，
        /// 设置 _token
        /// </summary>
        private void skipWhiteSpaceAndGetToken()
        {
            bool flag = false;
            while (true)
            {
                if (_INDEX_ >= _str.Length)
                {
                    _isfinished = true;
                    break;
                }
                switch (_str[_INDEX_])
                {
                    case ' ':
                    case '\r':
                    case '\t':
                        break;
                    case '\n':
                        _LINE_++;
                        _INDEXOFLINE_ = -1;
                        break;
                    default:
                        _token = _str[_INDEX_];
                        flag = true;
                        break;
                }
                _INDEXOFLINE_++;
                _INDEX_++;
                if (flag)
                    break;
            }
        }
        /*****************
         * 重置参数
         * **************************/
        private void reset()
        {
            _isWorkingInElement = false;
            _isElementNameHasBeenSet = false;
            _isStartOrEndHasBeenSet = false;
            _isValueHasBeenSet = false;
            _temp = null;
            _sb.Clear();
        }
        /// <summary>
        /// 检查是否是注释
        /// 注：未分单行还是多行
        /// 多行也是当成单行处理的
        /// </summary>
        private void handDenote()
        {
            /***************
             * check is denote
             * ***********************/
            bool isdenote = false;
            if (_INDEX_ + 1 < _str.Length)
            {
                if (_str[_INDEX_] == '!')
                    if (_str[_INDEX_ + 1] == '-')
                    {
                        isdenote = true;
                        _INDEX_ += 2;
                        _INDEXOFLINE_ += 2;
                    }
            }
            else
            {
                _isfinished = true;
                return;
            }


            if (isdenote)
                while (true)
                {
                    _INDEX_++;
                    _INDEXOFLINE_++;

                    if (_INDEX_ == _str.Length)
                    {
                        _isfinished = true;
                        return;
                    }
                    _token = _str[_INDEX_];
                    switch (_token)
                    {
                        /********************
                         * 检查注释是否结束
                         * **************************/
                        case '-':
                            if (_INDEX_ + 2 != _str.Length)
                            {
                                if (_str[_INDEX_ + 1] == '-')
                                {
                                    if (_str[_INDEX_ + 2] == '>')
                                    {
                                        _temp.InnerText = _sb.ToString();
                                        _temp.Single_Or_Double = Single_Or_Double.SINGLE;
                                        _sb.Clear();
                                        setStack();
                                        reset();
                                        _INDEX_ += 3;
                                        _INDEXOFLINE_ += 3;
                                        return;
                                    }
                                }
                                else
                                    _sb.Append(_str[_INDEX_]);
                            }
                            else
                                _isfinished = true;
                            break;
                        case '\n':
                            _INDEXOFLINE_ = -1;
                            _LINE_++;
                            _sb.Append(_str[_INDEX_]);
                            break;

                        default:
                            _sb.Append(_str[_INDEX_]);
                            break;
                    }


                }
        }
        /// <summary>
        /// 压栈出栈，建立关系
        /// </summary>
        private void setStack()
        {
            /**********************************************
             * 处理denote是否加入文档树
             * ***********************************************/
            if(_ignorDenote)
            {
                if (_temp.Element_Type == Element_Type.denote)
                    return;
            }

            /*************************
             * 避免html 消失
             * ************************/
            if (!_isDoubleElementFound)
            {
                if (_temp.Single_Or_Double == Single_Or_Double.BOUBLE)
                {
                    _firstDoubleElemen = _temp;
                    _isDoubleElementFound = true;
                }

            }
            /***************
             * peek
             * *****************/
            Element peek = null;
            if (_stack.Count != 0)
                peek = _stack.Peek();
            /**************
             * 压入第一个元素
             * **************/
            if (peek == null)
            {
                _stack.Push(_temp);
                return;
            }
            /************************
             * 准备压入单标签
             * ****************************/
            if (_temp.Single_Or_Double == Single_Or_Double.SINGLE)
            {
                /***************
                 * peek is single
                 * *********************/
                if (peek.Single_Or_Double == Single_Or_Double.SINGLE)
                {
                    peek.Next = _temp;
                    _temp.Previous = peek;
                    _stack.Push(_temp);
                }
                else
                {
                    if (peek.Children.Count!=0)
                    {
                        _temp.Previous = peek[peek.Children.Count - 1];
                        peek[peek.Children.Count - 1].Next = _temp;
                    }

                     peek.Children.Add(_temp);
                    _temp.Father = peek;
                   
                }
            }
            else
            {
                /*************************************
                 * if _temp is start element
                 * **********************************/
                if (_temp.Start_Or_End == Start_Or_End.START)
                {
                    /****************
                     * peek is double
                     * ************************/
                    if (peek.Single_Or_Double == Single_Or_Double.BOUBLE)
                    {
                        _stack.Push(_temp);
                        peek.Children.Add(_temp);
                        _temp.Father = peek;

                    }
                    else
                    {
                        _stack.Push(_temp);
                        peek.Next = _temp;
                        _temp.Previous = peek;
                    }
                }
                /**********************
                 * peek is end 
                 * *******************************/
                else
                {
                    /**********************************
                     * 不匹配
                     * ***************************/
                    if (peek.Single_Or_Double == Single_Or_Double.SINGLE)
                        error($"elementype doesn't match {_temp.Position_start}-{_temp.Position_end}");
                    else
                    {
                        /**********************
                         * 类型匹配出栈
                         * ******************************/
                        if (peek.Element_Type == _temp.Element_Type)
                        {
                            peek.Position_end = _temp.Position_end;
                          _stack.Pop();
                          
                        }
                        /**************
                         * 不匹配
                         * *****************/
                        else
                            error($"elementype doesn't match {_temp.Position_start}-{_temp.Position_end}");
                    }
                }

            }

        }
        /***************
         * 处理类似 js代码的问题
         * 包括处理 js注释 // /** /
         * 和 引号的过滤 
         * 内容过滤 
         * 如:
         * <script>
         * // fddsfsdf
         * document.write("<div></div>");
         * /* gfffffffffffffffffffffff
         * ffffffffffff * /
         * </script>
         * ************************/
        private void handScript()
        {
            /***************
             * check is script type
             * *******************/
            var type = "";
            switch (_temp.Name)
            {
                case "script":
                    type = "script";
                    break;
                case "style":
                    type = "style";
                    break;
                case "textarea":
                    type = "textarea";
                    break;
                case "noscript":
                    type = "noscript";
                    break;
                default:
                    return;
            }

            /****************************
             * use for quotation and denote
             * ****************************/
            bool isindenote = false;
            bool issingledenote = false;
            bool isinquota = false;
            _INDEXOFLINE_--;
            _INDEX_--;
            while (true)
            {
                /**************
                 * increase _index_
                 * *******************/
                _INDEX_++;
                _INDEXOFLINE_++;

                /****************
                 * check is finish
                 * **********************/
                if (_INDEX_ >= _str.Length)
                {
                    _isfinished = true;
                    break;
                }
                /**********
                 * set token
                 * ************************/
                _token = _str[_INDEX_];

                switch (_token)
                {
                    case '/':
                        /***********
                         * if in denote
                         * *******************/
                        if (isindenote)
                        {
                            /***********************
                             * muti-line denote end
                             * ***********************/
                            if (!issingledenote)
                            {
                                if (_str[_INDEX_ - 1] == '*')
                                {
                                    isindenote = false;
                                    issingledenote = false;
                                }
                            }
                            _sb.Append(_str[_INDEX_]);
                        }
                        else
                        {
                            /*************
                             * check is finish
                             * *****************/
                            if (_INDEX_ + 1 == _str.Length)
                            {
                                _isfinished = true;
                                return;
                            }


                            if (!isinquota)
                            {
                                /***************
                                 * single denote
                                 * *****************/
                                if (_str[_INDEX_ + 1] == '/')
                                {
                                    isindenote = true;
                                    issingledenote = true;
                                    _sb.Append(_str[_INDEX_]);
                                    _sb.Append(_str[_INDEX_ + 1]);
                                    _INDEX_++;
                                    _INDEXOFLINE_++;

                                }
                                /***********
                                 * muti-line denote
                                 * ************/
                                if (_str[_INDEX_ + 1] == '*')
                                {
                                    isindenote = true;
                                    issingledenote = false;
                                    _sb.Append(_str[_INDEX_]);
                                    _sb.Append(_str[_INDEX_ + 1]);
                                    _INDEX_++;
                                    _INDEXOFLINE_++;

                                }
                            }

                            else
                                _sb.Append(_str[_INDEX_]);
                        }

                        break;
                    case '\"':
                        if (isindenote)
                        {
                            _sb.Append(_str[_INDEX_]);
                            break;
                        }
                        if (isinquota)
                        {
                            if (_str[_INDEX_ - 1] != '\\')
                                isinquota = false;
                        }
                        else
                        {
                            if (_str[_INDEX_ - 1] != '\\')
                                isinquota = true;
                        }
                        _sb.Append(_str[_INDEX_]);
                        break;
                    case '<':
                        if (isindenote)
                        {
                            _sb.Append(_str[_INDEX_]);
                            break;
                        }

                        if (isinquota)
                        {
                            _sb.Append(_str[_INDEX_]);
                        }
                        else
                        {


                            var t = _str.IndexOf(">", _INDEX_);
                            if (t == -1)
                            {
                                _isfinished = true;
                                return;
                            }
                            else
                            {
                                var b = _str.Substring(_INDEX_, t - _INDEX_).ToLower();
                                if (b.Length < 20)
                                    if (b.Contains(type))
                                    {
                                        _temp.InnerText = _sb.ToString();
                                        _sb.Clear();
                                        return;
                                    }
                            }

                            _sb.Append(_str[_INDEX_]);

                        }
                        break;
                    case '\n':
                        if (!isinquota)
                        {
                            _INDEXOFLINE_ = -1;
                            _LINE_++;
                        }
                        if (isindenote)
                            if (issingledenote)
                            {
                                issingledenote = false;
                                isindenote = false;
                            }
                        _sb.Append(_str[_INDEX_]);
                        break;
                    default:
                        _sb.Append(_str[_INDEX_]);
                        break;
                }


            }

        }
        /************
         * 获取一单引号开头的 属性值
         * ************************/
        private void getValueSingleQuotation()
        {
            while (true)
            {
                if (_INDEX_ == _str.Length)
                {
                    _isfinished = true;
                    return;
                }

                /********************
                 * 处理引号并添加值
                 * *************************/

                if (_str[_INDEX_] != '\'')
                    _sb.Append(_str[_INDEX_]);
                else
                {
                    if (_str[_INDEX_ - 1] != '\\')
                    {
                        _INDEXOFLINE_++;
                        _INDEX_++;
                        return;
                    }
                    else
                        _sb.Append(_str[_INDEX_]);
                }

                _INDEXOFLINE_++;
                _INDEX_++;

            }
        }
        /***************************
         * 获取以双引号开头的属性值
         * ****************************/
        private void getValueBiQuotation()
        {
            while (true)
            {
                if (_INDEX_ == _str.Length)
                {
                    _isfinished = true;
                    return;
                }

                /********************
                 * 处理引号并添加值
                 * *************************/

                if (_str[_INDEX_] != '"')
                    _sb.Append(_str[_INDEX_]);
                else
                {
                    if (_str[_INDEX_ - 1] != '\\')
                    {
                        _INDEXOFLINE_++;
                        _INDEX_++;
                        return;
                    }
                    else
                        _sb.Append(_str[_INDEX_]);
                }

                _INDEXOFLINE_++;
                _INDEX_++;

            }
        }
      
        /// <summary>
        /// inheriting all 'semi-element' 
        /// </summary>
        /// 
        private void getRoot()
        {
            
            if (_stack.Count == 0)
                _root = _firstDoubleElemen;
            else
            {
                _stack = new Stack<Element>();
                bool flag = false;
                while(_stack.Count!=0)
                {
                    var b = _stack.Pop();
                    if (b.Single_Or_Double == Single_Or_Double.BOUBLE)
                        flag = true;
                    _root.Children.Add(b);
                    b.Father = _root;
                }
               
                if (_firstDoubleElemen != null)
                    if (!flag)
                    {
                        _root.Add(_firstDoubleElemen);
                        _firstDoubleElemen.Father = _root;
                    }
            }

        }
        /// <summary>
        /// save error to Errors;
        /// </summary>
        /// <param name="description"></param>
        private void error(string description)
        {
            Errors.Add(new Html_Exception(_LINE_, _INDEXOFLINE_, _INDEX_, _Error_Number, _FILE_, description));
            ++_Error_Number;
        }
    }
