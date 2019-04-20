using Jasmine.Parsers.Html;
using log4net;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Attribute = Jasmine.Parsers.Html.Attribute;

namespace Jamine.Parser.Html
{
    public class HtmlParser
    {
        public HtmlParser(ILog logger=null)
        {
            _logger = logger;
        }

        private Element _root;
        private bool _isWorkInTag;
        private int _currentIndex = -1;
        private int _length;
        private string _page;
        private StringBuilder _attributeValueBuilder = new StringBuilder();
        private StringBuilder _attributeKeyBuilder = new StringBuilder();
        private StringBuilder _nameBuilder = new StringBuilder();
        private StringBuilder _InnerTextBuilder = new StringBuilder();
        private StringBuilder _escapePatternBuilder = new StringBuilder();
        private StringBuilder _scriptEndPartBuilder = new StringBuilder();
        private bool _hasNameBeenSet;
        private bool _hasAttributeKeyBeenSet;
        private bool _hasEndFlagBeenSet;
        private Element _currentElement;
        private Stack<Element> _stack = new Stack<Element>();
        private Attribute _attribute = new Attribute();
        private ILog _logger;

        public Element Parse(string page)
        {
            if (page == null)
                throw new ArgumentNullException();

            _page = page.Trim();
            _length = _page.Length;

            reset();
            doParse();

            return getRoot();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool hasNext(int step) => step + _currentIndex < _length;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char next(int step)
        {
            _currentIndex += step;

            return _page[_currentIndex];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private char previous(int step)
        {
            _currentIndex -= step;

            return _page[_currentIndex];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void reset()
        {
            _hasAttributeKeyBeenSet = false;
            _hasNameBeenSet = false;
            _hasEndFlagBeenSet = false;
            _nameBuilder.Clear();
            _hasEndFlagBeenSet = false;
            _attributeKeyBuilder.Clear();
            _attributeValueBuilder.Clear();
            _isWorkInTag = false;
        }
        private void doParse()
        {

            while (hasNext(1))
            {
                switch (next(1))
                {
                    case '<':

                        if (_isWorkInTag)
                        {
                            recordError($"invalid char < at {_currentIndex}");
                        }
                        else
                        {
                            _currentElement = new Element()
                            {
                                StartOrEnd = StartOrEnd.Start,
                            };

                            _isWorkInTag = true;
                            checkAndFiltAnnotation();
                        }

                        break;


                    // can just in tag
                    case '/':

                        if (_isWorkInTag)
                        {
                            if (_hasEndFlagBeenSet)
                            {
                                recordError($"invalid char / at {_currentIndex} ");
                            }
                            else
                            {
                                _hasEndFlagBeenSet = true;
                                _currentElement.StartOrEnd = StartOrEnd.End;
                            }
                        }

                        break;

                    case '>':

                        if (_isWorkInTag)
                        {
                            if (!_hasNameBeenSet)
                            {
                                _currentElement.Name = _nameBuilder.ToString();
                                _currentElement.SingleOrDouble = Element.GetSingleOrDouble(Element.GetElemnetType(_currentElement.Name));
                            }

                            pushIntoStack(_currentElement);

                            // script elments should be treat spicialy ,cause them contains some distraction char 
                            //such as ,<script> alert("<div></div>") </script>
                            if (_currentElement.StartOrEnd != StartOrEnd.End && _currentElement.SingleOrDouble != SingleOrDouble.Single)
                                checkAndParseScript();

                            //
                            getInnerText();

                            reset();
                        }
                        else
                        {
                            recordError($" invalid char > at {_currentIndex}");
                        }

                        break;

                    case '\'':

                        if (!_hasAttributeKeyBeenSet)
                        {
                            recordError($"invalid char '  at {_currentIndex} ");
                        }
                        else
                        {
                            getAttributeValue('\'');
                        }

                        break;

                    case '"':

                        if (!_hasAttributeKeyBeenSet)
                        {
                            recordError($"invalid char \" at {_currentIndex} ");
                        }
                        else
                        {
                            getAttributeValue('"');
                        }

                        break;

                    case '=':

                        if (_hasAttributeKeyBeenSet)
                        {
                            recordError($" inavlid char '=' at {_currentIndex} ");
                        }
                        else
                        {
                            _hasAttributeKeyBeenSet = true;
                            _attribute.Key = _attributeKeyBuilder.ToString().Trim();
                            _attributeKeyBuilder.Clear();
                        }

                        break;

                    case ' ':
                    case '\r':
                    case '\t':
                    case '\n':

                        if (!_hasNameBeenSet)
                        {
                            _currentElement.Name = _nameBuilder.ToString().ToLower();
                            _hasNameBeenSet = true;
                            _currentElement.SingleOrDouble = Element.GetSingleOrDouble(Element.GetElemnetType(_currentElement.Name));
                        }

                        skeepWhiteSpice();

                        break;

                    default:

                        if (!_hasNameBeenSet)
                        {
                            _nameBuilder.Append(_page[_currentIndex]);
                        }
                        else
                        {
                            _attributeKeyBuilder.Append(_page[_currentIndex]);
                        }

                        break;
                }

            }

        }


        /// <summary>
        /// to handle some tag such as 
        /// <script>
        /// <div></div>
        /// </script>
        /// </summary>
        private void checkAndParseScript()
        {
            bool isScript = false;

            switch (_currentElement.Name)
            {
                case "noscript":
                case "script":
                case "style":
                case "textarea":
                    isScript = true;
                    break;
                default:
                    break;
            }

            if (isScript)
            {
                while (hasNext(1))
                {
                    next(1);

                    _InnerTextBuilder.Append(_page[_currentIndex]);

                    //skip  js quota
                    if (_page[_currentIndex] == '\'' && _page[_currentIndex - 1] != '\\')
                    {
                        skipMatchedPattern('\'');
                    }
                    //skip js biquota
                    else if (_page[_currentIndex] == '"' && _page[_currentIndex - 1] != '\\')
                    {
                        skipMatchedPattern('"');
                    }
                    //skip js single line annotation
                    else if (_page[_currentIndex] == '/' && hasNext(1) && next(1) == '/')
                    {
                        //anoter next(1) should append once more
                        _InnerTextBuilder.Append(_page[_currentIndex]);
                        skipMatchedPattern('\n');
                    }
                    //skip js mutiple annotation
                    else if (_page[_currentIndex] == '*' && _page[_currentIndex - 1] == '/')
                    {
                        skipScriptMutipleLineAnnotation();
                    }
                    //try check is script end
                    else if (_page[_currentIndex] == '<' && hasNext(1) && next(1) == '/')
                    {
                        _InnerTextBuilder.Remove(_InnerTextBuilder.Length - 1, 1);

                        bool isScriptParseEnd = false;
                        bool isBreak = false;

                        while (hasNext(1))
                        {

                            switch (next(1))
                            {
                                //name  found ,try match
                                case ' ':
                                case '\t':
                                case '\r':
                                case '\n':
                                case '>':

                                    //is match
                                    if (_scriptEndPartBuilder.ToString().Trim().ToLower() == _currentElement.Name)//inner text parse end
                                    {
                                        var element = new Element()
                                        {
                                            Name = _currentElement.Name,
                                            StartOrEnd = StartOrEnd.End,
                                        };

                                        _currentElement.InnerText = _InnerTextBuilder.ToString();
                                        _InnerTextBuilder.Clear();

                                        //  close end-tag part ,if current char is not '>'
                                        if (_page[_currentIndex] != '>')
                                            while (hasNext(1))
                                            {
                                                if (next(1) == '>')
                                                    break;
                                            }

                                        pushIntoStack(element);

                                        isScriptParseEnd = true;

                                        reset();

                                    }
                                    else
                                    {
                                        _InnerTextBuilder.Append("</")
                                                         .Append(_scriptEndPartBuilder.ToString())
                                                         .Append(_page[_currentIndex]);

                                    }

                                    _scriptEndPartBuilder.Clear();

                                    isBreak = true;

                                    break;
                                default:
                                    _scriptEndPartBuilder.Append(_page[_currentIndex]);
                                    break;
                            }

                            if (isBreak)
                                break;
                        }


                        if (isScriptParseEnd)
                            return;

                    }
                }
            }
        }


        private void pushIntoStack(Element element)
        {

            //if the element is single type ,that means the element is a child  of stack's peek 
            // if double ,do a push or a pop operatation
            //

            if (element.SingleOrDouble == SingleOrDouble.Single)//
            {
                // single element can not be  a root
                //
                if (_stack.Count > 0)
                {
                    var peek = _stack.Peek();

                    if (peek.Children.Count != 0)
                    {
                        peek.Children[peek.Children.Count - 1].Right = element;
                        element.Left = peek.Children[peek.Children.Count - 1];
                    }

                    peek.Add(element);
                }
                else
                {
                    recordError($"empty stack and push into a single element :{element.Name}!");
                }
            }
            else
            {
                //is stack  empty
                if (_stack.Count > 0)
                {
                    var peek = _stack.Peek();

                    // push element
                    if (element.StartOrEnd == StartOrEnd.Start)
                    {
                        if (peek.Children.Count != 0)
                        {
                            peek.Children[peek.Children.Count - 1].Right = element;
                            element.Left = peek.Children[peek.Children.Count - 1];
                        }

                        peek.Add(element);

                        _stack.Push(element);
                    }
                    else
                    {
                        //is the same type element
                        if (element.Name == peek.Name)
                        {
                            //meas  after poping root will be clear
                            if (_stack.Count == 1)
                            {
                                recordError($" set root {peek.Name}");
                                _root = peek;//keep the root not empty
                                _stack.Pop();
                                _currentElement = peek;//reset _current element
                            }
                            else
                            {
                                _stack.Pop();
                                _currentElement = _stack.Peek();
                            }
                        }
                        else
                        {
                            recordError($" unmatched element ${peek.Name} and {element.Name}");
                        }
                    }
                }
                else
                {
                    if (element.StartOrEnd == StartOrEnd.Start)
                    {
                        _stack.Push(element);
                    }
                    else
                    {
                        recordError($"empty stack and push into a single element :{element.Name}!");
                    }
                }
            }
        }

        private void skipScriptMutipleLineAnnotation()
        {
            while (hasNext(1))
            {
                _InnerTextBuilder.Append(_page[_currentIndex]);

                if (next(1) == '/' && _page[_currentIndex - 1] == '*')
                    return;
            }
        }


        /// <summary>
        ///  skip match-pattern grammer
        /// 1. ' ' 
        /// 2." "
        /// 3.// \n
        /// </summary>
        /// <param name="c"></param>
        private void skipMatchedPattern(char c)
        {
            while (hasNext(1))
            {
                _InnerTextBuilder.Append(_page[_currentIndex]);

                if (next(1) == c && _page[_currentIndex - 1] != '\\')
                    return;
            }
        }
        /// <summary>
        /// filt annotation element
        ///<!-- annotaion -->
        ///<!doctype>
        /// </summary>
        private void checkAndFiltAnnotation()
        {
            if (hasNext(1))
            {
                if (next(1) == '!')
                {
                    if (hasNext(1))
                    {
                        if (next(1) == '-')
                        {
                            if (hasNext(1))
                            {
                                if (next(1) == '-')
                                {

                                    while (hasNext(1))
                                    {
                                        if (next(1) == '>' && _page[_currentIndex - 1] == '-' && _page[_currentIndex - 2] == '-')
                                        {
                                            _isWorkInTag = false;

                                            _currentElement = _stack.Count == 0 ? null : _stack.Peek();

                                            getInnerText();

                                            return;
                                        }
                                    }

                                }
                                else
                                {
                                    previous(3);
                                }
                            }
                            else
                            {
                                // not match , do nothing ,and  nothing  left to parse
                            }
                        }
                        else//may be doctype
                        {
                            while (hasNext(1))
                            {
                                switch (next(1))
                                {
                                    case '>':

                                        _isWorkInTag = false;

                                        _currentElement = _stack.Count == 0 ? null : _stack.Peek();

                                        getInnerText();


                                        return;
                                    default:
                                        break;
                                }

                            }

                        }
                    }
                    else
                    {
                        // not match , do nothing ,and  nothing  left to parse
                    }
                }
                else
                {
                    previous(1);//back one char
                }
            }
        }

        private void getInnerText()
        {
            while (hasNext(1))
            {
                next(1);

                switch (_page[_currentIndex])
                {
                    //parse html escape pattern
                    // pattern start with '&' , end with ';' ,and lenth should less than 4
                    case '&':
                        _escapePatternBuilder.Append('&');



                        var i = 0;//record escape string length

                        while (hasNext(1))
                        {
                            if (i > 4)//over max length
                            {
                                _InnerTextBuilder.Append(_escapePatternBuilder.ToString());
                                _escapePatternBuilder.Clear();

                                break;
                            }

                            next(1);

                            //innertext end break token
                            if (_page[_currentIndex] == '<')
                            {
                                _InnerTextBuilder.Append(_escapePatternBuilder.ToString());
                                _escapePatternBuilder.Clear();
                                previous(1);

                                break;
                            }

                            _escapePatternBuilder.Append(_page[_currentIndex]);

                            //escape string end token
                            if (_page[_currentIndex] == ';')
                            {
                                //can replace pattern
                                if (HtmlEscapeStringMapper.Contains(_escapePatternBuilder.ToString()))
                                {
                                    _InnerTextBuilder.Append(HtmlEscapeStringMapper.Replace(_escapePatternBuilder.ToString()));
                                }
                                else
                                {
                                    _InnerTextBuilder.Append(_escapePatternBuilder.ToString());
                                    recordError($" unknow escape string ：{_escapePatternBuilder.ToString()}");
                                }

                                _escapePatternBuilder.Clear();

                                break;
                            }

                            i++;

                        }

                        break;

                    //skip whitespice
                    case ' ':
                    case '\r':
                    case '\t':
                    case '\n':

                        skeepWhiteSpice();

                        break;

                   //innertext end token
                    case '<':

                        if (_currentElement != null)
                            _currentElement.InnerText += _InnerTextBuilder.ToString();

                        _InnerTextBuilder.Clear();

                        previous(1);//back one char

                        return;

                    default:

                        _InnerTextBuilder.Append(_page[_currentIndex]);

                        break;
                }

            }
        }

        private void skeepWhiteSpice()
        {
            while (hasNext(1))
            {
                next(1);

                switch (_page[_currentIndex])
                {
                    case ' ':
                    case '\r':
                    case '\t':
                    case '\n':

                        break;

                    default:

                        previous(1);//back one char and  break while

                        return;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void getAttributeValue(char c)
        {
            while (hasNext(1))
            {
                next(1);

                //end with matched char 
                if (c == _page[_currentIndex] && _page[_currentIndex - 1] != '\\')
                {
                    _attribute.Value = _attributeValueBuilder.ToString();
                    _currentElement.Attributes.Add(_attribute);
                    _attributeValueBuilder.Clear();
                    _hasAttributeKeyBeenSet = false;
                    _attribute = new Attribute();

                    return;
                }
             
               _attributeValueBuilder.Append(_page[_currentIndex]);
               
            }
        }

        private void recordError(string msg)
        {
            _logger?.Info(msg);
        }
        private Element getRoot()
        {
            if (_root != null)
                return _root;

            while (_stack.Count != 0)
                _root = _stack.Pop();

            return _root;

        }

    }
}
