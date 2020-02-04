using System;
using Deveel.Math;
using DwLang.Language.Expressions;
using DwLang.Language.Interpreter;
using DwLang.Tests.Mocks;
using NUnit.Framework;

namespace DwLang.Tests
{
    public class InterpreterTests
    {
        private ExecutionContext _ctx;
        private MockOutputStream _out;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

        }

        [SetUp]
        public void Setup()
        {
            _out = new MockOutputStream();
            _ctx = new ExecutionContext(_out);  
        }

        [Test, Order(0)]
        public void VariableDeclaration_Declare_Should_Pass()
        {
            var evaluator = new VariableDeclarationEvaluator();
            var input = new VariableDeclarationExpression(new IdentifierExpression("aa"), null);
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            var currentValue = _ctx.Get("aa", input);
            Assert.AreEqual(currentValue, null);
        }

        [Test, Order(1)]
        public void Assignment_Assign_Constant_To_Declared_Var_Should_Pass()
        {
            VariableDeclaration_Declare_Should_Pass();
            var evaluator = new AssignmentEvaluator();
            var value = new BigDecimal(1);
            var input = new AssignmentExpression(new IdentifierExpression("aa"), new ConstantExpression(value));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            var currentValue = _ctx.Get("aa", input);
            Assert.AreEqual(value, currentValue);
        }

        [Test, Order(1)]
        public void Assignment_Assign_Constant_To_Undeclared_Var_Should_Fail()
        {
            var evaluator = new AssignmentEvaluator();
            var value = new BigDecimal(1);
            var input = new AssignmentExpression(new IdentifierExpression("bb"), new ConstantExpression(value));
            try
            {
                var result = evaluator.Evaluate(input, _ctx);
                Assert.Fail();
            } catch
            {
                Assert.Pass();
            }
        }

        [Test, Order(2)]
        public void BinaryExpression_Sum_Should_Pass()
        {
            GenerateVar("za", BigDecimal.One);
            GenerateVar("zb", BigDecimal.One);
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new IdentifierExpression("za"),
                BinaryOperatorType.Plus,
                new IdentifierExpression("zb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(2));
        }

        [Test, Order(2)]
        public void BinaryExpression_Division_Should_Pass()
        {
            GenerateVar("xa", new BigDecimal(20));
            GenerateVar("xb", new BigDecimal(5));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new IdentifierExpression("xa"),
                BinaryOperatorType.Divide,
                new IdentifierExpression("xb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(4));
        }

        [Test, Order(2)]
        public void BinaryExpression_Multiply_Should_Pass()
        {
            GenerateVar("xxa", new BigDecimal(5));
            GenerateVar("xxb", new BigDecimal(5));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new IdentifierExpression("xxa"),
                BinaryOperatorType.Multiply,
                new IdentifierExpression("xxb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(25));
        }

        [Test, Order(2)]
        public void BinaryExpression_Minus_Should_Pass()
        {
            GenerateVar("xxxa", new BigDecimal(5));
            GenerateVar("xxxb", new BigDecimal(5));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new IdentifierExpression("xxxa"),
                BinaryOperatorType.Minus,
                new IdentifierExpression("xxxb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(0));
        }

        [Test, Order(2)]
        public void BinaryExpression_Pow_Should_Pass()
        {
            GenerateVar("xxxxa", new BigDecimal(2));
            GenerateVar("xxxxb", new BigDecimal(4));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new IdentifierExpression("xxxxa"),
                BinaryOperatorType.Pow,
                new IdentifierExpression("xxxxb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(16));
        }

        [Test, Order(2)]
        public void BinaryExpression_Pwd_Should_Pass()
        {
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new ConstantExpression(new BigDecimal(52)),
                BinaryOperatorType.Pwd,
                new ConstantExpression(new BigDecimal(8)));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            var number = EvaluateRec(result, _ctx);
            Assert.Pass();
        }

        [Test, Order(3)]
        public void UnaryExpression_Factorial_Should_Pass()
        {
            GenerateVar("ma", new BigDecimal(3));
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Factorial,
                new IdentifierExpression("ma"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(6));
        }

        [Test, Order(3)]
        public void UnaryExpression_Print_Should_Pass()
        {
            GenerateVar("max", new BigDecimal(2));
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Print,
                new IdentifierExpression("max"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            Assert.AreEqual(_out.CurrentOutput, "2" + Environment.NewLine);
        }

        [Test, Order(3)]
        public void UnaryExpression_Print_Should_Pass_2()
        {
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Print,
                new GroupingExpression(
                    new BinaryExpression(
                        new ConstantExpression(new BigDecimal(3)),
                        BinaryOperatorType.Plus,
                        new UnaryExpression(UnaryOperatorType.Sqr, new ConstantExpression(new BigDecimal(9)))
                        )
                    )
                );
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            Assert.AreEqual(_out.CurrentOutput, "6" + Environment.NewLine);
        }
        [Test, Order(3)]
        public void UnaryExpression_Sqr_Should_Pass()
        {
            GenerateVar("mxa", new BigDecimal(81));
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Sqr,
                new IdentifierExpression("mxa"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(9));
        }

        [Test, Order(4)]
        public void GroupingExpression_Should_Pass()
        {
            var evaluator = new GroupingEvaluator();
            var input = new GroupingExpression(new ConstantExpression(new BigDecimal(44)));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(44));
        }

        [Test, Order(5)]
        public void Identifier_Should_Pass()
        {
            GenerateVar("mxaa", new BigDecimal(16));
            var evaluator = new IdentifierEvaluator();
            var input = new IdentifierExpression("mxaa");
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConstantExpression>(result);
            var casted = result as ConstantExpression;
            Assert.AreEqual(casted.Value, new BigDecimal(16));
        }

        [Test, Order(5)]
        public void Identifier_Should_Fail()
        {
            GenerateVar("masdxaa", new BigDecimal(16));
            var evaluator = new IdentifierEvaluator();
            var input = new IdentifierExpression("mxaasdav");
            try
            {
                var result = evaluator.Evaluate(input, _ctx);
                Assert.Fail();
            } catch
            {
                Assert.Pass();
            }
        }

        [Test, Order(6)]
        public void SetPrecision_Should_Pass()
        {
            var evaluator = new SetPrecisionEvaluator();
            var c = new BinaryExpression(
                new ConstantExpression(new BigDecimal(7)),
                BinaryOperatorType.Divide,
                new ConstantExpression(new BigDecimal(6))
                );
            var input = new SetPrecisionExpression(new ConstantExpression(new BigDecimal(4)));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            var evaluator2 = new UnaryEvaluator();
            var input2 = new UnaryExpression(
                UnaryOperatorType.Print,
                c);
            evaluator2.Evaluate(input2, _ctx);
            Assert.AreEqual(_out.CurrentOutput, "1,1667" + Environment.NewLine);
        }

        [Test, Order(7)]
        public void VarArgs_Avg_Should_Pass()
        {
            var evaluator = new VarArgsEvaluator();
            var input = new VarArgsExpression(
                VarArgsOperatorType.Avg,
                new Expression[]
                {
                    new ConstantExpression(new BigDecimal(2)),
                    new ConstantExpression(new BigDecimal(3)),
                    new ConstantExpression(new BigDecimal(7))
                }
                );
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            var number = EvaluateRec(result, _ctx);
            Assert.AreEqual(number, new BigDecimal(4));
        }

        [Test, Order(7)]
        public void VarArgs_Med_Should_Pass()
        {
            var evaluator = new VarArgsEvaluator();
            var input = new VarArgsExpression(
                VarArgsOperatorType.Med,
                new Expression[]
                {
                    new ConstantExpression(new BigDecimal(2)),
                    new ConstantExpression(new BigDecimal(4)),
                    new ConstantExpression(new BigDecimal(7)),
                    new ConstantExpression(new BigDecimal(8)),
                    new ConstantExpression(new BigDecimal(9))
                }
                );
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            var number = EvaluateRec(result, _ctx);
            Assert.AreEqual(number, new BigDecimal(7));
        }

        [Test, Order(7)]
        public void VarArgs_Med_Should_Pass_2()
        {
            var evaluator = new VarArgsEvaluator();
            var input = new VarArgsExpression(
                VarArgsOperatorType.Med,
                new Expression[]
                {
                    new ConstantExpression(new BigDecimal(8)),
                    new ConstantExpression(new BigDecimal(3)),
                    new ConstantExpression(new BigDecimal(5)),
                    new ConstantExpression(new BigDecimal(9))
                }
                );
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            var number = EvaluateRec(result, _ctx);
            Assert.AreEqual(number, new BigDecimal(6.5));
        }

        private void GenerateVar(string name, BigDecimal value)
        {
            var evaluator = new VariableDeclarationEvaluator();
            var input = new VariableDeclarationExpression(new IdentifierExpression(name), null);
            evaluator.Evaluate(input, _ctx);
            var evaluator2 = new AssignmentEvaluator();
            var input2 = new AssignmentExpression(new IdentifierExpression(name), new ConstantExpression(value));
            evaluator2.Evaluate(input2, _ctx);
        }

        private BigDecimal EvaluateRec(Expression e, ExecutionContext ctx)
        {
            var res = Reducer.Reduce(e, ctx);
            if (res == null)
            {
                return null;
            }
            return (res as ConstantExpression).Value;
        }
    }
}