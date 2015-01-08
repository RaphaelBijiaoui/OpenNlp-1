﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNLP.Tools.Parser;

namespace OpenNLP.Tools.Util.Trees.TRegex.Tsurgeon
{
    public class TsurgeonParser : TsurgeonParserTreeConstants
    {
        protected JJTTsurgeonParserState jjtree = new JJTTsurgeonParserState();
        private readonly TreeFactory treeFactory = new LabeledScoredTreeFactory();

        
// TODO: this is wasteful in terms of creating TsurgeonPatternRoot.
// Should separate that out into another production
        public TsurgeonPatternRoot Root() /*throws ParseException*/
        {
/*@bgen(jjtree) Root */
            var jjtn000 = new SimpleNode(JJTROOT);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            Token name;
            TsurgeonPattern result;
            List<TsurgeonPattern> results = null;
            try
            {
                switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                {
                    case DELETE:
                    case PRUNE:
                    case RELABEL:
                    case EXCISE:
                    case INSERT:
                    case MOVE:
                    case REPLACE:
                    case CREATE_SUBTREE:
                    case ADJOIN:
                    case ADJOIN_TO_HEAD:
                    case ADJOIN_TO_FOOT:
                    case COINDEX:
                    {
                        result = Operation();
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return new TsurgeonPatternRoot(result);
                    }
                    default:
                        jj_la1[1] = jj_gen;
                        if (Jj_2_1(2))
                        {
                            Jj_consume_token(IF);
                            Jj_consume_token(EXISTS);
                            name = Jj_consume_token(NAME);
                            result = Root();
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new TsurgeonPatternRoot(new IfExistsNode(name.Image, false, result.children));
                        }
                        else if (Jj_2_2(2))
                        {
                            Jj_consume_token(IF);
                            Jj_consume_token(NOT);
                            Jj_consume_token(EXISTS);
                            name = Jj_consume_token(NAME);
                            result = Root();
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new TsurgeonPatternRoot(new IfExistsNode(name.Image, true, result.children));
                        }
                        else
                        {
                            switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                            {
                                case OPEN_BRACKET:
                                {
                                    //label_1:
                                    while (true)
                                    {
                                        Jj_consume_token(OPEN_BRACKET);
                                        result = Root();
                                        Jj_consume_token(CLOSE_BRACKET);
                                        if (results == null)
                                        {
                                            results = new List<TsurgeonPattern>();
                                        }
                                        foreach (TsurgeonPattern child in result.children)
                                        {
                                            results.Add(child);
                                        }
                                        switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                                        {
                                            case OPEN_BRACKET:
                                            {
                                                ;
                                                break;
                                            }
                                            default:
                                                jj_la1[0] = jj_gen;
                                                goto post_label_1;
                                        }
                                    }
                                    post_label_1:
                                    {
                                        jjtree.CloseNodeScope(jjtn000, true);
                                        jjtc000 = false;
                                        var array = new TsurgeonPattern[results.Count];
                                        return new TsurgeonPatternRoot(results.ToArray());
                                    }
                                }
                                default:
                                    jj_la1[2] = jj_gen;
                                    Jj_consume_token(-1);
                                    throw new ParseException();
                            }
                        }
                }
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        public TsurgeonPattern Operation() /*throws ParseException*/
        {
/*@bgen(jjtree) Operation */
            var jjtn000 = new SimpleNode(JJTOPERATION);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            TsurgeonPattern child1;
            TsurgeonPattern child2 = null;
            Token newLabel = null;
            TreeLocation loc = null;
            Token operatorToken;
            AuxiliaryTree tree = null;
            List<AuxiliaryTree> treeList = null;
            List<TsurgeonPattern> nodeSelections = null;
            Token regex;
            Token hash_int;
            try
            {
                switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                {
                    case DELETE:
                    {
                        operatorToken = Jj_consume_token(DELETE);
                        nodeSelections = NodeSelectionList(new List<TsurgeonPattern>());
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return new DeleteNode(nodeSelections);
                    }
                    case PRUNE:
                    {
                        operatorToken = Jj_consume_token(PRUNE);
                        nodeSelections = NodeSelectionList(new List<TsurgeonPattern>());
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return new PruneNode(nodeSelections);
                    }
                    case EXCISE:
                    {
                        operatorToken = Jj_consume_token(EXCISE);
                        child1 = NodeSelection();
                        child2 = NodeSelection();
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return new ExciseNode(child1, child2);
                    }
                    default:
                        jj_la1[3] = jj_gen;
                        if (Jj_2_3(3))
                        {
                            operatorToken = Jj_consume_token(RELABEL);
                            child1 = NodeSelection();
                            newLabel = Jj_consume_token(IDENTIFIER);
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new RelabelNode(child1, newLabel.Image);
                        }
                        else if (Jj_2_4(3))
                        {
                            operatorToken = Jj_consume_token(RELABEL);
                            child1 = NodeSelection();
                            newLabel = Jj_consume_token(QUOTEX);
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new RelabelNode(child1, newLabel.Image);
                        }
                        else if (Jj_2_5(3))
                        {
                            operatorToken = Jj_consume_token(RELABEL);
                            child1 = NodeSelection();
                            regex = Jj_consume_token(REGEX);
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new RelabelNode(child1, regex.Image);
                        }
                        else if (Jj_2_6(3))
                        {
                            operatorToken = Jj_consume_token(RELABEL);
                            child1 = NodeSelection();
                            newLabel = Jj_consume_token(GENERAL_RELABEL);
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new RelabelNode(child1, newLabel.Image);
                        }
                        else if (Jj_2_7(3))
                        {
                            operatorToken = Jj_consume_token(REPLACE);
                            child1 = NodeSelection();
                            child2 = NodeSelection();
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new ReplaceNode(child1, new[] {child2});
                        }
                        else if (Jj_2_8(3))
                        {
                            operatorToken = Jj_consume_token(REPLACE);
                            child1 = NodeSelection();
                            treeList = TreeList(false);
                            jjtree.CloseNodeScope(jjtn000, true);
                            jjtc000 = false;
                            return new ReplaceNode(child1, treeList);
                        }
                        else
                        {
                            switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                            {
                                case MOVE:
                                {
                                    operatorToken = Jj_consume_token(MOVE);
                                    child1 = NodeSelection();
                                    loc = Location();
                                    jjtree.CloseNodeScope(jjtn000, true);
                                    jjtc000 = false;
                                    return new MoveNode(child1, loc);
                                }
                                default:
                                    jj_la1[4] = jj_gen;
                                    if (Jj_2_9(3))
                                    {
                                        operatorToken = Jj_consume_token(INSERT);
                                        child1 = NodeSelection();
                                        loc = Location();
                                        jjtree.CloseNodeScope(jjtn000, true);
                                        jjtc000 = false;
                                        return new InsertNode(child1, loc);
                                    }
                                    else if (Jj_2_10(3))
                                    {
                                        operatorToken = Jj_consume_token(INSERT);
                                        tree = TreeRoot(false);
                                        loc = Location();
                                        jjtree.CloseNodeScope(jjtn000, true);
                                        jjtc000 = false;
                                        return new InsertNode(tree, loc);
                                    }
                                    else
                                    {
                                        switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                                        {
                                            case CREATE_SUBTREE:
                                            {
                                                operatorToken = Jj_consume_token(CREATE_SUBTREE);
                                                tree = TreeRoot(false);
                                                nodeSelections = NodeSelectionList(new List<TsurgeonPattern>());
                                                jjtree.CloseNodeScope(jjtn000, true);
                                                jjtc000 = false;
                                                if (nodeSelections.Count == 1)
                                                {
                                                    return new CreateSubtreeNode(nodeSelections[0], tree);
                                                }
                                                else if (nodeSelections.Count == 2)
                                                {
                                                    return new CreateSubtreeNode(nodeSelections[0],
                                                            nodeSelections[1], tree);
                                                }
                                                else
                                                {
                                                    throw new ParseException(
                                                                "Illegal number of nodes given to createSubtree (" +
                                                                nodeSelections.Count + ")");
                                                }
                                            }
                                            case ADJOIN:
                                            {
                                                operatorToken = Jj_consume_token(ADJOIN);
                                                tree = TreeRoot(true);
                                                child1 = NodeSelection();
                                                jjtree.CloseNodeScope(jjtn000, true);
                                                jjtc000 = false;
                                                return new AdjoinNode(tree, child1);
                                            }
                                            case ADJOIN_TO_HEAD:
                                            {
                                                operatorToken = Jj_consume_token(ADJOIN_TO_HEAD);
                                                tree = TreeRoot(true);
                                                child1 = NodeSelection();
                                                jjtree.CloseNodeScope(jjtn000, true);
                                                jjtc000 = false;
                                                return new AdjoinToHeadNode(tree, child1);
                                            }
                                            case ADJOIN_TO_FOOT:
                                            {
                                                operatorToken = Jj_consume_token(ADJOIN_TO_FOOT);
                                                tree = TreeRoot(true);
                                                child1 = NodeSelection();
                                                jjtree.CloseNodeScope(jjtn000, true);
                                                jjtc000 = false;
                                                return new AdjoinToFootNode(tree, child1);
                                            }
                                            case COINDEX:
                                            {
                                                operatorToken = Jj_consume_token(COINDEX);
                                                nodeSelections = NodeSelectionList(new List<TsurgeonPattern>());
                                                jjtree.CloseNodeScope(jjtn000, true);
                                                jjtc000 = false;
                                                return new CoindexNodes(nodeSelections.ToArray());
                                            }
                                            default:
                                                jj_la1[5] = jj_gen;
                                                Jj_consume_token(-1);
                                                throw new ParseException();
                                        }
                                    }
                            }
                        }
                }
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        public TreeLocation Location() /*throws ParseException*/
        {
/*@bgen(jjtree) Location */
            var jjtn000 = new SimpleNode(JJTLOCATION);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            Token rel;
            TsurgeonPattern child;
            try
            {
                rel = Jj_consume_token(LOCATION_RELATION);
                child = NodeSelection();
                jjtree.CloseNodeScope(jjtn000, true);
                jjtc000 = false;
                return new TreeLocation(rel.Image, child);
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        /*readonly*/

        public List<TsurgeonPattern> NodeSelectionList(List<TsurgeonPattern> l) /*throws ParseException*/
        {
/*@bgen(jjtree) NodeSelectionList */
            var jjtn000 = new SimpleNode(JJTNODESELECTIONLIST);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            TsurgeonPattern result;
            try
            {
                result = NodeSelection();
                l.Add(result);
                //label_2:
                while (true)
                {
                    switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                    {
                        case IDENTIFIER:
                        {
                            ;
                            break;
                        }
                        default:
                            jj_la1[6] = jj_gen;
                            //break label_2;
                            goto post_label_2;
                    }
                    result = NodeSelection();
                    l.Add(result);
                }
                post_label_2:
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                    jjtc000 = false;
                    return l;
                }
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

// TODO: what does this next comment mean?
// we'll also put in a way to use a SELECTION with a list of nodes.
        /*readonly*/

        public TsurgeonPattern NodeSelection() /*throws ParseException*/
        {
/*@bgen(jjtree) NodeSelection */
            var jjtn000 = new SimpleNode(JJTNODESELECTION);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            TsurgeonPattern result;
            try
            {
                result = NodeName();
                jjtree.CloseNodeScope(jjtn000, true);
                jjtc000 = false;
                return result;
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        /*readonly */

        public TsurgeonPattern NodeName() /*throws ParseException */
        {
/*@bgen(jjtree) NodeName */
            var jjtn000 = new SimpleNode(JJTNODENAME);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            Token t;
            try
            {
                t = Jj_consume_token(IDENTIFIER);
                jjtree.CloseNodeScope(jjtn000, true);
                jjtc000 = false;
                return new FetchNode(t.Image);
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        /*readonly */

        public List<AuxiliaryTree> TreeList(bool requiresFoot) /*throws ParseException */
        {
/*@bgen(jjtree) TreeList */
            var jjtn000 = new SimpleNode(JJTTREELIST);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            var trees = new List<AuxiliaryTree>();
            AuxiliaryTree tree;
            try
            {
                tree = TreeRoot(requiresFoot);
                trees.Add(tree);
                //label_3:
                while (true)
                {
                    switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                    {
                        case IDENTIFIER:
                        case TREE_NODE_TERMINAL_LABEL:
                        case TREE_NODE_NONTERMINAL_LABEL:
                        {
                            ;
                            break;
                        }
                        default:
                            jj_la1[7] = jj_gen;
                            //break label_3;
                            goto post_label_3;
                    }
                    tree = TreeRoot(requiresFoot);
                    trees.Add(tree);
                }
                post_label_3:
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                    jjtc000 = false;
                    return trees;
                }
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

// the argument says whether there must be a foot node on the aux tree.
        /*readonly*/

        public AuxiliaryTree TreeRoot(bool requiresFoot) /*throws ParseException */
        {
/*@bgen(jjtree) TreeRoot */
            var jjtn000 = new SimpleNode(JJTTREEROOT);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            Tree t;
            try
            {
                t = TreeNode();
                jjtree.CloseNodeScope(jjtn000, true);
                jjtc000 = false;
                return new AuxiliaryTree(t, requiresFoot);
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        /*readonly*/

        public Tree TreeNode() /*throws ParseException */
        {
/*@bgen(jjtree) TreeNode */
            var jjtn000 = new SimpleNode(JJTTREENODE);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            Token label;
            List<Tree> dtrs = null;
            try
            {
                switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                {
                    case TREE_NODE_NONTERMINAL_LABEL:
                    {
                        label = Jj_consume_token(TREE_NODE_NONTERMINAL_LABEL);
                        dtrs = TreeDtrs(new List<Tree>());
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return treeFactory.NewTreeNode(label.Image.Substring(1), dtrs);
                    }
                    case TREE_NODE_TERMINAL_LABEL:
                    {
                        label = Jj_consume_token(TREE_NODE_TERMINAL_LABEL);
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return treeFactory.NewTreeNode(label.Image, new List<Tree>());
                    }
                    case IDENTIFIER:
                    {
                        label = Jj_consume_token(IDENTIFIER);
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return treeFactory.NewTreeNode(label.Image, new List<Tree>());
                    }
                    default:
                        jj_la1[8] = jj_gen;
                        Jj_consume_token(-1);
                        throw new ParseException();
                }
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        /*readonly*/

        public List<Tree> TreeDtrs(List<Tree> dtrs) /*throws ParseException */
        {
/*@bgen(jjtree) TreeDtrs */
            var jjtn000 = new SimpleNode(JJTTREEDTRS);
            bool jjtc000 = true;
            jjtree.OpenNodeScope(jjtn000);
            Tree tree;
            try
            {
                switch ((jj_ntk == -1) ? Jj_ntk_f() : jj_ntk)
                {
                    case IDENTIFIER:
                    case TREE_NODE_TERMINAL_LABEL:
                    case TREE_NODE_NONTERMINAL_LABEL:
                    {
                        tree = TreeNode();
                        TreeDtrs(dtrs);
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        dtrs.Insert(0, tree);
                        return dtrs;
                    }
                    case CLOSE_PAREN:
                    {
                        Jj_consume_token(CLOSE_PAREN);
                        jjtree.CloseNodeScope(jjtn000, true);
                        jjtc000 = false;
                        return dtrs;
                    }
                    default:
                        jj_la1[9] = jj_gen;
                        Jj_consume_token(-1);
                        throw new ParseException();
                }
            }
            catch (Exception jjte000)
            {
                if (jjtc000)
                {
                    jjtree.ClearNodeScope(jjtn000);
                    jjtc000 = false;
                }
                else
                {
                    jjtree.PopNode();
                }
                if (jjte000 is SystemException)
                {
                    throw jjte000;
                }
                if (jjte000 is ParseException)
                {
                    throw jjte000;
                }
                throw jjte000;
            }
            finally
            {
                if (jjtc000)
                {
                    jjtree.CloseNodeScope(jjtn000, true);
                }
            }
            //throw new Error("Missing return statement in function");
        }

        private bool Jj_2_1(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_1();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(0, xla);
            }
        }

        private bool Jj_2_2(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_2();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(1, xla);
            }
        }

        private bool Jj_2_3(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_3();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(2, xla);
            }
        }

        private bool Jj_2_4(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_4();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(3, xla);
            }
        }

        private bool Jj_2_5(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_5();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(4, xla);
            }
        }

        private bool Jj_2_6(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_6();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(5, xla);
            }
        }

        private bool Jj_2_7(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_7();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(6, xla);
            }
        }

        private bool Jj_2_8(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_8();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(7, xla);
            }
        }

        private bool Jj_2_9(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_9();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(8, xla);
            }
        }

        private bool Jj_2_10(int xla)
        {
            jj_la = xla;
            jj_lastpos = jj_scanpos = token;
            try
            {
                return !Jj_3_10();
            }
            catch (LookaheadSuccess ls)
            {
                return true;
            }
            finally
            {
                Jj_save(9, xla);
            }
        }

        private bool Jj_3R_15()
        {
            if (Jj_scan_token(CLOSE_PAREN)) return true;
            return false;
        }

        private bool Jj_3R_8()
        {
            if (Jj_scan_token(IDENTIFIER)) return true;
            return false;
        }

        private bool Jj_3R_14()
        {
            if (Jj_3R_9()) return true;
            return false;
        }

        private bool Jj_3R_13()
        {
            Token xsp;
            xsp = jj_scanpos;
            if (Jj_3R_14())
            {
                jj_scanpos = xsp;
                if (Jj_3R_15()) return true;
            }
            return false;
        }

        private bool Jj_3R_4()
        {
            if (Jj_3R_8()) return true;
            return false;
        }

        private bool Jj_3R_12()
        {
            if (Jj_scan_token(IDENTIFIER)) return true;
            return false;
        }

        private bool Jj_3_10()
        {
            if (Jj_scan_token(INSERT)) return true;
            if (Jj_3R_7()) return true;
            if (Jj_3R_6()) return true;
            return false;
        }

        private bool Jj_3R_11()
        {
            if (Jj_scan_token(TREE_NODE_TERMINAL_LABEL)) return true;
            return false;
        }

        private bool Jj_3_9()
        {
            if (Jj_scan_token(INSERT)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_3R_6()) return true;
            return false;
        }

        private bool Jj_3R_9()
        {
            Token xsp;
            xsp = jj_scanpos;
            if (Jj_3R_10())
            {
                jj_scanpos = xsp;
                if (Jj_3R_11())
                {
                    jj_scanpos = xsp;
                    if (Jj_3R_12()) return true;
                }
            }
            return false;
        }

        private bool Jj_3R_10()
        {
            if (Jj_scan_token(TREE_NODE_NONTERMINAL_LABEL)) return true;
            if (Jj_3R_13()) return true;
            return false;
        }

        private bool Jj_3_8()
        {
            if (Jj_scan_token(REPLACE)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_3R_5()) return true;
            return false;
        }

        private bool Jj_3_7()
        {
            if (Jj_scan_token(REPLACE)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_3R_4()) return true;
            return false;
        }

        private bool Jj_3R_7()
        {
            if (Jj_3R_9()) return true;
            return false;
        }

        private bool Jj_3_2()
        {
            if (Jj_scan_token(IF)) return true;
            if (Jj_scan_token(NOT)) return true;
            return false;
        }

        private bool Jj_3_6()
        {
            if (Jj_scan_token(RELABEL)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_scan_token(GENERAL_RELABEL)) return true;
            return false;
        }

        private bool Jj_3_1()
        {
            if (Jj_scan_token(IF)) return true;
            if (Jj_scan_token(EXISTS)) return true;
            return false;
        }

        private bool Jj_3_5()
        {
            if (Jj_scan_token(RELABEL)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_scan_token(REGEX)) return true;
            return false;
        }

        private bool Jj_3_4()
        {
            if (Jj_scan_token(RELABEL)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_scan_token(QUOTEX)) return true;
            return false;
        }

        private bool Jj_3R_6()
        {
            if (Jj_scan_token(LOCATION_RELATION)) return true;
            return false;
        }

        private bool Jj_3_3()
        {
            if (Jj_scan_token(RELABEL)) return true;
            if (Jj_3R_4()) return true;
            if (Jj_scan_token(IDENTIFIER)) return true;
            return false;
        }

        private bool Jj_3R_5()
        {
            if (Jj_3R_7()) return true;
            return false;
        }

        /** Generated Token Manager. */
        public TsurgeonParserTokenManager token_source;
        private SimpleCharStream jj_input_stream;
        /** Current token. */
        public Token token;
        /** Next token. */
        public Token jj_nt;
        private int jj_ntk;
        private Token jj_scanpos, jj_lastpos;
        private int jj_la;
        private int jj_gen;
        private readonly int[] jj_la1 = new int[10];
        //private static int[] jj_la1_0;
        private static readonly int[] jj_la1_0 = new int[]
        {
            0x20, 0x1ffe00, 0x20, 0x1600, 0x4000, 0x1f0000, 0x2000000, unchecked ((int) 0xc2000000),
            unchecked ((int) 0xc2000000), unchecked ((int) 0xc2000000),
        };

        //private static int[] jj_la1_1;
        private static readonly int[] jj_la1_1 = new int[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1,};

        /*private static 
        {
            jj_la1_init_0();
            jj_la1_init_1();
        }*/

        /*private static void jj_la1_init_0()
        {
            jj_la1_0 = new int[]
            {0x20, 0x1ffe00, 0x20, 0x1600, 0x4000, 0x1f0000, 0x2000000, 0xc2000000, 0xc2000000, 0xc2000000,};
        }*/

        /*private static void jj_la1_init_1()
        {
            jj_la1_1 = new int[] {0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x1,};
        }*/

        private readonly JjCalls[] jj_2_rtns = new JjCalls[10];
        private bool jj_rescan = false;
        private int jj_gc = 0;

        /** Constructor with InputStream. */

        public TsurgeonParser( /*java.io.InputStream*/ Stream stream) :
            this(stream, null)
        {
        }

        /** Constructor with InputStream and supplied encoding */

        public TsurgeonParser( /*java.io.InputStream*/ Stream stream, string encoding)
        {
            try
            {
                jj_input_stream = new SimpleCharStream(stream, encoding, 1, 1);
            }
            catch ( /*java.io.UnsupportedEncodingException*/Exception e)
            {
                throw new SystemException(e.Message);
            }
            token_source = new TsurgeonParserTokenManager(jj_input_stream);
            token = new Token();
            jj_ntk = -1;
            jj_gen = 0;
            for (int i = 0; i < 10; i++) jj_la1[i] = -1;
            for (int i = 0; i < jj_2_rtns.Length; i++) jj_2_rtns[i] = new JjCalls();
        }

        /** Reinitialise. */
        /*public void ReInit(java.io.InputStream stream) {
     ReInit(stream, null);
  }*/
        /** Reinitialise. */
        /*public void ReInit(java.io.InputStream stream, string encoding) {
    try { jj_input_stream.ReInit(stream, encoding, 1, 1); } catch(java.io.UnsupportedEncodingException e) { throw new SystemException(e); }
    token_source.ReInit(jj_input_stream);
    token = new Token();
    jj_ntk = -1;
    jjtree.reset();
    jj_gen = 0;
    for (int i = 0; i < 10; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }*/

        /** Constructor. */

        public TsurgeonParser(TextReader stream)
        {
            jj_input_stream = new SimpleCharStream(stream, 1, 1);
            token_source = new TsurgeonParserTokenManager(jj_input_stream);
            token = new Token();
            jj_ntk = -1;
            jj_gen = 0;
            for (int i = 0; i < 10; i++) jj_la1[i] = -1;
            for (int i = 0; i < jj_2_rtns.Length; i++) jj_2_rtns[i] = new JjCalls();
        }

        /** Reinitialise. */
        /*public void ReInit(java.io.Reader stream) {
    jj_input_stream.ReInit(stream, 1, 1);
    token_source.ReInit(jj_input_stream);
    token = new Token();
    jj_ntk = -1;
    jjtree.reset();
    jj_gen = 0;
    for (int i = 0; i < 10; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }*/

        /** Constructor with generated Token Manager. */
        /*public TsurgeonParser(TsurgeonParserTokenManager tm) {
    token_source = tm;
    token = new Token();
    jj_ntk = -1;
    jj_gen = 0;
    for (int i = 0; i < 10; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }*/

        /** Reinitialise. */
        /*public void ReInit(TsurgeonParserTokenManager tm) {
    token_source = tm;
    token = new Token();
    jj_ntk = -1;
    jjtree.reset();
    jj_gen = 0;
    for (int i = 0; i < 10; i++) jj_la1[i] = -1;
    for (int i = 0; i < jj_2_rtns.length; i++) jj_2_rtns[i] = new JJCalls();
  }s*/

        private Token Jj_consume_token(int kind) /*throws ParseException*/
        {
            Token oldToken;
            if ((oldToken = token).Next != null) token = token.Next;
            else token = token.Next = token_source.GetNextToken();
            jj_ntk = -1;
            if (token.Kind == kind)
            {
                jj_gen++;
                if (++jj_gc > 100)
                {
                    jj_gc = 0;
                    for (int i = 0; i < jj_2_rtns.Length; i++)
                    {
                        JjCalls c = jj_2_rtns[i];
                        while (c != null)
                        {
                            if (c.gen < jj_gen) c.first = null;
                            c = c.next;
                        }
                    }
                }
                return token;
            }
            token = oldToken;
            jj_kind = kind;
            throw GenerateParseException();
        }

        //@SuppressWarnings("serial")
        private sealed class LookaheadSuccess : SystemException
        {
        }

        private readonly LookaheadSuccess jj_ls = new LookaheadSuccess();

        private bool Jj_scan_token(int kind)
        {
            if (jj_scanpos == jj_lastpos)
            {
                jj_la--;
                if (jj_scanpos.Next == null)
                {
                    jj_lastpos = jj_scanpos = jj_scanpos.Next = token_source.GetNextToken();
                }
                else
                {
                    jj_lastpos = jj_scanpos = jj_scanpos.Next;
                }
            }
            else
            {
                jj_scanpos = jj_scanpos.Next;
            }
            if (jj_rescan)
            {
                int i = 0;
                Token tok = token;
                while (tok != null && tok != jj_scanpos)
                {
                    i++;
                    tok = tok.Next;
                }
                if (tok != null) Jj_add_error_token(kind, i);
            }
            if (jj_scanpos.Kind != kind) return true;
            if (jj_la == 0 && jj_scanpos == jj_lastpos) throw jj_ls;
            return false;
        }


/** Get the next Token. */
        /*readonly*/

        public Token GetNextToken()
        {
            if (token.Next != null) token = token.Next;
            else token = token.Next = token_source.GetNextToken();
            jj_ntk = -1;
            jj_gen++;
            return token;
        }

/** Get the specific Token. */
        /*readonly*/

        /*public Token getToken(int index)
        {
            Token t = token;
            for (int i = 0; i < index; i++)
            {
                if (t.next != null) t = t.next;
                else t = t.next = token_source.getNextToken();
            }
            return t;
        }*/

        private int Jj_ntk_f()
        {
            if ((jj_nt = token.Next) == null)
                return (jj_ntk = (token.Next = token_source.GetNextToken()).Kind);
            else
                return (jj_ntk = jj_nt.Kind);
        }

        private readonly List<int[]> jj_expentries = new List<int[]>();
        private int[] jj_expentry;
        private int jj_kind = -1;
        private readonly int[] jj_lasttokens = new int[100];
        private int jj_endpos;

        private void Jj_add_error_token(int kind, int pos)
        {
            if (pos >= 100) return;
            if (pos == jj_endpos + 1)
            {
                jj_lasttokens[jj_endpos++] = kind;
            }
            else if (jj_endpos != 0)
            {
                jj_expentry = new int[jj_endpos];
                for (int i = 0; i < jj_endpos; i++)
                {
                    jj_expentry[i] = jj_lasttokens[i];
                }
                /*jj_entries_loop:
                for (java.util.Iterator < ? > it = jj_expentries.iterator();
                it.hasNext();)*/
                foreach (var jjExpentry in jj_expentries)
                {
                    //int[] oldentry = (int[]) (it.next());
                    int[] oldentry = jjExpentry;
                    if (oldentry.Length == jj_expentry.Length)
                    {
                        for (int i = 0; i < jj_expentry.Length; i++)
                        {
                            if (oldentry[i] != jj_expentry[i])
                            {
                                //continue jj_entries_loop;
                                goto post_jj_entries_loop;
                            }
                        }
                        jj_expentries.Add(jj_expentry);
                        //break jj_entries_loop;
                        goto post_jj_entries_loop;
                    }
                }
                post_jj_entries_loop:
                {
                    if (pos != 0) jj_lasttokens[(jj_endpos = pos) - 1] = kind;
                }
            }
        }

        /** Generate ParseException. */

        public ParseException GenerateParseException()
        {
            jj_expentries.Clear();
            bool[] la1tokens = new bool[33];
            if (jj_kind >= 0)
            {
                la1tokens[jj_kind] = true;
                jj_kind = -1;
            }
            for (int i = 0; i < 10; i++)
            {
                if (jj_la1[i] == jj_gen)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        if ((jj_la1_0[i] & (1 << j)) != 0)
                        {
                            la1tokens[j] = true;
                        }
                        if ((jj_la1_1[i] & (1 << j)) != 0)
                        {
                            la1tokens[32 + j] = true;
                        }
                    }
                }
            }
            for (int i = 0; i < 33; i++)
            {
                if (la1tokens[i])
                {
                    jj_expentry = new int[1];
                    jj_expentry[0] = i;
                    jj_expentries.Add(jj_expentry);
                }
            }
            jj_endpos = 0;
            Jj_rescan_token();
            Jj_add_error_token(0, 0);
            var exptokseq = new int[jj_expentries.Count][];
            for (int i = 0; i < jj_expentries.Count; i++)
            {
                exptokseq[i] = jj_expentries[i];
            }
            return new ParseException(token, exptokseq, tokenImage);
        }

        /** Enable tracing. */
        /*readonly*/

        public void Enable_tracing()
        {
        }

        /** Disable tracing. */
        /*readonly */

        public void Disable_tracing()
        {
        }

        private void Jj_rescan_token()
        {
            jj_rescan = true;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    JjCalls p = jj_2_rtns[i];
                    do
                    {
                        if (p.gen > jj_gen)
                        {
                            jj_la = p.arg;
                            jj_lastpos = jj_scanpos = p.first;
                            switch (i)
                            {
                                case 0:
                                    Jj_3_1();
                                    break;
                                case 1:
                                    Jj_3_2();
                                    break;
                                case 2:
                                    Jj_3_3();
                                    break;
                                case 3:
                                    Jj_3_4();
                                    break;
                                case 4:
                                    Jj_3_5();
                                    break;
                                case 5:
                                    Jj_3_6();
                                    break;
                                case 6:
                                    Jj_3_7();
                                    break;
                                case 7:
                                    Jj_3_8();
                                    break;
                                case 8:
                                    Jj_3_9();
                                    break;
                                case 9:
                                    Jj_3_10();
                                    break;
                            }
                        }
                        p = p.next;
                    } while (p != null);
                }
                catch (LookaheadSuccess ls)
                {
                }
            }
            jj_rescan = false;
        }

        private void Jj_save(int index, int xla)
        {
            JjCalls p = jj_2_rtns[index];
            while (p.gen > jj_gen)
            {
                if (p.next == null)
                {
                    p = p.next = new JjCalls();
                    break;
                }
                p = p.next;
            }
            p.gen = jj_gen + xla - jj_la;
            p.first = token;
            p.arg = xla;
        }

        private class JjCalls
        {
            public int gen;
            public Token first;
            public int arg;
            public JjCalls next;
        }
    }
}