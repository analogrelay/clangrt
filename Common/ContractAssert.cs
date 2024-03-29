﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LibClangSharp.Common
{
    internal static class ContractAssert
    {
        public static void NotNull(Expression<Action> op, string paramName, bool ignoreTrace = false)
        {
            Action act = op.Compile();
            ArgumentNullException argEx = Assert.Throws<ArgumentNullException>(() => act());
            VerifyArgEx(argEx, paramName, ignoreTrace ? null : op);
        }

        public static void OutOfRange(Expression<Action> op, string paramName, bool ignoreTrace = false)
        {
            InvalidArgument<ArgumentOutOfRangeException>(op, paramName, ignoreTrace);
        }

        public static void ValidEnumMember(Expression<Action> op, string paramName, Type enumType, bool ignoreTrace = false)
        {
            Action act = op.Compile();
            ArgumentOutOfRangeException argEx = Assert.Throws<ArgumentOutOfRangeException>(() => act());
            VerifyArgEx(argEx, paramName, ignoreTrace ? null : op);
            Assert.Equal(
                ContractAssert.ToFullArgExMessage(
                    String.Format(
                        CommonResources.Argument_InvalidEnumValue,
                        paramName,
                        enumType.FullName),
                    paramName),
                argEx.Message);
        }

        public static void InvalidArgument<T>(Expression<Action> op, string paramName, bool ignoreTrace = false) where T : ArgumentException
        {
            Action act = op.Compile();
            T argEx = Assert.Throws<T>(() => act());
            VerifyArgEx(argEx, paramName, ignoreTrace ? null : op);
        }

        public static void NotNullOrEmpty(Expression<Action<string>> op, string paramName, bool ignoreTrace = false)
        {
            Action<string> act = op.Compile();
            VerifyNotNullOrEmpty(Assert.Throws<ArgumentException>(() => act(null)), paramName, ignoreTrace ? null : op);
            VerifyNotNullOrEmpty(Assert.Throws<ArgumentException>(() => act(String.Empty)), paramName, ignoreTrace ? null : op);
        }

        private static void VerifyNotNullOrEmpty(ArgumentException argumentException, string paramName, LambdaExpression op)
        {
            Assert.Equal(
                ToFullArgExMessage(String.Format(CommonResources.Argument_NotNullOrEmpty, paramName), paramName),
                argumentException.Message);
            VerifyArgEx(argumentException, paramName, op);
        }

        private static void VerifyArgEx(ArgumentException argumentException, string paramName, LambdaExpression op)
        {
            if (op != null && op.Body.NodeType == ExpressionType.Call)
            {
                // Check and make sure that call is on the top of the stack after removing Requires
                var call = ((MethodCallExpression)op.Body);
                var expected = call.Method;
                StackTrace stack = new StackTrace(argumentException);
                var frame = stack.GetFrames().SkipWhile(f => f.GetMethod().DeclaringType.FullName == typeof(Requires).FullName).FirstOrDefault();
                var actual = frame.GetMethod();
                Assert.True(actual != null, "Unable to find stack frame.");

                string expectedSite = call.Object.Type.FullName + "." + expected.Name;
                string actualSite = actual.DeclaringType.FullName + "." + actual.Name;
                Assert.True(String.Equals(expectedSite, actualSite),
                            "Expected exception was thrown at an unexpected site." + Environment.NewLine +
                            "Expected: " + expectedSite + Environment.NewLine +
                            "Actual: " + actualSite);
            }

            Assert.Equal(paramName, argumentException.ParamName);
        }

        public static string ToFullArgExMessage(string message, string paramName)
        {
            return String.Format("{0}\r\nParameter name: {1}", message, paramName);
        }
    }
}
