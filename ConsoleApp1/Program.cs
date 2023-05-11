namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RList list1 = new RList(6);
            //RList list2 = new RList(52);
            //list1.AddBefore(0, 5);
            //list1.AddBefore(0, 4);
            //list1.AddBefore(0, 3);
            //list1.AddBefore(0, 2);
            //list1.AddBefore(0, 1);
            list1.AddBeforeByValue(0, 6);
            list1.AddBeforeByValue(15212, -12);
            list1.MyPrint();
            //list1.DeleteBetween(3, 10);
            Console.WriteLine();
            //list1.MyPrint();

            //list1.AddBefore(0, 5);
            //RList result = list1 + list2;
            //result.MyPrint();
            //list1.DeleteBetween(-12, 3);

        }
    }
    class RList
    {
        public int? info;
        public RList? next;

        // 1. Конструктор з одним параметром (число); 
        public RList(int i)
        {
            info = i;
            next = null;
        }
        // 2. Конструктор з двома параметрами (число, посилання на наступний елемент);
        public RList(int? i, RList n)
        {
            info = i;
            next = n;
        }
        public RList(RList other)
        {
            info = other.info;
            next = other.next;
        }
        // 7. Не рекурсивний метод додавання нового елемента n-ним у список;
        public void AddByIndex(int index, int value)
        {
            // Метод рахує елементи від 1
            var node = new RList(value);

            if (index <= 1)
            {
                node.next = this.next;
                this.next = node;
                (node.info, this.info) = (this.info, node.info);
                return;
            }
            int length = GetLength();
            if (index > length)
            {
                var current1 = this;
                while (current1.next != null)
                {
                    current1 = current1.next;
                }
                current1.next = new RList(value);
                return;
            }
            var current = this;
            int count = 1;
            while (count != index - 1)
            {
                current = current.next;
                count++;
            }
            node.next = current.next;
            current.next = node;
        }
        public int GetLength()
        {
            var current = this;
            int length = 0;
            while (current != null)
            {
                length++;
                current = current.next;
            }
            return length;
        }
        // 9. Метод додавання нового елементу у список перед елементом із заданим значенням;
        // OKAY
        public void AddBeforeByValue(int data, int byValue)
        {
            RList newNode = new RList(data);
            var current = this;
            if (current == null) return;
            if(current.info == byValue)
            {
                newNode = new RList(current);
                current.info = data;
                current.next = newNode;
                return;
            }
            if(current.next == null)
            {
                current.next = newNode;
                return;
            }
            while (current.next != null && current.next.info != byValue)
            {
                current = current.next;
            }
            newNode.next = current.next;
            current.next = newNode;
            return;
        }

        //public void AddBefore(int index, int n)
        //{
        //    // рахуєм елементи від 0
        //    if (index <= 0)
        //    {
        //        RList newNode = new RList(info, next);
        //        info = n;
        //        next = newNode;
        //    }
        //    else if (next == null)
        //    {
        //        RList newNode = new RList(n, null);
        //        next = newNode;
        //    }
        //    else
        //    {
        //        next.AddBefore(index - 1, n);
        //    }
        //}

        // 14.Не рекурсивний метод видалення останнього в списку елемента;
        public void RemoveLast()
        {
            if (next == null)
            {
                info = null;
                return;
            }
            else if (next.next == null)
                next = null;
            else
                next.RemoveLast();
        }
        // 23 Метод видалення всіх парних по порядку елементів;
        public void RemoveEvenNodes()
        {
            RList? curr = this;
            while (curr.next != null && curr.next.next != null)
            {
                curr.next = curr.next.next;
                curr = curr.next;
            }
            if (curr.next != null && curr.next.next == null)
            {
                curr.next = null;
            }
        }

        // myPrint
        public void MyPrint()
        {
            Console.Write(info + " ");
            if (next != null)
            {
                next.MyPrint();
            }

        }
        // 33. Рекурсивний метод друку елементів списку у прямому порядку у стовпець;
        public void Print()
        {
            Console.WriteLine(info + " ");
            if (next != null)
            {
                next.Print();
            }

        }
        // 47.Метод з двома параметрами, що дозволяє видалити всі елементи списку, розташовані  між елементами із заданими номерами.
        public void DeleteBetween(int a, int b)
        {
            if (b < a)
            {
                (a, b) = (b, a);
            }

            int length = GetLength();

            if (a < 1 && b > length)
            {
                info = null;
                next = null;
                return;
            }

            if (b > length)
            {
                b = length + 1;
            }

            if (b == a || a > length) return;

            if (a < 1)
            {
                RList current = this;
                for (int i = 1; i < b; i++)
                {
                    current = current.next;
                }
                info = current.info;
                next = current.next;
                return;
            }

            RList firstBetween = this;
            for (int i = 1; i < a; i++)
            {
                firstBetween = firstBetween.next;
            }

            RList secondBetween = firstBetween.next;
            for (int i = 1; i <= b - a - 1; i++)
            {
                secondBetween = secondBetween.next;
            }

            firstBetween.next = secondBetween;
        }
        // 53. Властивість IsOne, яка  дорівнює  true, якщо   кожне  значення  у  списку дорівнює 1 (тільки для  зчитування)
        public bool IsOne
        {
            get
            {
                RList current = this;
                while (current != null)
                {
                    if (current.info != 1)
                    {
                        return false;
                    }
                    current = current.next;
                }
                return true;
            }
            private set { }
        }

        // 66. Перевизначити для списку  операцію    ++
        // Добавляє 1-цю до списку

        public static RList operator ++(RList list)
        {
            if (list == null)
            {
                return new RList(1, null);
            }
            else
            {
                RList current = list;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = new RList(1, null);
                return list;
            }
        }
        // 79. Перевизначити для списку будь-яку операцію (+)
        // Об'єднує два списка
        public static RList operator +(RList list1, RList list2)
        {
            if (list1 == null)
            {
                return list2;
            }
            else if (list2 == null)
            {
                return list1;
            }
            else
            {
                RList current = list1;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = list2;
                return list1;
            }
        }
    }
}