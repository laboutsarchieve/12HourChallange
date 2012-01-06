using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    static class GraphMath
    {
        /// <summary>
        /// Finds a point in between two points, a and b
        /// </summary>
        /// <param name="a">The first point</param>
        /// <param name="b">The seconds point</param>
        /// <param name="amountGreaterThanA">The amount more than a the value is</param>
        /// <returns>The values between A and B</returns>
        static public double CosineInterpolate( double a, double b, double amountGreaterThanA )
        {
            double angle = amountGreaterThanA * Math.PI; 

            double weightOfB = (1.0 - Math.Cos(angle)) * 0.5;

            return a * (1.0 - weightOfB) + b*weightOfB;
        }
        /// <summary>
        /// Calculates and returns the adverage of a and b
        /// </summary>
        /// <param name="a">The First Point</param>
        /// <param name="b">The Second Point</param>
        /// <returns>The value between a and b</returns>
        static public double LinearInterpolate( double a, double b )
        {
            return (a + b)/2.0; // Average of a and b
        }
    }
}
