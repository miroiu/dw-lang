using DwLang.Language;
using DwLang.Language.Expressions;
using DwLang.Language.Interpreter;
using DwLang.Language.Lexer;
using DwLang.Language.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace DwLang.Editor
{
    public class DwLangApp : DwLangObservable
    {
        public DwLangApp()
        {
            RunCommand = new DwLangCommand(Run);
            Console = new DwLangConsole();
        }

        public DwLangConsole Console { get; }
        public ICommand RunCommand { get; }

        private IEnumerable<DwLangTreeNode> _nodes;
        public IEnumerable<DwLangTreeNode> Nodes
        {
            get => _nodes;
            set => SetProperty(ref _nodes, value);
        }

        private DwLangTreeNode _selectedNode;
        public DwLangTreeNode SelectedNode
        {
            get => _selectedNode;
            set => SetProperty(ref _selectedNode, value);
        }

        private void Run()
        {
            try
            {
                var code = Console.ReadLine();
                ExecuteCode(code);
                ShowExpressionTree(code);
            }
            catch (DwLangLexerException lexEx)
            {
                Console.WriteLine($"[{lexEx.Line}, {lexEx.Column}]: {lexEx.Message}");
            }
            catch (DwLangExecutionException dwLangExx)
            {
                Console.WriteLine($"[{dwLangExx.Expression.Token.Line}, {dwLangExx.Expression.Token.Column}]: {dwLangExx.Message}");
            }
            catch (DwLangParserException dwLangParserEx)
            {
                Console.WriteLine($"[{dwLangParserEx.Token.Line}, {dwLangParserEx.Token.Column}]: {dwLangParserEx.Message}");
            }
            catch (DwLangException dwLangEx)
            {
                Console.WriteLine(dwLangEx.ToString());
            }
            catch
            {
                Console.WriteLine("Catastrophic failure!");
            }
        }

        private void ShowExpressionTree(string code)
        {
            var preLexer = new DwLangPreLexer(code);
            var source = preLexer.Sanitize();
            var lexer = new DwLangLexer(source);
            var parser = new DwLangParser(lexer);

            List<Expression> expressions = new List<Expression>();
            while (parser.HasNext)
            {
                var expr = parser.Next();
                expressions.Add(expr);
            }

            Nodes = expressions.Select(expr => DwLangTransformTree.Transform(expr));
        }

        private void ExecuteCode(string code)
        {
            var preLexer = new DwLangPreLexer(code);
            var source = preLexer.Sanitize();
            var lexer = new DwLangLexer(source);
            var parser = new DwLangParser(lexer);

            var interpreter = new DwLangInterpreter(Console);
            interpreter.Run(parser);
        }
    }
}
