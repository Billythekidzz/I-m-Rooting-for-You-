// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0060, IDE0066

namespace Animatext
{
    public static class SortUtility
    {
        public static int[] Order(int length, SortType type)
        {
            switch (type)
            {
                case SortType.FrontToBack:
                    return OrderFrontToBack(length);

                case SortType.BackToFront:
                    return OrderBackToFront(length);

                case SortType.FrontToMiddle:
                    return OrderFrontToMiddle(length);

                case SortType.MiddleToFront:
                    return OrderMiddleToFront(length);

                case SortType.BackToMiddle:
                    return OrderBackToMiddle(length);

                case SortType.MiddleToBack:
                    return OrderMiddleToBack(length);

                case SortType.MiddleToSidesFront:
                    return OrderMiddleToSidesFront(length);

                case SortType.MiddleToSidesBack:
                    return OrderMiddleToSidesBack(length);

                case SortType.SidesToMiddleFront:
                    return OrderSidesToMiddleFront(length);

                case SortType.SidesToMiddleBack:
                    return OrderSidesToMiddleBack(length);

                default:
                    return OrderFrontToBack(length);
            }
        }

        public static int[] Rank(int length, SortType type)
        {
            switch (type)
            {
                case SortType.FrontToBack:
                    return RankFrontToBack(length);

                case SortType.BackToFront:
                    return RankBackToFront(length);

                case SortType.FrontToMiddle:
                    return RankFrontToMiddle(length);

                case SortType.MiddleToFront:
                    return RankMiddleToFront(length);

                case SortType.BackToMiddle:
                    return RankBackToMiddle(length);

                case SortType.MiddleToBack:
                    return RankMiddleToBack(length);

                case SortType.MiddleToSidesFront:
                    return RankMiddleToSidesFront(length);

                case SortType.MiddleToSidesBack:
                    return RankMiddleToSidesBack(length);

                case SortType.SidesToMiddleFront:
                    return RankSidesToMiddleFront(length);

                case SortType.SidesToMiddleBack:
                    return RankSidesToMiddleBack(length);

                default:
                    return RankFrontToBack(length);
            }
        }

        public static int Order(int index, int length, SortType type)
        {
            switch (type)
            {
                case SortType.FrontToBack:
                    return OrderFrontToBack(index, length);

                case SortType.BackToFront:
                    return OrderBackToFront(index, length);

                case SortType.FrontToMiddle:
                    return OrderFrontToMiddle(index, length);

                case SortType.MiddleToFront:
                    return OrderMiddleToFront(index, length);

                case SortType.BackToMiddle:
                    return OrderBackToMiddle(index, length);

                case SortType.MiddleToBack:
                    return OrderMiddleToBack(index, length);

                case SortType.MiddleToSidesFront:
                    return OrderMiddleToSidesFront(index, length);

                case SortType.MiddleToSidesBack:
                    return OrderMiddleToSidesBack(index, length);

                case SortType.SidesToMiddleFront:
                    return OrderSidesToMiddleFront(index, length);

                case SortType.SidesToMiddleBack:
                    return OrderSidesToMiddleBack(index, length);

                default:
                    return OrderFrontToBack(index, length);
            }
        }

        public static int Rank(int index, int length, SortType type)
        {
            switch (type)
            {
                case SortType.FrontToBack:
                    return RankFrontToBack(index, length);

                case SortType.BackToFront:
                    return RankBackToFront(index, length);

                case SortType.FrontToMiddle:
                    return RankFrontToMiddle(index, length);

                case SortType.MiddleToFront:
                    return RankMiddleToFront(index, length);

                case SortType.BackToMiddle:
                    return RankBackToMiddle(index, length);

                case SortType.MiddleToBack:
                    return RankMiddleToBack(index, length);

                case SortType.MiddleToSidesFront:
                    return RankMiddleToSidesFront(index, length);

                case SortType.MiddleToSidesBack:
                    return RankMiddleToSidesBack(index, length);

                case SortType.SidesToMiddleFront:
                    return RankSidesToMiddleFront(index, length);

                case SortType.SidesToMiddleBack:
                    return RankSidesToMiddleBack(index, length);

                default:
                    return RankFrontToBack(index, length);
            }
        }

        public static int[] OrderFrontToBack(int length)
        {
            int[] values = new int[length];

            for (int i = 0; i < length; i++)
            {
                values[i] = i;
            }

            return values;
        }

        public static int[] RankFrontToBack(int length)
        {
            int[] values = new int[length];

            for (int i = 0; i < length; i++)
            {
                values[i] = i;
            }

            return values;
        }

        public static int OrderFrontToBack(int index, int length)
        {
            return index;
        }

        public static int RankFrontToBack(int index, int length)
        {
            return index;
        }

        public static int[] OrderBackToFront(int length)
        {
            int[] values = new int[length];

            for (int i = 0; i < length; i++)
            {
                values[length - i - 1] = i;
            }

            return values;
        }

        public static int[] RankBackToFront(int length)
        {
            int[] values = new int[length];

            for (int i = 0; i < length; i++)
            {
                values[i] = length - i - 1;
            }

            return values;
        }

        public static int OrderBackToFront(int index, int length)
        {
            return length - index - 1;
        }

        public static int RankBackToFront(int index, int length)
        {
            return length - index - 1;
        }

        public static int[] OrderFrontToMiddle(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[length + number - i - 1] = i;
            }

            return values;
        }

        public static int[] RankFrontToMiddle(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = length + number - i - 1;
            }

            return values;
        }

        public static int OrderFrontToMiddle(int index, int length)
        {
            return index * 2 < length - 1 ? index : length + length / 2 - index - 1;
        }

        public static int RankFrontToMiddle(int index, int length)
        {
            return index * 2 < length - 1 ? index : length + length / 2 - index - 1;
        }

        public static int[] OrderMiddleToFront(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[length - i - 1] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[i - number] = i;
            }

            return values;
        }

        public static int[] RankMiddleToFront(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = length - i - 1;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = i - number;
            }

            return values;
        }

        public static int OrderMiddleToFront(int index, int length)
        {
            return index * 2 <= length - 1 ? index + length / 2 : length - index - 1;
        }

        public static int RankMiddleToFront(int index, int length)
        {
            return index * 2 < length - 1 ? length - index - 1 : index - length / 2;
        }

        public static int[] OrderBackToMiddle(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[length - number + i] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[length - i - 1] = i;
            }

            return values;
        }

        public static int[] RankBackToMiddle(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = length - number + i;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = length - i - 1;
            }

            return values;
        }

        public static int OrderBackToMiddle(int index, int length)
        {
            return index * 2 < length - 1 ? length - index - 1 : index - length / 2;
        }

        public static int RankBackToMiddle(int index, int length)
        {
            return index * 2 <= length - 1 ? length / 2 + index : length - index - 1;
        }

        public static int[] OrderMiddleToBack(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[number - i - 1] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = i;
            }

            return values;
        }

        public static int[] RankMiddleToBack(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = number - i - 1;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = i;
            }

            return values;
        }

        public static int OrderMiddleToBack(int index, int length)
        {
            return index * 2 <= length - 1 ? length - length / 2 - index - 1 : index;
        }

        public static int RankMiddleToBack(int index, int length)
        {
            return index * 2 <= length - 1 ? length - length / 2 - index - 1 : index;
        }

        public static int[] OrderMiddleToSidesFront(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[length - i * 2 - 2] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[i * 2 - length + 1] = i;
            }

            return values;
        }

        public static int[] RankMiddleToSidesFront(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = length - i * 2 - 2;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = i * 2 - length + 1;
            }

            return values;
        }

        public static int OrderMiddleToSidesFront(int index, int length)
        {
            return (length - index) % 2 == 0 ? length / 2 - index / 2 - 1 : length / 2 + index / 2;
        }

        public static int RankMiddleToSidesFront(int index, int length)
        {
            return index * 2 < length - 1 ? length - index * 2 - 2 : index * 2 - length + 1;
        }

        public static int[] OrderMiddleToSidesBack(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[length - i * 2 - 1] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[i * 2 - length] = i;
            }

            return values;
        }

        public static int[] RankMiddleToSidesBack(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = length - i * 2 - 1;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = i * 2 - length;
            }

            return values;
        }

        public static int OrderMiddleToSidesBack(int index, int length)
        {
            return (length - index) % 2 == 0 ? length - length / 2 + index / 2 : length - length / 2 - index / 2 - 1;
        }

        public static int RankMiddleToSidesBack(int index, int length)
        {
            return index * 2 <= length - 1 ? length - index * 2 - 1 : index * 2 - length;
        }

        public static int[] OrderSidesToMiddleFront(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i * 2] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[length * 2 - i * 2 - 1] = i;
            }

            return values;
        }

        public static int[] RankSidesToMiddleFront(int length)
        {
            int[] values = new int[length];
            int number = length - length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = i * 2;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = length * 2 - i * 2 - 1;
            }

            return values;
        }

        public static int OrderSidesToMiddleFront(int index, int length)
        {
            return index % 2 == 0 ? index / 2 : length - index / 2 - 1;
        }

        public static int RankSidesToMiddleFront(int index, int length)
        {
            return index * 2 <= length - 1 ? index * 2 : length * 2 - index * 2 - 1;
        }

        public static int[] OrderSidesToMiddleBack(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i * 2 + 1] = i;
            }

            for (int i = number; i < length; i++)
            {
                values[length * 2 - i * 2 - 2] = i;
            }

            return values;
        }

        public static int[] RankSidesToMiddleBack(int length)
        {
            int[] values = new int[length];
            int number = length / 2;

            for (int i = 0; i < number; i++)
            {
                values[i] = i * 2 + 1;
            }

            for (int i = number; i < length; i++)
            {
                values[i] = length * 2 - i * 2 - 2;
            }

            return values;
        }

        public static int OrderSidesToMiddleBack(int index, int length)
        {
            return index % 2 == 0 ? length - index / 2 - 1 : index / 2;
        }

        public static int RankSidesToMiddleBack(int index, int length)
        {
            return index * 2 < length - 1 ? index * 2 + 1 : length * 2 - index * 2 - 2;
        }
    }
}