using Deveel.Math;
using DwLang.Language;
using DwLang.Language.Expressions;
using DwLang.Language.Interpreter;
using DwLang.Tests.Mocks;
using Moq;
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
            _out = new MockOutputStream();
            _ctx = new ExecutionContext(_out);
        }

        [SetUp]
        public void Setup()
        {
            
        }

        [Test, Order(0)]
        public void VariableDeclaration_Declare_Should_Pass()
        {
            var evaluator = new VariableDeclarationEvaluator();
            var input = new VariableDeclaration(new Identifier("aa"), null);
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            var currentValue = _ctx.Get("aa");
            Assert.AreEqual(currentValue, null);
        }

        [Test, Order(1)]
        public void Assignment_Assign_Constant_To_Declared_Var_Should_Pass()
        {
            var evaluator = new AssignmentEvaluator();
            var value = new BigDecimal(1);
            var input = new Assignment(new Identifier("aa"), new Constant(value));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            var currentValue = _ctx.Get("aa");
            Assert.AreEqual(value, currentValue);
        }

        [Test, Order(1)]
        public void Assignment_Assign_Constant_To_Undeclared_Var_Should_Fail()
        {
            var evaluator = new AssignmentEvaluator();
            var value = new BigDecimal(1);
            var input = new Assignment(new Identifier("bb"), new Constant(value));
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
                new Identifier("za"),
                BinaryOperatorType.Plus,
                new Identifier("zb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(2));
        }

        [Test, Order(2)]
        public void BinaryExpression_Division_Should_Pass()
        {
            GenerateVar("xa", new BigDecimal(20));
            GenerateVar("xb", new BigDecimal(5));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new Identifier("xa"),
                BinaryOperatorType.Divide,
                new Identifier("xb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(4));
        }

        [Test, Order(2)]
        public void BinaryExpression_Multiply_Should_Pass()
        {
            GenerateVar("xxa", new BigDecimal(5));
            GenerateVar("xxb", new BigDecimal(5));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new Identifier("xxa"),
                BinaryOperatorType.Multiply,
                new Identifier("xxb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(25));
        }

        [Test, Order(2)]
        public void BinaryExpression_Minus_Should_Pass()
        {
            GenerateVar("xxxa", new BigDecimal(5));
            GenerateVar("xxxb", new BigDecimal(5));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new Identifier("xxxa"),
                BinaryOperatorType.Minus,
                new Identifier("xxxb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(0));
        }

        [Test, Order(2)]
        public void BinaryExpression_Pow_Should_Pass()
        {
            GenerateVar("xxxxa", new BigDecimal(2));
            GenerateVar("xxxxb", new BigDecimal(4));
            var evaluator = new BinaryExpressionEvaluator();
            var input = new BinaryExpression(
                new Identifier("xxxxa"),
                BinaryOperatorType.Pow,
                new Identifier("xxxxb"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(16));
        }

        [Test, Order(3)]
        public void UnaryExpression_Factorial_Should_Pass()
        {
            GenerateVar("ma", new BigDecimal(3));
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Factorial,
                new Identifier("ma"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(6));
        }

        [Test, Order(3)]
        public void UnaryExpression_Print_Should_Pass()
        {
            GenerateVar("max", new BigDecimal(2));
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Print,
                new Identifier("max"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNull(result);
            Assert.AreEqual(_out.CurrentOutput, "2\r\n");
        }

        [Test, Order(3)]
        public void UnaryExpression_Sqr_Should_Pass()
        {
            GenerateVar("mxa", new BigDecimal(16));
            var evaluator = new UnaryEvaluator();
            var input = new UnaryExpression(
                UnaryOperatorType.Sqr,
                new Identifier("mxa"));
            var result = evaluator.Evaluate(input, _ctx);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Constant>(result);
            var casted = result as Constant;
            Assert.AreEqual(casted.Value, new BigDecimal(4));
        }

        private void GenerateVar(string name, BigDecimal value)
        {
            var evaluator = new VariableDeclarationEvaluator();
            var input = new VariableDeclaration(new Identifier(name), null);
            evaluator.Evaluate(input, _ctx);
            var evaluator2 = new AssignmentEvaluator();
            var input2 = new Assignment(new Identifier(name), new Constant(value));
            evaluator2.Evaluate(input2, _ctx);
        }
    }
}