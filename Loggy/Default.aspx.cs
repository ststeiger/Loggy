using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Loggy
{


    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Console.WriteLine("Hello");
            System.Diagnostics.Debug.WriteLine("world");
        }


        // https://github.com/eteran/cpp-utilities/blob/master/uint128.h
        public void uint128(string sz) //: lo(0), hi(0) 
        {

            // do we have at least one character?
            if (sz != string.Empty)
            {
                // make some reasonable assumptions
                int radix = 10;
                bool minus = false;

                // auto i = sz.begin();
                int i = 0;

                // check for minus sign, i suppose technically this should only apply
                // to base 10, but who says that -0x1 should be invalid?
                if (sz[i] == '-')
                {
                    ++i;
                    minus = true;
                }

                // check if there is radix changing prefix (0 or 0x)
                //if(i != sz.end()) {
                if (i != sz.Length)
                {
                    if (sz[i] == '0')
                    {
                        radix = 8;
                        ++i;
                        //if(i != sz.end()) {
                        if (i != sz.Length)
                        {
                            if (sz[i] == 'x')
                            {
                                radix = 16;
                                ++i;
                            }
                        }
                    }

                    //while(i != sz.end()) {
                    while (i != sz.Length)
                    {
                        uint n;
                        char ch = sz[i];

                        if (ch >= 'A' && ch <= 'Z')
                        {
                            if (((ch - 'A') + 10) < radix)
                            {
                                n = ((uint)ch - 'A') + 10;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (ch >= 'a' && ch <= 'z')
                        {
                            if (((ch - 'a') + 10) < radix)
                            {
                                n = ((uint)ch - 'a') + 10;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (ch >= '0' && ch <= '9')
                        {
                            if ((ch - '0') < radix)
                            {
                                n = ((uint)ch - '0');
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            /* completely invalid character */
                            break;
                        }

                        ////////////////////////(*this) *= radix;
                        ////////////////////////(*this) += n;

                        ++i;
                    }
                }

                // if this was a negative number, do that two's compliment madness :-P
                if (minus)
                {
                    ///////////////////////*this = -*this;
                }
            }

        } // End UInt128 - constructor 


    }

}