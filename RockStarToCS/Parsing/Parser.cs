using System;
using System.Collections.Generic;

/* Warning: this file is generated, changes made here may be be overwritten. */

namespace RockStarToCS.Parsing
{
    class Parser
    {
        private int TokenIndex;
        private List<Token> Tokens;
        public int HighestLine { get; private set; }

        public ParseNode Parse(List<Token> Tokens)
        {
            this.Tokens = Tokens;
            TokenIndex = 0;
            HighestLine = 0;
            ParseNode retVal = null;
            if((retVal = Matches_root()) != null)
            {
                return retVal;
            }
            return null;
        }

        private ParseNode Matches_root()
        {
            //root => rstarprog
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches_rstarprog()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_rstarprog_1()
        {
            //rstarprog => EOF
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("EOF")) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList();
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_rstarprog(int RuleIndex=0)
        {
            ParseNode matches = null;
            //rstarprog => EOF
            if(RuleIndex != 1 && (matches = Matches_rstarprog_1()) != null)
            {
                return matches;
            }
            //rstarprog => blk
            if(RuleIndex != 2 && (matches = Matches_blk()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_blk()
        {
            //blk => stmtlst eline
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_stmtlst()) != null && (matches[1] = Matches_eline()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_stmtlst()
        {
            //stmtlst => _stmtlst
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches__stmtlst()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__stmtlst_1()
        {
            //_stmtlst => stmt _stmtlst
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_stmt()) != null && (matches[1] = Matches__stmtlst()) != null)
            {
                Func<object[], ParseNode> f = x => { var lst = x[1] as ParseNodeList; lst.Add(x[0] as ParseNode); return lst; };
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__stmtlst(int RuleIndex=0)
        {
            ParseNode matches = null;
            //_stmtlst => stmt _stmtlst
            if(RuleIndex != 1 && (matches = Matches__stmtlst_1()) != null)
            {
                return matches;
            }
            //_stmtlst => stmt
            if(RuleIndex != 2 && (matches = Matches_stmt()) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList(x[0] as ParseNode);
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_stmt()
        {
            //stmt => ass
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches_ass()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_1()
        {
            //ass => var IS NULL
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = TokenMatches("NULL")) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode(x[1] as Token, new NullParseNode(x[2] as Token), x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_2()
        {
            //ass => var IS bool
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = Matches_bool()) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode(x[1] as Token, x[2] as ParseNode, x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass_3()
        {
            //ass => var IS wrdlst
            int ti = TokenIndex;
            object[] matches = new object[3];
            if((matches[0] = Matches_var()) != null && (matches[1] = TokenMatches("IS")) != null && (matches[2] = Matches_wrdlst()) != null)
            {
                Func<object[], ParseNode> f = x => new AssignmentParseNode(x[1] as Token, x[2] as ParseNode, x[0] as VariableParseNode);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_ass(int RuleIndex=0)
        {
            ParseNode matches = null;
            //ass => var IS NULL
            if(RuleIndex != 1 && (matches = Matches_ass_1()) != null)
            {
                return matches;
            }
            //ass => var IS bool
            if(RuleIndex != 2 && (matches = Matches_ass_2()) != null)
            {
                return matches;
            }
            //ass => var IS wrdlst
            if(RuleIndex != 3 && (matches = Matches_ass_3()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_var_1()
        {
            //var => CVARSP WORD
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("CVARSP")) != null && (matches[1] = TokenMatches("WORD")) != null)
            {
                Func<object[], ParseNode> f = x => new VariableParseNode(x[0] as Token, (x[1] as Token).Value, false);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_var_2()
        {
            //var => PVAR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("PVAR")) != null)
            {
                Func<object[], ParseNode> f = x => new VariableParseNode(x[0] as Token, (x[0] as Token).Value, true);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_var_3()
        {
            //var => LVAR
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("LVAR")) != null)
            {
                Func<object[], ParseNode> f = x => new VariableParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_var(int RuleIndex=0)
        {
            ParseNode matches = null;
            //var => CVARSP WORD
            if(RuleIndex != 1 && (matches = Matches_var_1()) != null)
            {
                return matches;
            }
            //var => PVAR
            if(RuleIndex != 2 && (matches = Matches_var_2()) != null)
            {
                return matches;
            }
            //var => LVAR
            if(RuleIndex != 3 && (matches = Matches_var_3()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_bool_1()
        {
            //bool => TRUE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("TRUE")) != null)
            {
                Func<object[], ParseNode> f = x => new BooleanParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_bool_2()
        {
            //bool => FALSE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("FALSE")) != null)
            {
                Func<object[], ParseNode> f = x => new BooleanParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_bool(int RuleIndex=0)
        {
            ParseNode matches = null;
            //bool => TRUE
            if(RuleIndex != 1 && (matches = Matches_bool_1()) != null)
            {
                return matches;
            }
            //bool => FALSE
            if(RuleIndex != 2 && (matches = Matches_bool_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_wrdlst()
        {
            //wrdlst => _wrdlst
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches__wrdlst()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__wrdlst_1()
        {
            //_wrdlst => wrd _wrdlst
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = Matches_wrd()) != null && (matches[1] = Matches__wrdlst()) != null)
            {
                Func<object[], ParseNode> f = x => { var lst = x[1] as ParseNodeList; lst.Add(x[0] as ParseNode); return lst; };
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__wrdlst(int RuleIndex=0)
        {
            ParseNode matches = null;
            //_wrdlst => wrd _wrdlst
            if(RuleIndex != 1 && (matches = Matches__wrdlst_1()) != null)
            {
                return matches;
            }
            //_wrdlst => wrd
            if(RuleIndex != 2 && (matches = Matches_wrd()) != null)
            {
                Func<object[], ParseNode> f = x => new ParseNodeList(x[0] as ParseNode);
                return f(new object[]{ matches });
            }
            return null;
        }

        private ParseNode Matches_wrd_1()
        {
            //wrd => WORD
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("WORD")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd_2()
        {
            //wrd => CVARSP
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("CVARSP")) != null)
            {
                Func<object[], ParseNode> f = x => new WordParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches_wrd(int RuleIndex=0)
        {
            ParseNode matches = null;
            //wrd => WORD
            if(RuleIndex != 1 && (matches = Matches_wrd_1()) != null)
            {
                return matches;
            }
            //wrd => CVARSP
            if(RuleIndex != 2 && (matches = Matches_wrd_2()) != null)
            {
                return matches;
            }
            return null;
        }

        private ParseNode Matches_eline()
        {
            //eline => _eline
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = Matches__eline()) != null)
            {
                Func<object[], ParseNode> f = x => x[0] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__eline_1()
        {
            //_eline => ELINE _eline
            int ti = TokenIndex;
            object[] matches = new object[2];
            if((matches[0] = TokenMatches("ELINE")) != null && (matches[1] = Matches__eline()) != null)
            {
                Func<object[], ParseNode> f = x => x[1] as ParseNode;
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__eline_2()
        {
            //_eline => ELINE
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("ELINE")) != null)
            {
                Func<object[], ParseNode> f = x => new EmptyLineParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__eline_3()
        {
            //_eline => EOF
            int ti = TokenIndex;
            object[] matches = new object[1];
            if((matches[0] = TokenMatches("EOF")) != null)
            {
                Func<object[], ParseNode> f = x => new EmptyLineParseNode(x[0] as Token);
                return f(matches);
            }
            TokenIndex = ti;
            return null;
        }

        private ParseNode Matches__eline(int RuleIndex=0)
        {
            ParseNode matches = null;
            //_eline => ELINE _eline
            if(RuleIndex != 1 && (matches = Matches__eline_1()) != null)
            {
                return matches;
            }
            //_eline => ELINE
            if(RuleIndex != 2 && (matches = Matches__eline_2()) != null)
            {
                return matches;
            }
            //_eline => EOF
            if(RuleIndex != 3 && (matches = Matches__eline_3()) != null)
            {
                return matches;
            }
            return null;
        }

        private Token TokenMatches(string TestString)
        {
            if(TokenIndex < Tokens.Count && Tokens[TokenIndex].Name == TestString)
            {
                Token t = Tokens[TokenIndex++];
                if(t.LineNumber > HighestLine)
                {
                    HighestLine = t.LineNumber;
                }
                return t;
            }
            return null;
        }

    }

    class Token
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int LineNumber { get; set; }

        public Token(string Name, string Value)
        {
            this.Name = Name;
            this.Value = Value;
            LineNumber = 0;
        }

        public Token(string Name, string Value, int LineNumber) : this(Name, Value) 
        {
            this.LineNumber = LineNumber;
        }

        public override string ToString()
        {
            return Name + ": " + Value;
        }
    }
}
