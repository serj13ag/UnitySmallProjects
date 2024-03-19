/*
  Copyright (c) 2008-2012 Ryan Juckett
  http://www.ryanjuckett.com/
 
  This software is provided 'as-is', without any express or implied
  warranty. In no event will the authors be held liable for any damages
  arising from the use of this software.
 
  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:
 
  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
 
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
 
  3. This notice may not be removed or altered from any source
     distribution.
*/

using UnityEngine;

namespace Springs
{
    public class SpringyMotionParams
    {
        public float PositionPosCoef;
        public float PositionVelCoef;

        public float VelocityPosCoef;
        public float VelocityVelCoef;
    }

    public static class SpringyUtils
    {
        private const float Epsilon = 0.0001f;

        public static void CalcDampedSpringMotionParams(ref SpringyMotionParams motionParams, float deltaTime,
            float angularFrequency, float dampingRatio)
        {
            if (dampingRatio < 0.0f)
            {
                dampingRatio = 0.0f;
                Debug.Log($"{nameof(dampingRatio)} is {dampingRatio}. Should be 0 or more, set to 0");
            }

            if (angularFrequency < 0.0f)
            {
                angularFrequency = 0.0f;
                Debug.Log($"{nameof(angularFrequency)} is {angularFrequency}. Should be 0 or more, set to 0");
            }

            if (angularFrequency < Epsilon)
            {
                motionParams.PositionPosCoef = 1.0f;
                motionParams.PositionVelCoef = 0.0f;
                motionParams.VelocityPosCoef = 0.0f;
                motionParams.VelocityVelCoef = 1.0f;
                return;
            }

            if (dampingRatio > 1.0f + Epsilon)
            {
                // over-damped
                var za = -angularFrequency * dampingRatio;
                var zb = angularFrequency * Mathf.Sqrt(dampingRatio * dampingRatio - 1.0f);
                var z1 = za - zb;
                var z2 = za + zb;

                var e1 = Mathf.Exp(z1 * deltaTime);
                var e2 = Mathf.Exp(z2 * deltaTime);

                var invTwoZb = 1.0f / (2.0f * zb);

                var e1_Over_TwoZb = e1 * invTwoZb;
                var e2_Over_TwoZb = e2 * invTwoZb;

                var z1e1_Over_TwoZb = z1 * e1_Over_TwoZb;
                var z2e2_Over_TwoZb = z2 * e2_Over_TwoZb;

                motionParams.PositionPosCoef = e1_Over_TwoZb * z2 - z2e2_Over_TwoZb + e2;
                motionParams.PositionVelCoef = -e1_Over_TwoZb + e2_Over_TwoZb;

                motionParams.VelocityPosCoef = (z1e1_Over_TwoZb - z2e2_Over_TwoZb + e2) * z2;
                motionParams.VelocityVelCoef = -z1e1_Over_TwoZb + z2e2_Over_TwoZb;
            }
            else if (dampingRatio < 1.0f - Epsilon)
            {
                // under-damped
                var omegaZeta = angularFrequency * dampingRatio;
                var alpha = angularFrequency * Mathf.Sqrt(1.0f - dampingRatio * dampingRatio);

                var expTerm = Mathf.Exp(-omegaZeta * deltaTime);
                var cosTerm = Mathf.Cos(alpha * deltaTime);
                var sinTerm = Mathf.Sin(alpha * deltaTime);

                var invAlpha = 1.0f / alpha;

                var expSin = expTerm * sinTerm;
                var expCos = expTerm * cosTerm;
                var expOmegaZetaSin_Over_Alpha = expTerm * omegaZeta * sinTerm * invAlpha;

                motionParams.PositionPosCoef = expCos + expOmegaZetaSin_Over_Alpha;
                motionParams.PositionVelCoef = expSin * invAlpha;

                motionParams.VelocityPosCoef = -expSin * alpha - omegaZeta * expOmegaZetaSin_Over_Alpha;
                motionParams.VelocityVelCoef = expCos - expOmegaZetaSin_Over_Alpha;
            }
            else
            {
                // critically damped
                var expTerm = Mathf.Exp(-angularFrequency * deltaTime);
                var timeExp = deltaTime * expTerm;
                var timeExpFreq = timeExp * angularFrequency;

                motionParams.PositionPosCoef = timeExpFreq + expTerm;
                motionParams.PositionVelCoef = timeExp;

                motionParams.VelocityPosCoef = -angularFrequency * timeExpFreq;
                motionParams.VelocityVelCoef = -timeExpFreq + expTerm;
            }
        }

        public static void UpdateDampedSpringMotion(ref float position, ref float velocity, float equilibriumPosition,
            in SpringyMotionParams springParams)
        {
            var oldPos = position - equilibriumPosition;
            var oldVel = velocity;

            position = oldPos * springParams.PositionPosCoef + oldVel * springParams.PositionVelCoef + equilibriumPosition;
            velocity = oldPos * springParams.VelocityPosCoef + oldVel * springParams.VelocityVelCoef;
        }
    }
}