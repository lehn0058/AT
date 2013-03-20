using AT.Core;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AT.Data
{
    /// <summary>
    /// Used to extract parameter values from IQueryable expression trees.
    /// </summary>
    public class ParameterExtractorExpressionVisitor : ExpressionVisitor
    {
        private HashSet<Expression> _visitedNodes = new HashSet<Expression>();
        private Queue<Object> _extractedParameters = new Queue<object>();
        private Dictionary<IQueryable, Queue<object>> _allExtractedParemeters = new Dictionary<IQueryable, Queue<object>>();

        /// <summary>
        /// Retrieves all extracted parameters for the given query. Note that the query's
        /// expression tree must have been previously visited in order to use this method.
        /// </summary>
        /// <param name="query">The query we are requesting the parameters for.</param>
        /// <returns></returns>
        public Queue<Object> ExtractedParametersForQuery(IQueryable query)
        {
            return _allExtractedParemeters[query];
        }

        /// <summary>
        /// Begins visiting each node of the expression tree describing an IQueryable entity.
        /// Will find and keep a record of each parameter that has been passed into the query.
        /// </summary>
        /// <param name="query">The entity to explore each node of for parameters..</param>
        public void Visit(IQueryable query)
        {
            Argument.NotNull(() => query);

            // Begin parameter extraction
            base.Visit(query.Expression);

            // Complete parameter extraction
            _allExtractedParemeters.Add(query, _extractedParameters);
            _visitedNodes = new HashSet<Expression>();
            _extractedParameters = new Queue<object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override Expression VisitUnary(UnaryExpression node)
        {
            Argument.NotNull(() => node);

            try
            {
                dynamic dynamicNode = node.Operand;
                var body = dynamicNode.Body;

                if (!_visitedNodes.Contains(node))
                {
                    var argument = body.Arguments[0];
                    object nodeValue = ExtractValue(argument);

                    if (nodeValue != null)
                    {
                        _extractedParameters.Enqueue(nodeValue);
                    }

                    _visitedNodes.Add(node);
                }
            }
            catch (RuntimeBinderException)
            {
                // If an exception happens here, then the parameter was not reachable.
            }

            return base.VisitUnary(node);
        }

        /// <summary>
        /// Override for when potentially visiting a node that has a parameter value.
        /// </summary>
        /// <param name="node">The node to visit</param>
        /// <returns></returns>
        protected override Expression VisitBinary(BinaryExpression node)
        {
            Argument.NotNull(() => node);

            Expression left = this.Visit(node.Left);
            Expression right = this.Visit(node.Right);

            if (!_visitedNodes.Contains(left))
            {
                object leftValue = ExtractValue(left);
                if (leftValue != null)
                {
                    _extractedParameters.Enqueue(leftValue);
                }

                _visitedNodes.Add(left);
            }

            if (!_visitedNodes.Contains(right))
            {
                object rightValue = ExtractValue(right);
                if (rightValue != null)
                {
                    _extractedParameters.Enqueue(rightValue);
                }

                _visitedNodes.Add(right);
            }

            return base.VisitBinary(node);
        }

        private static object ExtractValue(Expression expression)
        {
            try
            {
                Delegate f = Expression.Lambda(expression).Compile();
                return f.DynamicInvoke();
            }
            catch (InvalidOperationException)
            {
                // If an exception happens here, then the parameter was not readable.
            }

            return null;
        }
    }
}