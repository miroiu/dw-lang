using DwLang.Language.Expressions;
using System;
using System.Collections.Generic;

namespace DwLang.Editor
{
    public static class DwLangTransformTree
    {
        public static DwLangTreeNode Transform(Expression node)
        {
            switch (node)
            {
                case AssignmentExpression assignment:
                    return CreateNode(node, new DwLangTreeNode[]
                    {
                        Transform(assignment.Identifier),
                        Transform(assignment.Initializer)
                    });

                case BinaryExpression binary:
                    return CreateNode(node, new DwLangTreeNode[]
                    {
                        Transform(binary.Left),
                        Transform(binary.Right)
                    });

                case GroupingExpression grouping:
                    return CreateNode(node, new DwLangTreeNode[]
                    {
                        Transform(grouping.Inner)
                    });

                case SetPrecisionExpression setPrecision:
                    return CreateNode(node, new DwLangTreeNode[]
                    {
                        Transform(setPrecision.Precision)
                    });

                case UnaryExpression unary:
                    return CreateNode(node, new DwLangTreeNode[]
                    {
                        Transform(unary.Operand)
                    });

                case VarArgsExpression varArgs:
                    var children = new List<DwLangTreeNode>();
                    foreach (var arg in varArgs.Arguments)
                    {
                        children.Add(Transform(arg));
                    }
                    return CreateNode(node, children.ToArray());

                case VariableDeclarationExpression variable:
                    return CreateNode(node, new DwLangTreeNode[]
                    {
                        Transform(variable.Identifier),
                        Transform(variable.Initializer)
                    });

                case CommandExpression _:
                case ConstantExpression _:
                case EmptyExpression _:
                case IdentifierExpression _:
                default:
                    return CreateNode(node, Array.Empty<DwLangTreeNode>());
            }
        }

        private static DwLangTreeNode CreateNode(Expression expr, DwLangTreeNode[] children)
            => new DwLangTreeNode
            {
                Type = expr.GetType().Name,
                Column = expr.Token.Column,
                Line = expr.Token.Line,
                Text = expr.Token.Text,
                Children = children
            };
    }
}
