﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RockStarToCS.Compile;
using RockStarToCS.Interpreter;

namespace RockStarToCS.Parsing.ParseNodes
{
    class DivisionParseNode : ParseNode
    {
        public ParseNode LHS { get; set; }
        public ParseNode RHS { get; set; }

        public DivisionParseNode(Token T, ParseNode LHS, ParseNode RHS) : base(T)
        {
            this.LHS = LHS;
            this.RHS = RHS;
        }

        public override CSResult BuildToCS(BuildEnvironment Env)
        {
            throw new NotImplementedException();
        }

        public override InterpreterResult Interpret(InterpreterEnvironment Env)
        {
            InterpreterResult lhsResult = LHS.Interpret(Env);
            InterpreterResult rhsResult = RHS.Interpret(Env);
            if (lhsResult.Type == InterpreterVariableType.Undefined || rhsResult.Type == InterpreterVariableType.Undefined ||
                lhsResult.Value == null || rhsResult.Value == null)
            {
                throw new InterpreterException("Unable to divide mysterious", T);
            }
            decimal? lhs = InterpreterVariable.GetNumericValueFor(lhsResult.Value);
            decimal? rhs = InterpreterVariable.GetNumericValueFor(rhsResult.Value);
            if (!lhs.HasValue || !rhs.HasValue)
            {
                throw new InterpreterException("Unable to divide mysterious", T);
            }
            if(rhs.Value == 0)
            {
                throw new InterpreterException("Divide by zero error", T);
            }
            return new InterpreterResult() { Type = InterpreterVariableType.Numeric, Value = lhs.Value / rhs.Value };
        }
    }
}
