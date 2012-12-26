using System;
using System.Collections.Generic;
using System.Text;

namespace Z.Util
{
    /// <summary>
    /// ��ѧ���㹤��
    /// </summary>
    public static class MathTools
    {
        #region MinValue

        /// <summary>
        /// �Ƚϴ��������Ϊ T�����飬 ������Сֵ, T����֧�� IComparable �ӿ�
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static T MinValue<T>(params T[] numbers) where T: IComparable
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentNullException("����Ϊ��");

            T min = numbers[0];

            foreach (T i in numbers)
            {
                if ((i as IComparable).CompareTo(min) < 0)
                    min = i;
            }

            return min;
        }

        #endregion

        #region MaxValue

        /// <summary>
        /// �Ƚϴ��������Ϊ T�����飬 ������Сֵ, T����֧�� IComparable �ӿ�
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static T MaxValue<T>(params T[] numbers) where T : IComparable
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentNullException("����Ϊ��");

            T max = numbers[0];

            foreach (T i in numbers)
            {
                if ((i as IComparable).CompareTo(max) > 0)
                    max = i;
            }

            return max;
        }

        #endregion
    }
}
