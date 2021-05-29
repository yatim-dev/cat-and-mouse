using System.Collections;
using System.Collections.Generic;

namespace cat_and_mouse.Domain
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private readonly int length;
        private readonly SinglyLinkedList<T> previous;
        public readonly T Value;

        public SinglyLinkedList(T value, SinglyLinkedList<T> previous = null)
        {
            Value = value;
            this.previous = previous;
            length = previous?.length + 1 ?? 1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            yield return Value;
            var pathItem = previous;
            while (pathItem != null)
            {
                yield return pathItem.Value;
                pathItem = pathItem.previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}