﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Classes
{
    public static class Lists
    {
        public static List<string> newArrayList(string[] lists)
        {
            List<string> list = new List<string>();
            foreach (var i in lists)
            {
                list.Add(i);
            }
            return list;
        }
    }
}