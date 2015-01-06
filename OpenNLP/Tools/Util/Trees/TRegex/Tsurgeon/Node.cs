﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNLP.Tools.Util.Trees.TRegex.Tsurgeon
{
    /* All AST nodes must implement this interface.  It provides basic
   machinery for constructing the parent and child relationships
   between nodes. */

    public interface Node
    {

        /** This method is called after the node has been made the current
          node.  It indicates that child nodes can now be added to it. */
        void JjtOpen();

        /** This method is called after all the child nodes have been
          added. */
        void JjtClose();

        /** This pair of methods are used to inform the node of its
          parent. */
        void JjtSetParent(Node n);
        Node JjtGetParent();

        /** This method tells the node to add its argument to the node's
          list of children.  */
        void JjtAddChild(Node n, int i);

        /** This method returns a child node.  The children are numbered
           from zero, left to right. */
        Node JjtGetChild(int i);

        /** Return the number of children the node has. */
        int JjtGetNumChildren();
    }
}