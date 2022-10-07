using System;
using System.Collections;

public class CircularBuffer <T> : IEnumerable
{
    public int Count => _elements.Length; // Count of elements in the array
    
    private T[] _elements; // Array
    private int _index; // Index of a new element
    
    public T this[int index] => Get(index); // Indexer
    
    public CircularBuffer(int count) => _elements = new T[count]; // Creates a new array based on the number from the user
    
    public void Add(T element) // Adds a new element to the array
    {
        _elements[_index % _elements.Length] = element; // Adds an element to the index using a formula
        _index++;
    }
    
    private T Get(int index) // Gets an element by an index
    {
        // If an index smaller than its Lenght, we simply return an element by the user's index, otherwise calculates
        // the element by the formula
        return _index < _elements.Length ? _elements[index] : _elements[(index + _index) % _elements.Length];
    }
    
    public IEnumerator GetEnumerator()
    {
        return new CircularBufferEnumerator(this);
    }
    
    private class CircularBufferEnumerator : IEnumerator
    {
        private readonly CircularBuffer<T> _circularBuffer;
        private int _index;
    
        public CircularBufferEnumerator(CircularBuffer<T> circularBuffer)
        {
            _circularBuffer = circularBuffer;
            _index = -1;
        }
        public bool MoveNext()
        {
            _index++;
            return _circularBuffer.Count > _index;
        }
    
        public void Reset()
        {
            _index = -1;
        }
    
        public object Current => _circularBuffer.Get(_index);
    }
}