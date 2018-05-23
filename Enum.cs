namespace Fal.DataStructure.Tree
{
    /********************************************
     * To tag start or end of double element
     * just as <div> → start , </div> → end
     * ***************************************/
    public enum Start_Or_End
    {
        END, START, UNKNOW,
    }
    /**********************************
     * To tag element s_d type
     * as <div></div> → Double
     * <img> → Single
     * ********************************/
    public enum Single_Or_Double
    {
        BOUBLE, SINGLE, UNKNOW,
    }
    /**************************************
     *  To mark html element type
     * ************************************/
    public enum Element_Type
    {
        denote, doctype, a, abbr, address, applet, acronym, area, article, aside, audio, b, bese,
        basefont, bdi, bdo, big, blockquote, body, br, button, canvas, caption, center, cite,
        code, col, colgroup, command, datalist, dd, del, details, dfn, dir, div, dl, dt, em,
        embed, fieldset, figcaption, figure, font, footer, form, frame, frameset, h1, h2, h3, h4, h5, h6,
        head, header, hgroup, hr, html, i, iframe, img, input, ins, keygen, kbd, label, legend, li, link, map,
        mark, menu, meta, meter, nav, noframes, noscript, abject, ol, optgroup, option, output, p, param, pre, progress, q,
        rp, ruby, s, samp, script, select, small, source, span, strike, strong, style, sub, summary, sup, table, tbody, td,
        textarea, tfoot, th, thead, time, title, tr, track, tt, u, ul, var, video, wbr, rt, section, unknow
    }
}
