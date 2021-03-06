﻿/*
 *  Copyright (C) 2013 Pavel Charvat
 * 
 *  This file is part of IEDExplorer.
 *
 *  IEDExplorer is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  IEDExplorer is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with IEDExplorer.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEDExplorer
{
    class NodeIed: NodeBase
    {
        private string  _vendorName;
        private string  _modelName;
        private string _revision;
        private bool _defineNVL;
        private bool _identify;

        public NodeIed(string Name)
            : base(Name)
        {
        }
        public string  VendorName {
            get { return _vendorName;}
            set { _vendorName = value; }
        }

        public string  ModelName {
            get { return _modelName; }
            set { _modelName = value; }
        }

        public string Revision {
            get { return _revision; }
            set { _revision = value; }
        }

        public bool DefineNVL
        {
            get { return _defineNVL; }
            set { _defineNVL = value; }
        }

        public bool Identify
        {
            get { return _identify; }
            set { _identify = value; }
        }

        public NodeBase FindNodeByAddress(string Domain, string IecAddress, bool FindList = false)
        {
            if (Domain == null || IecAddress == null)
                return null;
            NodeBase b = this.FindChildNode(Domain);
            if (b != null)
            {
                if (FindList)
                {
                    if ((b = b.FindChildNode(IecAddress)) == null)
                    {
                        return null;
                    }
                }
                else
                {
                    string[] parts = IecAddress.Split(new char[] { '$' });
                    for (int i = 0; i < parts.Length; i++)
                    {
                        if ((b = b.FindChildNode(parts[i])) == null)
                        {
                            return null;
                        }
                    }
                }
                return b;
            }
            return null;
        }

        public NodeBase FindNodeByAddress(string CompleteIecAddress, bool FindList = false)
        {
            if (CompleteIecAddress == null)
                return null;
            string[] parts = CompleteIecAddress.Split(new char[] { '/' }, 2);
            if (parts.Length == 2)
                return FindNodeByAddress(parts[0], parts[1], FindList);
            return null;
        }

        public Iec61850State iecs { get; set; }

    }
}
