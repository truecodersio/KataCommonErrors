using System.Collections.Generic;
using System.Linq;

namespace CommonErrors.Shared
{
    public class AnswerQueue<T> : Queue<T> where T : IGradable
    {
        public int Size { get; }
        public decimal Grade => this.Any() ? this.Average(x => x.Grade) : 0;

        /// <inheritdoc/>
        /// <summary>
        /// Queue that cannot exceed it's size
        /// </summary>
        /// <param name="size">Maximum size of the queue</param>
        public AnswerQueue(int size)
        {
            Size = size;
        }

        /// <summary>
        /// Hides the default implementation of queue Enqueue 
        /// </summary>
        /// <param name="item"></param>
        public new void Enqueue(T item)
        {
            if (Count >= Size) { Dequeue(); }
            base.Enqueue(item);
        }
    }
}
